import { pascal } from "./compiler.mjs";
import { banner, doc, text } from "./render-models.mjs";

const convenience = [
  ["createNational", "CustomerNationalCreateInput", "create"],
  ["createForeign", "CustomerForeignCreateInput", "create"],
  ["createGeneric", "CustomerGenericCreateInput", "create"],
  ["createWithEditLink", "CustomerCreateWithEditLinkInput", "create"],
  ["updateWithEditLink", "CustomerUpdateWithEditLinkInput", "update"],
];

function methodProse(method, parameters) {
  return (
    doc(
      [method.summary, method.description].filter(Boolean).join("\n\n"),
      "        ",
    ) +
    parameters
      .map(
        ([name, type, description]) =>
          `        /// <param name="${name}">${text(description).replaceAll("&", "&amp;").replaceAll("<", "&lt;").replaceAll(">", "&gt;").replaceAll("\n", " ")}</param>\n`,
      )
      .join("") +
    `        /// <returns>${text(
      method.responseDescription || "Respuesta de la API.",
    )
      .replaceAll("&", "&amp;")
      .replaceAll("<", "&lt;")
      .replaceAll(
        ">",
        "&gt;",
      )}</returns>\n        /// <see href="https://docs.facturapi.io/api/#operation/${method.id}"/>\n`
  );
}

export function renderResources(contract) {
  const outputs = new Map();
  let content =
    banner +
    "using System;\nusing System.Collections.Generic;\nusing System.Net.Http;\nusing System.Text;\nusing System.Threading;\nusing System.Threading.Tasks;\nusing Newtonsoft.Json;\n\nnamespace Facturapi.Wrappers\n{\n" +
    doc("Operaciones de clientes.", "    ") +
    "    public class CustomerWrapper : BaseWrapper, ICustomerWrapper\n    {\n        internal CustomerWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient) {}\n\n";
  let interfaces =
    banner +
    "using System.Collections.Generic;\nusing System.Threading;\nusing System.Threading.Tasks;\n\nnamespace Facturapi.Wrappers\n{\n" +
    doc("Operaciones de clientes para inyección de dependencias.", "    ") +
    "    public interface ICustomerWrapper\n    {\n";
  let router =
    banner +
    "using System;\nusing System.Collections.Generic;\n\nnamespace Facturapi\n{\n    internal static partial class Router\n    {\n";
  for (const method of contract.methods) {
    const id = method.parameters.find((parameter) => parameter.in === "path");
    const hasBody = Boolean(method.requestBody);
    const paramsName = method.name === "list" ? "query" : "queryParams";
    const parameters = [
      ...(id ? [["id", "string", id.description]] : []),
      ...(hasBody
        ? [
            [
              "data",
              "Dictionary<string, object>",
              `Datos de la solicitud. ${method.name === "sendEditLinkByEmail" ? "Admite CustomerEditLinkEmailInput." : "Admite los modelos Customer*Input."}`,
            ],
          ]
        : []),
      ...(method.query
        ? [
            [
              paramsName,
              "Dictionary<string, object>",
              `Parámetros de consulta. Admite ${method.query.ref}.`,
            ],
          ]
        : []),
      [
        "cancellationToken",
        "CancellationToken",
        "Permite cancelar la solicitud.",
      ],
    ];
    const signature = parameters
      .map(
        ([name, type]) =>
          `${type} ${name}${name === paramsName ? " = null" : name === "cancellationToken" ? " = default" : ""}`,
      )
      .join(", ");
    const returnType =
      method.name === "sendEditLinkByEmail"
        ? "Task"
        : `Task<${method.name === "list" ? "SearchResult<Customer>" : method.response.ref}>`;
    const declaration = `${returnType} ${pascal(method.name)}Async(${signature})`;
    interfaces +=
      methodProse(method, parameters) + `        ${declaration};\n\n`;
    content +=
      methodProse(method, parameters) +
      `        public async ${declaration}\n        {\n`;
    const routeName = {
      list: "ListCustomers",
      retrieve: "RetrieveCustomer",
      create: "CreateCustomer",
      update: "UpdateCustomer",
      delete: "DeleteCustomer",
      validateTaxInfo: "ValidateCustomerTaxInfo",
      sendEditLinkByEmail: "SendEditLinkByEmail",
    }[method.name];
    router += `        public static string ${routeName}(${[...(id ? ["string id"] : []), ...(method.query ? ["Dictionary<string, object> query = null"] : [])].join(", ")})\n        {\n            return UriWithQuery($"${method.path.slice(1).replace(/\{[^}]+\}/g, "{Uri.EscapeDataString(id)}")}"${method.query ? ", query" : ""});\n        }\n\n`;
    const route = `Router.${routeName}(${[...(id ? ["id"] : []), ...(method.query ? [paramsName] : [])].join(", ")})`;
    if (hasBody)
      content +=
        '            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))\n';
    content += `            using (var response = await client.${pascal(method.verb)}Async(${route}${hasBody ? ", content" : ""}, cancellationToken).ConfigureAwait(false))\n            {\n                await ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);\n`;
    if (method.name !== "sendEditLinkByEmail")
      content += `                return JsonConvert.DeserializeObject<${method.name === "list" ? "SearchResult<Customer>" : method.response.ref}>(await response.Content.ReadAsStringAsync().ConfigureAwait(false), jsonSettings);\n`;
    content += "            }\n        }\n\n";
  }
  for (const [name, model, operation] of convenience) {
    const method = contract.methods.find((method) => method.name === operation);
    const parameters = [
      ...(operation === "update" ? [["id", "string", "ID del cliente."]] : []),
      [
        "data",
        model,
        contract.models.find((item) => item.name === model).description,
      ],
      [
        "cancellationToken",
        "CancellationToken",
        "Permite cancelar la solicitud.",
      ],
    ];
    const declaration = `Task<Customer> ${pascal(name)}Async(${parameters.map(([name, type]) => `${type} ${name}${name === "cancellationToken" ? " = default" : ""}`).join(", ")})`;
    interfaces +=
      methodProse(method, parameters) + `        ${declaration};\n\n`;
    content +=
      methodProse(method, parameters) +
      `        public ${declaration}\n        {\n            return ${pascal(operation)}Async(${operation === "update" ? "id, " : ""}data, ${name.endsWith("WithEditLink") ? 'new Dictionary<string, object> { ["createEditLink"] = true }' : "null"}, cancellationToken);\n        }\n\n`;
  }
  outputs.set("Wrappers/CustomerWrapper.cs", content + "    }\n}\n");
  outputs.set("Wrappers/ICustomerWrapper.cs", interfaces + "    }\n}\n");
  outputs.set("Router/CustomerRouter.cs", router + "    }\n}\n");
  return outputs;
}
