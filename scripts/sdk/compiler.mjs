import assert from "node:assert/strict";
import openapiTS, { astToString } from "openapi-typescript";
import ts from "typescript";

export const pascal = (value) =>
  value
    .replace(/[^a-zA-Z0-9_]/g, "_")
    .replace(/(^|_)([a-zA-Z0-9])/g, (_, prefix, letter) => letter.toUpperCase())
    .replace(/^[0-9]/, (digit) => "Value" + digit);
export const camel = (value) =>
  pascal(value).replace(/^./, (letter) => letter.toLowerCase());

// Let openapi-typescript and the TypeScript checker resolve references,
// intersections and nullability. Rendering never reinterprets JSON Schema.
export async function compileContract(spec, config) {
  const operations = new Map();
  for (const [path, item] of Object.entries(spec.paths)) {
    for (const [verb, operation] of Object.entries(item)) {
      if (!operation.operationId) continue;
      assert(
        !operations.has(operation.operationId),
        `Duplicate operation: ${operation.operationId}`,
      );
      operations.set(operation.operationId, { ...operation, path, verb });
    }
  }
  const filename = "/contract.ts";
  const content =
    "type Integer = number & { readonly __integer: unique symbol }; type Binary = string & { readonly __binary: unique symbol };\n" +
    astToString(
      await openapiTS(spec, {
        defaultNonNullable: false,
        emptyObjectsUnknown: true,
        transform(schema) {
          const type =
            schema.format === "binary"
              ? ts.factory.createTypeReferenceNode("Binary")
              : schema.format === "date-time"
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
  const named = new Map();
  const names = new Map();
  for (const symbol of checker.getPropertiesOfType(schemas)) {
    // Prefer deliberate SDK aliases over structurally identical schema aliases.
    if (
      !names.has(checker.getNonNullableType(at(symbol))) ||
      config.modelNames[symbol.name]
    )
      names.set(
        checker.getNonNullableType(at(symbol)),
        config.modelNames[symbol.name] || symbol.name,
      );
    named.set(symbol.name, at(symbol));
  }
  const compiled = { input: new Map(), output: new Map() };
  const prose = (symbol) =>
    [
      ts.displayPartsToString(symbol.getDocumentationComment(checker)),
      ...symbol
        .getJsDocTags(checker)
        .filter((tag) => ["description", "deprecated"].includes(tag.name))
        .map((tag) => ts.displayPartsToString(tag.text)),
    ]
      .filter(Boolean)
      .join("\n")
      .replaceAll("(enum property replaced by openapi-typescript)", "")
      .replaceAll("discriminator enum property added by openapi-typescript", "")
      .trim();
  const merge = (fields) => {
    const nullable = fields.some(
      (field) => field.nullable || field.kind === "null",
    );
    fields = fields
      .flatMap((field) => (field.kind === "union" ? field.variants : [field]))
      .filter((field) => !["null", "never"].includes(field.kind));
    if (!fields.length) return { kind: "null", nullable: true };
    if (
      fields.every((field) => ["string", "inputDate"].includes(field.kind)) &&
      fields.some((field) => field.kind === "inputDate")
    )
      return { kind: "inputDate", nullable };
    if (
      fields.every((field) => field.kind === fields[0].kind) &&
      [
        "string",
        "integer",
        "number",
        "boolean",
        "date",
        "inputDate",
        "binary",
      ].includes(fields[0].kind)
    )
      return {
        kind: fields[0].kind,
        nullable,
        ...(fields.every((field) => field.values)
          ? { values: [...new Set(fields.flatMap((field) => field.values))] }
          : {}),
      };
    if (fields.every((field) => field.kind === "array"))
      return {
        kind: "array",
        nullable,
        item: merge(fields.map((field) => field.item)),
      };
    const variants = [
      ...new Map(
        fields.map((field) => [
          JSON.stringify({ ...field, nullable: false }),
          field,
        ]),
      ).values(),
    ];
    if (variants.length === 1) return { ...variants[0], nullable };
    return {
      kind: "union",
      nullable,
      variants: variants.map((field) => ({ ...field, nullable: false })),
    };
  };
  function compile(type, hint, input) {
    if (
      type.flags &
      (ts.TypeFlags.Any | ts.TypeFlags.Unknown | ts.TypeFlags.NonPrimitive)
    )
      return { kind: "any", nullable: true };
    if (type.flags & (ts.TypeFlags.Null | ts.TypeFlags.Undefined))
      return { kind: "null", nullable: true };
    if (checker.typeToString(type) === "never") return { kind: "never" };
    const nullable =
      type.isUnion() &&
      type.types.some((item) => item.flags & ts.TypeFlags.Null);
    type = checker.getNonNullableType(type);
    if (type.aliasSymbol?.name === "Integer")
      return { kind: "integer", nullable };
    if (
      type.symbol?.name === "Date" ||
      (type.isIntersection() &&
        type.types.some((part) => part.symbol?.name === "Date"))
    )
      return { kind: input ? "inputDate" : "date", nullable };
    if (type.aliasSymbol?.name === "Binary")
      return { kind: "binary", nullable };
    if (
      type.isIntersection() &&
      type.types.some((part) => part.flags & ts.TypeFlags.NumberLike)
    )
      return {
        kind: checker.getPropertyOfType(type, "__integer")
          ? "integer"
          : "number",
        nullable,
      };
    if (
      type.isIntersection() &&
      type.types.some((part) => part.flags & ts.TypeFlags.StringLike)
    )
      return {
        kind: checker.getPropertyOfType(type, "__binary") ? "binary" : "string",
        nullable,
      };
    if (
      type.flags & ts.TypeFlags.BooleanLike ||
      (type.isUnion() &&
        type.types.every((item) => item.flags & ts.TypeFlags.BooleanLike))
    )
      return { kind: "boolean", nullable };
    if (type.flags & ts.TypeFlags.NumberLike)
      return { kind: "number", nullable };
    if (
      type.flags & ts.TypeFlags.StringLike ||
      (type.isUnion() &&
        type.types.every((item) => item.flags & ts.TypeFlags.StringLike))
    )
      return {
        kind: "string",
        nullable,
        ...(type.isStringLiteral() ||
        (type.isUnion() && type.types.every((item) => item.isStringLiteral()))
          ? {
              values: (type.isUnion() ? type.types : [type]).map(
                (item) => item.value,
              ),
            }
          : {}),
      };
    if (
      type.flags &
      (ts.TypeFlags.Any | ts.TypeFlags.Unknown | ts.TypeFlags.NonPrimitive)
    )
      return { kind: "any", nullable };
    if (type.flags & ts.TypeFlags.Never)
      return { kind: nullable ? "null" : "never", nullable };
    if (checker.isArrayType(type) || checker.isTupleType(type))
      return {
        kind: "array",
        nullable,
        item: compile(
          checker.getIndexTypeOfType(type, ts.IndexKind.Number),
          hint + "Item",
          input,
        ),
      };
    const cache = compiled[input ? "input" : "output"];
    if (cache.has(type)) return { ...cache.get(type), nullable };
    const alternatives = (type.isUnion() ? type.types : [type]).filter(
      (variant) => checker.typeToString(variant) !== "never",
    );
    // C# has no structural object unions. Expose their documented superset;
    // named input variants below retain their own required constructor fields.
    if (
      type.isUnion() &&
      !alternatives.every(
        (variant) =>
          checker.getPropertiesOfType(variant).length > 0 &&
          !checker.isArrayType(variant) &&
          !(
            variant.flags &
            (ts.TypeFlags.StringLike |
              ts.TypeFlags.NumberLike |
              ts.TypeFlags.BooleanLike)
          ),
      )
    ) {
      return merge(
        alternatives.map((variant, index) =>
          compile(variant, hint + "Variant" + (index + 1), input),
        ),
      );
    }
    const properties = new Map();
    for (const variant of alternatives)
      for (const symbol of checker.getPropertiesOfType(variant)) {
        if (
          input &&
          symbol.declarations?.some((declaration) =>
            declaration.modifiers?.some(
              (modifier) => modifier.kind === ts.SyntaxKind.ReadonlyKeyword,
            ),
          )
        )
          continue;
        if (!properties.has(symbol.name)) properties.set(symbol.name, []);
        properties.get(symbol.name).push(symbol);
      }
    const additional = checker.getIndexTypeOfType(type, ts.IndexKind.String);
    if (!properties.size)
      return additional
        ? {
            kind: "map",
            nullable,
            item: compile(additional, hint + "Value", input),
          }
        : { kind: "map", nullable, item: { kind: "any" } };
    const base =
      config.modelNames[names.get(type) || hint] || names.get(type) || hint;
    const name =
      input && !base.includes("Input") && !base.endsWith("Params")
        ? base + "Input"
        : base;
    assert.match(name, /^[A-Za-z][A-Za-z0-9]*$/, `Invalid model name: ${name}`);
    if (
      !input &&
      properties.has("data") &&
      (properties.has("total_results") || properties.has("page"))
    ) {
      const data = compile(at(properties.get("data")[0]), name + "Data", false);
      assert.equal(data.kind, "array", `Expected paginated data for ${name}`);
      const result = { kind: "pagination", item: data.item };
      cache.set(type, result);
      return { ...result, nullable };
    }
    const ref = { kind: "model", ref: name };
    cache.set(type, ref);
    const model = models.get(name) || {
      name,
      input,
      description:
        spec.components.schemas[names.get(type)]?.description ||
        spec.components.schemas[names.get(type)]?.title ||
        name,
      properties: [],
    };
    assert.equal(
      model.input,
      input,
      `Separate input/output model names for ${name}`,
    );
    models.set(name, model);
    const fields = [...properties].map(([key, symbols]) => ({
      ...merge(
        symbols.map((symbol) => compile(at(symbol), name + pascal(key), input)),
      ),
      name: key,
      required:
        symbols.length === alternatives.length &&
        symbols.every((symbol) => !(symbol.flags & ts.SymbolFlags.Optional)),
      description: [...new Set(symbols.map(prose).filter(Boolean))].join("\n"),
    }));
    if (!input) {
      model.variantMappings ||= {};
      for (const discriminator of fields.filter(
        (field) => field.values?.length === 1,
      )) {
        model.variantMappings[discriminator.name] ||= {};
        model.variantMappings[discriminator.name][discriminator.values[0]] ||=
          {};
        for (const field of fields.filter(
          (field) =>
            field.name !== discriminator.name && field.kind !== "union",
        ))
          model.variantMappings[discriminator.name][discriminator.values[0]][
            field.name
          ] = field;
      }
    }
    for (const field of fields) {
      const previous = model.properties.find(
        (property) => property.name === field.name,
      );
      if (previous) {
        const type = merge([
          Object.fromEntries(
            Object.entries(previous).filter(
              ([key]) => !["name", "required", "description"].includes(key),
            ),
          ),
          Object.fromEntries(
            Object.entries(field).filter(
              ([key]) => !["name", "required", "description"].includes(key),
            ),
          ),
        ]);
        Object.assign(previous, type, {
          required: previous.required && field.required,
        });
      } else model.properties.push(field);
    }
    if (additional)
      model.additional = compile(additional, name + "Value", input);
    return { ...ref, nullable };
  }
  // Compile all explicitly named input variants, including complements.
  for (const [name, type] of named)
    if (name.endsWith("Input") || (config.inputs || []).includes(name))
      compile(type, name, true);
  for (const [name, binding] of Object.entries(config.variants || {})) {
    const root = checker.getNonNullableType(named.get(binding.schema));
    assert(root.isUnion(), `Expected an input union for ${name}`);
    const matches = root.types.filter((variant) =>
      Object.entries(binding.match).every(([key, expected]) => {
        const symbol = checker.getPropertyOfType(variant, key);
        if (!symbol) return false;
        const type = checker.getNonNullableType(at(symbol));
        const values = (type.isUnion() ? type.types : [type]).map((type) =>
          type.isStringLiteral() ? type.value : undefined,
        );
        return (
          values.length === (Array.isArray(expected) ? expected.length : 1) &&
          values.every((value) =>
            (Array.isArray(expected) ? expected : [expected]).includes(value),
          )
        );
      }),
    );
    assert.equal(
      matches.length,
      1,
      `Select exactly one public input variant for ${name}`,
    );
    const variant = compile(matches[0], name, true);
    assert.equal(variant.kind, "model");
    const model = models.get(variant.ref);
    model.description =
      spec.components.schemas[binding.schema].description || name;
    if (binding.parent) model.parent = compile(root, binding.schema, true).ref;
  }
  const methods = [];
  const used = new Set();
  const dereference = (value) =>
    value?.$ref
      ? value.$ref
          .slice(2)
          .split("/")
          .reduce(
            (target, part) =>
              target[part.replaceAll("~1", "/").replaceAll("~0", "~")],
            spec,
          )
      : value;
  for (const [resource, definition] of Object.entries(config.resources))
    for (const [name, binding] of Object.entries(definition.methods)) {
      if (binding.alias) {
        methods.push({ resource, name, ...binding });
        continue;
      }
      assert(
        operations.has(binding.operation),
        `Unknown public operation: ${binding.operation}`,
      );
      used.add(binding.operation);
      const operation = operations.get(binding.operation);
      const type = property(operationTypes, binding.operation);
      const method = {
        resource,
        name,
        ...operation,
        ...binding,
        httpPath: operation.path,
        fixedPath: binding.path || {},
      };
      method.parameters = [
        ...(spec.paths[operation.path].parameters || []),
        ...(operation.parameters || []),
      ].map(dereference);
      for (const [key, value] of Object.entries(binding.path || {})) {
        const parameter = method.parameters.find(
          (parameter) => parameter.in === "path" && parameter.name === key,
        );
        assert(
          parameter,
          `Unknown fixed path parameter for ${method.name}: ${key}`,
        );
        assert(
          !parameter.schema.enum || parameter.schema.enum.includes(value),
          `Unsupported fixed format for ${method.name}: ${value}`,
        );
      }
      const parameters = property(type, "parameters");
      const query = property(parameters, "query");
      if (!(checker.getNonNullableType(query).flags & ts.TypeFlags.Never))
        method.query = compile(
          binding.querySchema ? named.get(binding.querySchema) : query,
          resource + name.replace(/Async$/, "") + "Params",
          true,
        );
      const body = dereference(operation.requestBody);
      if (body) {
        const content = property(property(type, "requestBody"), "content");
        const mediaType = Object.keys(body.content)[0];
        method.mediaType = mediaType;
        method.body = compile(
          binding.bodySchema
            ? named.get(binding.bodySchema)
            : property(content, mediaType),
          pascal(binding.operation) + "Input",
          true,
        );
        method.bodyRequired = body.required !== false;
      }
      const responses = property(type, "responses");
      const success = checker
        .getPropertiesOfType(responses)
        .filter((symbol) => /^2\d\d$/.test(symbol.name));
      assert(
        success.length,
        `Missing success response for ${binding.operation}`,
      );
      method.responseDescription = [
        ...new Set(
          success.map(
            (response) =>
              dereference(operation.responses[response.name]).description,
          ),
        ),
      ].join("\n");
      const resultTypes = success.map((response) => {
        const resolved = dereference(operation.responses[response.name]);
        if (!resolved.content) return { kind: "void" };
        const mediaType =
          Object.keys(resolved.content).find(
            (type) => type === "application/json",
          ) || Object.keys(resolved.content)[0];
        return compile(
          property(property(at(response), "content"), mediaType),
          pascal(binding.operation) + "Response",
          false,
        );
      });
      method.response = binding.responseSchema
        ? compile(
            named.get(binding.responseSchema),
            binding.responseSchema,
            false,
          )
        : merge(resultTypes);
      methods.push(method);
    }
  assert.deepEqual(
    [...operations.keys()].sort(),
    [...used].sort(),
    "Every public operation must have an SDK method binding.",
  );
  for (const name of config.outputModels || [])
    compile(named.get(name), name, false);
  // Preserve one generic pagination contract shared by every collection.
  if (named.has("SearchResult")) {
    const result = compile(named.get("SearchResult"), "SearchResult", false);
    models.get(result.ref).generic = true;
    models.get(result.ref).properties.push({
      name: "data",
      required: true,
      kind: "array",
      item: { kind: "parameter", ref: "T" },
      description: "Elementos de la página.",
    });
  }
  const families = [];
  for (const [schemaName, schema] of Object.entries(spec.components.schemas)) {
    const alternatives = schema.oneOf || schema.anyOf;
    if (
      !(schema.discriminator || schemaName === "CustomerCreateInput") ||
      !alternatives?.every((alternative) => alternative.$ref)
    )
      continue;
    const parent = compiled.input.get(
      checker.getNonNullableType(named.get(schemaName)),
    );
    const members = alternatives.map((alternative) =>
      compiled.input.get(
        checker.getNonNullableType(
          named.get(alternative.$ref.split("/").at(-1)),
        ),
      ),
    );
    if (
      parent?.kind === "model" &&
      members.every((member) => member?.kind === "model")
    )
      families.push({
        parent: parent.ref,
        members: members.map((member) => member.ref),
      });
  }
  families.sort((left, right) => right.members.length - left.members.length);
  const replaced = new Map();
  for (const family of families) {
    if (family.members.every((name) => !models.get(name).parent)) {
      for (const name of family.members)
        models.get(name).parent = family.parent;
    } else replaced.set(family.parent, family.members);
  }
  function replaceFamilies(field) {
    if (field.kind === "model" && replaced.has(field.ref))
      return {
        kind: "union",
        nullable: field.nullable,
        variants: replaced
          .get(field.ref)
          .map((ref) => ({ kind: "model", ref })),
      };
    if (field.item) return { ...field, item: replaceFamilies(field.item) };
    if (field.variants)
      return { ...field, variants: field.variants.map(replaceFamilies) };
    return field;
  }
  for (const model of models.values())
    model.properties = model.properties.map((field) => ({
      ...replaceFamilies(field),
      name: field.name,
      required: field.required,
      description: field.description,
    }));
  for (const method of methods)
    if (method.body) method.body = replaceFamilies(method.body);
  for (const name of replaced.keys()) models.delete(name);

  return { models: [...models.values()], methods, operationCount: used.size };
}
