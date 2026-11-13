import test from "node:test";
import assert from "node:assert/strict";
import { readFile } from "node:fs/promises";
import { compileContract } from "./compiler.mjs";
import { renderModels } from "./render-models.mjs";

const config = JSON.parse(
  await readFile(new URL("./config.json", import.meta.url)),
);
function fixture() {
  return {
    openapi: "3.1.0",
    info: { title: "Contract", version: "1" },
    paths: {
      "/customers/{id}": {
        get: {
          operationId: "getCustomer",
          parameters: [
            {
              in: "path",
              name: "id",
              required: true,
              schema: { type: "string" },
            },
          ],
          responses: {
            200: {
              description: "Customer",
              content: {
                "application/json": {
                  schema: { $ref: "#/components/schemas/Customer" },
                },
              },
            },
          },
        },
      },
    },
    components: {
      schemas: {
        Customer: {
          allOf: [
            { $ref: "#/components/schemas/Base" },
            {
              type: "object",
              properties: {
                created_at: { type: ["string", "null"], format: "date-time" },
                contact: { $ref: "#/components/schemas/Contact" },
              },
            },
          ],
        },
        Base: {
          type: "object",
          required: ["id"],
          properties: {
            id: { type: "string", description: "Public identifier." },
          },
        },
        Contact: {
          type: "object",
          properties: { email: { type: ["string", "null"] } },
        },
        CustomerUpdateInput: {
          type: "object",
          properties: {
            legal_name: { type: "string" },
            email: { type: ["string", "null"] },
            country: { type: "string", default: "MEX" },
            page: { type: ["integer", "null"] },
          },
        },
      },
    },
  };
}
const binding = {
  ...config,
  outputModels: [],
  variants: {},
  resources: {
    Customer: { methods: { RetrieveAsync: { operation: "getCustomer" } } },
  },
  inputs: ["CustomerUpdateInput"],
};

test("resolves references and intersections, preserving integer, defaults, optionality and explicit null", async () => {
  const contract = await compileContract(fixture(), binding);
  const customer = contract.models.find((model) => model.name === "Customer");
  assert.equal(
    customer.properties.find((field) => field.name === "id").required,
    true,
  );
  assert.match(
    customer.properties.find((field) => field.name === "id").description,
    /Public identifier/,
  );
  assert.equal(
    customer.properties.find((field) => field.name === "created_at").nullable,
    true,
  );
  assert.equal(
    customer.properties.find((field) => field.name === "created_at").kind,
    "date",
  );
  assert.equal(
    customer.properties.find((field) => field.name === "contact").ref,
    "Contact",
  );
  const input = contract.models.find(
    (model) => model.name === "CustomerUpdateInput",
  );
  assert.equal(
    input.properties.find((field) => field.name === "country").required,
    false,
  );
  assert.equal(
    input.properties.find((field) => field.name === "legal_name").nullable,
    false,
  );
  assert.equal(
    input.properties.find((field) => field.name === "email").nullable,
    true,
  );
  assert.equal(
    input.properties.find((field) => field.name === "page").kind,
    "integer",
  );
  assert.equal(
    input.properties.find((field) => field.name === "page").nullable,
    true,
  );
});

test("a new contract field flows through to source without a handwritten property inventory", async () => {
  const spec = fixture();
  spec.components.schemas.Base.properties.new_field = {
    type: "string",
    description: "New documented value.",
  };
  const outputs = renderModels(await compileContract(spec, binding));
  assert.match([...outputs.values()].join("\n"), /New documented value/);
  assert.match([...outputs.values()].join("\n"), /new_field/);
});

test("requires a public method for every resource operation", async () => {
  const spec = fixture();
  spec.paths["/customers/new"] = {
    get: { ...spec.paths["/customers/{id}"].get, operationId: "newOperation" },
  };
  await assert.rejects(
    compileContract(spec, binding),
    /Every public operation/,
  );
});

test("preserves deliberately open payloads without inventing a schema", async () => {
  const spec = fixture();
  spec.components.schemas.Base.properties.metadata = {
    type: "object",
    additionalProperties: true,
  };
  const contract = await compileContract(spec, binding);
  const metadata = contract.models
    .find((model) => model.name === "Customer")
    .properties.find((field) => field.name === "metadata");
  assert.equal(metadata.kind, "map");
});

test("represents heterogeneous alternatives without a JSON envelope", async () => {
  const spec = fixture();
  spec.components.schemas.CustomerUpdateInput.properties.variant = {
    oneOf: [{ type: "string" }, { type: "integer" }],
  };
  const contract = await compileContract(spec, binding);
  const variant = contract.models
    .find((model) => model.name === "CustomerUpdateInput")
    .properties.find((field) => field.name === "variant");
  assert.equal(variant.kind, "union");
  assert.deepEqual(variant.variants.map((value) => value.kind).sort(), [
    "integer",
    "string",
  ]);
  assert.match([...renderModels(contract).values()].join("\n"), /OneOf</);
});

test("keeps null-only response variants nullable without widening their value type", async () => {
  const spec = fixture();
  spec.components.schemas.Customer = {
    oneOf: [
      {
        type: "object",
        properties: {
          uuid: { type: "string" },
          status: { type: "string", const: "valid" },
        },
      },
      {
        type: "object",
        properties: {
          uuid: { type: "null" },
          status: { type: "string", const: "draft" },
        },
      },
    ],
  };
  const customer = (await compileContract(spec, binding)).models.find(
    (model) => model.name === "Customer",
  );
  assert.equal(
    customer.properties.find((field) => field.name === "uuid").kind,
    "string",
  );
  assert.equal(
    customer.properties.find((field) => field.name === "uuid").nullable,
    true,
  );
  assert.equal(
    customer.properties.find((field) => field.name === "status").kind,
    "string",
  );
});

test("normalizes numeric intersections and omits read-only request fields", async () => {
  const spec = fixture();
  spec.components.schemas.CustomerUpdateInput.properties.days = {
    allOf: [{ type: "number" }, { type: "integer" }],
  };
  spec.components.schemas.CustomerUpdateInput.properties.created_at = {
    type: "string",
    format: "date-time",
    readOnly: true,
  };
  const input = (await compileContract(spec, binding)).models.find(
    (model) => model.name === "CustomerUpdateInput",
  );
  assert.equal(
    input.properties.find((field) => field.name === "days").kind,
    "integer",
  );
  assert.equal(
    input.properties.some((field) => field.name === "created_at"),
    false,
  );
});

test("retains constructor requirements while making named input variants assignable to their family", async () => {
  const spec = fixture();
  spec.components.schemas.CustomerNationalCreateInput = {
    type: "object",
    required: ["legal_name", "tax_id"],
    properties: { legal_name: { type: "string" }, tax_id: { type: "string" } },
  };
  spec.components.schemas.CustomerForeignCreateInput = {
    type: "object",
    required: ["legal_name"],
    properties: { legal_name: { type: "string" }, country: { type: "string" } },
  };
  spec.components.schemas.CustomerCreateInput = {
    anyOf: [
      { $ref: "#/components/schemas/CustomerNationalCreateInput" },
      { $ref: "#/components/schemas/CustomerForeignCreateInput" },
    ],
  };
  const contract = await compileContract(spec, binding);
  const national = contract.models.find(
    (model) => model.name === "CustomerNationalCreateInput",
  );
  assert.equal(national.parent, "CustomerCreateInput");
  assert.equal(
    national.properties.find((field) => field.name === "tax_id").required,
    true,
  );
  assert.match(
    renderModels(contract).get("Models/Inputs/CustomerNationalCreateInput.cs"),
    /: base\(@legalName\)/,
  );
});
