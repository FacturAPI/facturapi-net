import assert from "node:assert/strict";
import { pascal, camel } from "./compiler.mjs";
import { banner, doc, text, xml, fieldType } from "./render-models.mjs";

function parseSignature(signature) {
  return signature.split(/,(?![^<]*>)/).map((argument) => {
    const match = argument.trim().match(/^(.+?)\s+(\w+)(?:\s*=\s*(.+))?$/);
    assert(match, `Invalid compatibility argument: ${argument}`);
    return { type: match[1], name: match[2], default: match[3] };
  });
}
function documentation(method, argumentsList) {
  return (
    doc(
      [method.summary, method.description].filter(Boolean).join("\n\n"),
      "        ",
    ) +
    argumentsList
      .map(
        (argument) =>
          `        /// <param name="${argument.name}">${xml(text(argument.description || (argument.name === "cancellationToken" ? "Permite cancelar la solicitud." : argument.name))).replaceAll("\n", " ")}</param>\n`,
      )
      .join("") +
    `        /// <returns>${xml(text(method.response.kind === "binary" ? "Stream en memoria, posicionado al inicio. El consumidor debe disponerlo al terminar." : method.responseDescription || "Respuesta de la API.")).replaceAll("\n", " ")}</returns>\n        /// <see href="https://docs.facturapi.io/api/#operation/${method.operation}"/>\n`
  );
}
export function renderResources(contract, config) {
  const outputs = new Map();
  for (const resource of Object.keys(config.resources)) {
    const wrapper = resource + "Wrapper";
    let content =
      banner +
      "#nullable enable\nusing System;\nusing System.IO;\nusing System.Collections.Generic;\nusing System.Net.Http;\nusing System.Threading;\nusing System.Threading.Tasks;\n#nullable disable\nusing JsonObject = System.Collections.Generic.Dictionary<string, object>;\n#nullable enable\n\nnamespace Facturapi.Wrappers\n{\n" +
      doc(`Operaciones de ${resource}.`, "    ") +
      `    public partial class ${wrapper} : BaseWrapper, I${wrapper}\n    {\n        internal ${wrapper}(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient) {}\n\n`;
    let interfaces =
      banner +
      "#nullable enable\nusing System;\nusing System.IO;\nusing System.Collections.Generic;\nusing System.Threading;\nusing System.Threading.Tasks;\n#nullable disable\nusing JsonObject = System.Collections.Generic.Dictionary<string, object>;\n#nullable enable\n\nnamespace Facturapi.Wrappers\n{\n" +
      doc(
        `Operaciones de ${resource} para inyección de dependencias.`,
        "    ",
      ) +
      `    public partial interface I${wrapper}\n    {\n`;
    for (const method of contract.methods.filter(
      (method) => method.resource === resource && !method.alias,
    )) {
      const pathParameters = method.parameters.filter(
        (parameter) =>
          parameter.in === "path" &&
          !(parameter.name in (method.fixedPath || {})),
      );
      let argumentsList = method.signature
        ? parseSignature(method.signature)
        : [
            ...pathParameters.map((parameter, index) => ({
              name:
                pathParameters.length === 1 && /_id$/.test(parameter.name)
                  ? "id"
                  : camel(parameter.name),
              type: "string",
              description: parameter.description,
            })),
            ...(method.body
              ? [
                  {
                    name: "data",
                    type: "Dictionary<string, object>",
                    ...(!method.bodyRequired ? { default: "null" } : {}),
                  },
                ]
              : []),
            ...(method.query
              ? [
                  {
                    name: "query",
                    type: "Dictionary<string, object>",
                    default: "null",
                  },
                ]
              : []),
            {
              name: "cancellationToken",
              type: "CancellationToken",
              default: "default",
            },
          ];
      const pathArguments = argumentsList
        .filter((argument) => argument.type === "string")
        .slice(0, pathParameters.length);
      assert.equal(
        pathArguments.length,
        pathParameters.length,
        `Bind path arguments for ${method.name}`,
      );
      let path = method.httpPath;
      for (const [name, value] of Object.entries(method.fixedPath || {}))
        path = path.replace(`{${name}}`, value);
      pathParameters.forEach((parameter, index) => {
        path = path.replace(
          `{${parameter.name}}`,
          `{Uri.EscapeDataString(${pathArguments[index].name})}`,
        );
        pathArguments[index].description = parameter.description;
      });
      if (method.multipart)
        for (const [field, name] of Object.entries(method.multipart)) {
          const argument = argumentsList.find(
            (argument) => argument.name === name,
          );
          argument.description =
            contract.models
              .find((model) => model.name === method.body.ref)
              ?.properties.find((property) => property.name === field)
              ?.description || name;
          if (argument.type === "Stream")
            argument.description +=
              " Usa un Stream; se dispone al finalizar la solicitud.";
        }
      const bodyArgument =
        method.body && !method.multipart && !method.bodyFields
          ? argumentsList.find((argument) =>
              argument.type.startsWith("Dictionary<"),
            )
          : null;
      const queryArgument =
        method.query && !method.queryFields
          ? argumentsList.find(
              (argument) =>
                argument.type.startsWith("Dictionary<") &&
                argument !== bodyArgument,
            )
          : null;
      if (bodyArgument)
        bodyArgument.description = `Datos de la solicitud. Admite ${fieldType(method.body, false)}.`;
      if (queryArgument)
        queryArgument.description = `Parámetros de consulta. Admite ${fieldType(method.query, false)}.`;
      const resultType = method.resultProperty
        ? "bool"
        : fieldType(method.response, false).replace(/\?$/, "");
      method.resultType = resultType;
      method.argumentsList = argumentsList;
      method.pathArguments = pathArguments;
      const declaration = (args) =>
        `Task<${resultType}> ${method.name}(${args.map((argument) => `${argument.type.replace("Dictionary<string, object>", "JsonObject")}${argument.default === "null" && !argument.type.endsWith("?") ? "?" : ""} ${argument.name}${argument.default ? " = " + argument.default : ""}`).join(", ")})`;
      const emit = (args, typed = false) => {
        const query = method.queryFields
          ? `new Dictionary<string, object?> { ${Object.entries(
              method.queryFields,
            )
              .map(([key, name]) => `[${JSON.stringify(key)}] = ${name}`)
              .join(", ")} }`
          : queryArgument
            ? queryArgument.name
            : "null";
        const body = method.bodyFields
          ? `new Dictionary<string, object?> { ${Object.entries(
              method.bodyFields,
            )
              .map(([key, name]) => `[${JSON.stringify(key)}] = ${name}`)
              .join(", ")} }`
          : bodyArgument
            ? bodyArgument.name
            : "null";
        const prose = documentation(method, args);
        interfaces += prose + `        ${declaration(args)};\n\n`;
        content +=
          prose +
          `        public ${method.multipart || method.resultProperty ? "async " : ""}${declaration(args)}\n        {\n`;
        if (method.bodyFlags) {
          content +=
            `            var requestBody = new Dictionary<string, object?>(${bodyArgument.name});\n` +
            Object.entries(method.bodyFlags)
              .map(
                ([key, value]) =>
                  `            requestBody[${JSON.stringify(key)}] = ${JSON.stringify(value)};\n`,
              )
              .join("");
        }
        if (method.multipart) {
          content +=
            "            using (var form = new MultipartFormDataContent())\n            {\n";
          for (const [field, argument] of Object.entries(method.multipart))
            content +=
              field === "password"
                ? `                form.Add(new StringContent(${argument}), "password");\n`
                : `                form.Add(new StreamContent(${argument}), ${JSON.stringify(field)}, ${JSON.stringify(field === "file" ? "logo.jpg" : field + "." + field)});\n`;
          content += `                return await RequestMultipartAsync<${resultType}>(HttpMethod.${pascal(method.verb)}, $"${path.slice(1)}", form, cancellationToken).ConfigureAwait(false);\n            }\n`;
        } else if (method.resultProperty)
          content += `            return (await RequestAsync<${fieldType(method.response, false)}>(HttpMethod.${pascal(method.verb)}, $"${path.slice(1)}", ${query}, ${method.bodyFlags ? "requestBody" : body}, ${Boolean(method.body).toString().toLowerCase()}, cancellationToken).ConfigureAwait(false)).${pascal(method.resultProperty)} == true;\n`;
        else
          content += `            return RequestAsync<${resultType}>(HttpMethod.${pascal(method.verb)}, $"${path.slice(1)}", ${query}, ${method.bodyFlags ? "requestBody" : body}, ${Boolean(method.body).toString().toLowerCase()}, cancellationToken);\n`;
        content += "        }\n\n";
      };
      emit(argumentsList);
      // A single strongly typed overload preserves dictionary compatibility.
      // Query-only overloads require their query argument, avoiding no-arg ambiguity.
      if (
        !method.multipart &&
        !method.bodyFields &&
        !method.queryFields &&
        ((bodyArgument && method.body.kind === "model") ||
          (queryArgument && method.query.kind === "model"))
      ) {
        const typed = argumentsList.map((argument) =>
          argument === bodyArgument && method.body.kind === "model"
            ? {
                ...argument,
                type: fieldType(method.body, false),
                default: undefined,
              }
            : argument === queryArgument && method.query.kind === "model"
              ? {
                  ...argument,
                  type:
                    fieldType(method.query, false) +
                    (queryArgument.default === "null" ? "?" : ""),
                  ...(!bodyArgument ? { default: undefined } : {}),
                }
              : argument,
        );
        emit(typed, true);
      }
    }
    for (const alias of contract.methods.filter(
      (method) => method.resource === resource && method.alias,
    )) {
      const target = contract.methods.find(
        (method) => method.resource === resource && method.name === alias.alias,
      );
      const args = parseSignature(alias.signature);
      const prose = documentation(target, args);
      const declaration = `Task<${target.resultType}> ${alias.name}(${args.map((argument) => `${argument.type.replace("Dictionary<string, object>", "JsonObject")}${argument.default === "null" ? "?" : ""} ${argument.name}${argument.default ? " = " + argument.default : ""}`).join(", ")})`;
      interfaces +=
        prose +
        `        [Obsolete("Use ${alias.alias} instead.")]\n        ${declaration};\n\n`;
      content +=
        prose +
        `        [Obsolete("Use ${alias.alias} instead.")]\n        public ${declaration} => ${alias.alias}(${args.map((argument) => argument.name).join(", ")});\n\n`;
    }
    for (const [name, binding] of Object.entries(
      config.convenience?.[resource] || {},
    )) {
      const target = contract.methods.find(
        (method) =>
          method.resource === resource && method.name === binding.method,
      );
      const args = [
        ...target.pathArguments,
        {
          name: "data",
          type: binding.schema,
          description: contract.models.find(
            (model) => model.name === binding.schema,
          ).description,
        },
        {
          name: "cancellationToken",
          type: "CancellationToken",
          default: "default",
        },
      ];
      const declaration = `Task<${target.resultType}> ${name}(${args.map((argument) => `${argument.type} ${argument.name}${argument.default ? " = " + argument.default : ""}`).join(", ")})`;
      const prose = documentation(target, args);
      interfaces += prose + `        ${declaration};\n\n`;
      content +=
        prose +
        `        public ${declaration}\n        {\n            return ${target.name}(${target.pathArguments.map((argument) => argument.name + ", ").join("")}data${
          target.query
            ? ", " +
              (binding.query
                ? `new Dictionary<string, object?> { ${Object.entries(
                    binding.query,
                  )
                    .map(
                      ([key, value]) => `[${JSON.stringify(key)}] = ${value}`,
                    )
                    .join(", ")} }`
                : "null")
            : ""
        }, cancellationToken);\n        }\n\n`;
    }
    outputs.set(`Wrappers/${wrapper}.cs`, content + "    }\n}\n");
    outputs.set(`Wrappers/I${wrapper}.cs`, interfaces + "    }\n}\n");
  }

  return outputs;
}
