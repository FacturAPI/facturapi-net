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
  methods: { retrieve: "getCustomer" },
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
    /Every Customers operation/,
  );
});

test("fails on unsupported open types instead of generating an untyped model", async () => {
  const spec = fixture();
  spec.components.schemas.Base.properties.unknown = {};
  await assert.rejects(compileContract(spec, binding), /Unsupported open type/);
});

test("fails on an unsupported union instead of flattening incompatible variants", async () => {
  const spec = fixture();
  spec.components.schemas.Base.properties.variant = {
    oneOf: [{ type: "string" }, { type: "integer" }],
  };
  await assert.rejects(compileContract(spec, binding), /Unsupported union/);
});
