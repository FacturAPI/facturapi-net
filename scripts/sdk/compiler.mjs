import assert from "node:assert/strict";
import openapiTS, { astToString } from "openapi-typescript";
import ts from "typescript";

export const pascal = (value) =>
  value.replace(/(^|[_-])([a-z])/g, (_, prefix, letter) =>
    letter.toUpperCase(),
  );
export const camel = (value) =>
  pascal(value).replace(/^./, (letter) => letter.toLowerCase());

// Let openapi-typescript and the TypeScript checker resolve references,
// intersections and nullability. Rendering never reinterprets JSON Schema.
export async function compileContract(spec, config) {
  const operations = new Map();
  for (const [path, item] of Object.entries(spec.paths)) {
    for (const [verb, operation] of Object.entries(item)) {
      if (
        !operation.operationId ||
        !(
          path === "/" + config.resource ||
          path.startsWith("/" + config.resource + "/")
        )
      )
        continue;
      assert(
        !operations.has(operation.operationId),
        `Duplicate operation: ${operation.operationId}`,
      );
      operations.set(operation.operationId, { ...operation, path, verb });
    }
  }
  assert.deepEqual(
    [...operations.keys()].sort(),
    Object.values(config.methods).sort(),
    "Every Customers operation must have a method binding.",
  );
  const filename = "/contract.ts";
  const content =
    "type Integer = number & { readonly __integer: unique symbol };\n" +
    astToString(
      await openapiTS(spec, {
        defaultNonNullable: false,
        transform(schema) {
          const type =
            schema.format === "date-time"
              ? ts.factory.createTypeReferenceNode("Date")
              : schema.type === "integer" ||
                  (Array.isArray(schema.type) &&
                    schema.type.includes("integer") &&
                    schema.type.every(
                      (type) => type === "integer" || type === "null",
                    ))
                ? ts.factory.createTypeReferenceNode("Integer")
                : null;
          if (type)
            return Array.isArray(schema.type) && schema.type.includes("null")
              ? ts.factory.createUnionTypeNode([
                  type,
                  ts.factory.createLiteralTypeNode(ts.factory.createNull()),
                ])
              : type;
        },
      }),
    );
  const options = {
    strict: true,
    target: ts.ScriptTarget.ES2022,
    skipLibCheck: true,
  };
  const host = ts.createCompilerHost(options);
  const original = host.getSourceFile.bind(host);
  host.getSourceFile = (path, version, ...rest) =>
    path === filename
      ? ts.createSourceFile(path, content, version, true)
      : original(path, version, ...rest);
  const program = ts.createProgram([filename], options, host);
  assert.equal(
    program.getSemanticDiagnostics().length,
    0,
    "The generated OpenAPI types must compile.",
  );
  const checker = program.getTypeChecker();
  const source = program.getSourceFile(filename);
  const at = (symbol) => checker.getTypeOfSymbolAtLocation(symbol, source);
  const property = (type, name) => {
    const symbol = checker.getPropertyOfType(
      checker.getNonNullableType(type),
      name,
    );
    assert(symbol, `Missing contract property: ${name}`);
    return at(symbol);
  };
  const schemas = property(
    checker.getTypeAtLocation(
      source.statements.find(
        (node) =>
          ts.isInterfaceDeclaration(node) && node.name.text === "components",
      ),
    ),
    "schemas",
  );
  const operationTypes = checker.getTypeAtLocation(
    source.statements.find(
      (node) =>
        ts.isInterfaceDeclaration(node) && node.name.text === "operations",
    ),
  );
  const models = new Map();
  const names = new Map();
  for (const symbol of checker.getPropertiesOfType(schemas)) {
    names.set(
      checker.getNonNullableType(at(symbol)),
      config.modelNames[symbol.name] || symbol.name,
    );
  }
  const compiled = new Map();
  const prose = (symbol) =>
    [
      ts.displayPartsToString(symbol.getDocumentationComment(checker)),
      ...symbol
        .getJsDocTags(checker)
        .filter((tag) => ["description", "deprecated"].includes(tag.name))
        .map((tag) => ts.displayPartsToString(tag.text)),
    ]
      .filter(Boolean)
      .join("\n");
  function compile(type, hint, input) {
    const nullable =
      type.isUnion() &&
      type.types.some((item) => item.flags & ts.TypeFlags.Null);
    type = checker.getNonNullableType(type);
    if (type.aliasSymbol?.name === "Integer")
      return { kind: "integer", nullable };
    if (type.symbol?.name === "Date")
      return { kind: input ? "string" : "date", nullable };
    if (
      type.flags & ts.TypeFlags.BooleanLike ||
      (type.isUnion() &&
        type.types.every((item) => item.flags & ts.TypeFlags.BooleanLike))
    )
      return { kind: "boolean", nullable };
    if (type.flags & ts.TypeFlags.NumberLike)
      return { kind: "number", nullable };
    if (
      type.flags & ts.TypeFlags.String ||
      (type.isUnion() &&
        type.types.every((item) => item.flags & ts.TypeFlags.StringLike) &&
        type.types.some((item) => !item.isStringLiteral()))
    )
      return { kind: "string", nullable };
    if (
      type.isStringLiteral() ||
      (type.isUnion() && type.types.every((item) => item.isStringLiteral()))
    ) {
      // Literal constraints remain visible in docs; strings preserve compatibility
      // with catalog values and allow response enums to evolve without parse errors.
      return {
        kind: "string",
        nullable,
        values: (type.isUnion() ? type.types : [type]).map(
          (item) => item.value,
        ),
      };
    }
    if (checker.isArrayType(type))
      return {
        kind: "array",
        nullable,
        item: compile(
          checker.getIndexTypeOfType(type, ts.IndexKind.Number),
          hint + "Item",
          input,
        ),
      };
    assert(
      !(
        type.flags &
        (ts.TypeFlags.Any | ts.TypeFlags.Unknown | ts.TypeFlags.Never)
      ),
      `Unsupported open type at ${hint}`,
    );
    assert(
      !type.isUnion(),
      `Unsupported union at ${hint}: ${checker.typeToString(type)}. Add an explicit binding instead of widening the type.`,
    );
    assert(
      !checker.getIndexTypeOfType(type, ts.IndexKind.String),
      `Unsupported open dictionary at ${hint}`,
    );
    assert(
      checker.getPropertiesOfType(type).length > 0,
      `Unsupported open type at ${hint}`,
    );
    if (compiled.has(type))
      return { kind: "model", ref: compiled.get(type), nullable };
    const name =
      config.modelNames[names.get(type) || hint] || names.get(type) || hint;
    assert.match(name, /^[A-Za-z][A-Za-z0-9]*$/, `Invalid model name: ${name}`);
    assert(!models.has(name), `Conflicting generated model: ${name}`);
    compiled.set(type, name);
    const model = {
      name,
      input,
      description:
        spec.components.schemas[name]?.description ||
        spec.components.schemas[name]?.title ||
        name,
      properties: [],
    };
    models.set(name, model);
    model.properties = checker.getPropertiesOfType(type).map((symbol) => ({
      name: symbol.name,
      required: !(symbol.flags & ts.SymbolFlags.Optional),
      description: prose(symbol),
      ...compile(at(symbol), name + pascal(symbol.name), input),
    }));
    return { kind: "model", ref: name, nullable };
  }
  for (const name of config.inputs)
    compile(property(schemas, name), name, true);
  compile(property(schemas, "Customer"), "Customer", false);
  const methods = [];
  for (const [name, id] of Object.entries(config.methods)) {
    const operation = operations.get(id);
    const type = property(operationTypes, id);
    const parameters = property(type, "parameters");
    const query = property(parameters, "query");
    const hasQuery = !(
      checker.getNonNullableType(query).flags & ts.TypeFlags.Never
    );
    const method = { name, id, ...operation };
    if (hasQuery)
      method.query = compile(query, "Customer" + pascal(name) + "Params", true);
    if (name === "sendEditLinkByEmail")
      method.body = compile(
        property(
          property(property(type, "requestBody"), "content"),
          "application/json",
        ),
        "CustomerEditLinkEmailInput",
        true,
      );
    const responses = property(type, "responses");
    const success = checker
      .getPropertiesOfType(responses)
      .filter((symbol) => /^2\d\d$/.test(symbol.name));
    assert(success.length > 0, `Missing success response for ${id}`);
    assert(
      success.every(
        (response) =>
          property(property(at(response), "content"), "application/json") ===
          property(property(at(success[0]), "content"), "application/json"),
      ),
      `Bind the response variants for ${id}`,
    );
    method.response = compile(
      property(property(at(success[0]), "content"), "application/json"),
      name === "validateTaxInfo"
        ? "TaxInfoValidation"
        : name === "sendEditLinkByEmail"
          ? "CustomerEditLinkEmailResult"
          : "Customer" + pascal(name) + "Result",
      false,
    );
    const body = operation.requestBody?.$ref
      ? operation.requestBody.$ref
          .slice(2)
          .split("/")
          .reduce((target, part) => target[part], spec)
      : operation.requestBody;
    method.bodyRequired = body && body.required !== false;
    method.responseDescription =
      operation.responses[success[0].name].description;
    method.parameters = [
      ...(spec.paths[operation.path].parameters || []),
      ...(operation.parameters || []),
    ].map((parameter) =>
      parameter.$ref
        ? parameter.$ref
            .slice(2)
            .split("/")
            .reduce((target, part) => target[part], spec)
        : parameter,
    );
    methods.push(method);
  }
  return { models: [...models.values()], methods };
}
