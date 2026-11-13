import assert from "node:assert/strict";
import { readFile, writeFile, mkdir, unlink } from "node:fs/promises";
import { banner } from "./sdk/render-models.mjs";
import { readSpecification } from "./sdk/openapi-source.mjs";
import { compileContract } from "./sdk/compiler.mjs";
import { renderModels } from "./sdk/render-models.mjs";
import { renderResources } from "./sdk/render-resources.mjs";

const root = new URL("../", import.meta.url);
const config = JSON.parse(
  await readFile(new URL("scripts/sdk/config.json", root), "utf8"),
);
const contract = await compileContract(
  await readSpecification(
    JSON.parse(await readFile(new URL("openapi/source.json", root), "utf8")),
  ),
  config,
);
const outputs = new Map([
  ...renderModels(contract),
  ...renderResources(contract),
]);
for (const [filename, content] of outputs)
  outputs.set(filename, content.replace(/[ \t]+$/gm, ""));
let attributes = "";
try {
  attributes = await readFile(new URL(".gitattributes", root), "utf8");
} catch (error) {
  if (error.code !== "ENOENT") throw error;
}
const generatedAttributes =
  "# BEGIN generated SDK\n" +
  [...outputs.keys()]
    .sort()
    .map((filename) => `${filename} linguist-generated=true`)
    .join("\n") +
  "\n# END generated SDK";
outputs.set(
  ".gitattributes",
  attributes.includes("# BEGIN generated SDK")
    ? attributes.replace(
        /# BEGIN generated SDK[\s\S]*?# END generated SDK/,
        generatedAttributes,
      )
    : attributes.trimEnd() +
        (attributes ? "\n\n" : "") +
        generatedAttributes +
        "\n",
);
// The manifest detects removed as well as changed artifacts in check mode.
outputs.set(
  "openapi/generated.json",
  JSON.stringify([...outputs.keys()].sort(), null, 2) + "\n",
);
let previous = [];
try {
  previous = JSON.parse(
    await readFile(new URL("openapi/generated.json", root), "utf8"),
  );
} catch (error) {
  if (error.code !== "ENOENT") throw error;
}
for (const filename of previous.filter((filename) => !outputs.has(filename))) {
  assert(
    !process.argv.includes("--check"),
    `Obsolete generated file: ${filename}. Run pnpm generate:sdk.`,
  );
  assert(
    !filename.includes("..") && !filename.startsWith("/"),
    "Invalid generated path.",
  );
  assert(
    (await readFile(new URL(filename, root), "utf8")).includes(banner),
    `Refusing to remove handwritten file: ${filename}`,
  );
  await unlink(new URL(filename, root));
}
for (const [filename, content] of outputs) {
  if (process.argv.includes("--check")) {
    assert.equal(
      await readFile(new URL(filename, root), "utf8"),
      content,
      `${filename} is stale. Run pnpm generate:sdk.`,
    );
  } else {
    await mkdir(new URL(".", new URL(filename, root)), { recursive: true });
    await writeFile(new URL(filename, root), content);
  }
}
console.log(
  `${process.argv.includes("--check") ? "Verified" : "Generated"} Customers: ${contract.methods.length} operations, ${contract.models.length} models.`,
);
