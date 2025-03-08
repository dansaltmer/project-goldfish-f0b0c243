"use strict";

import { handler } from "../../app.mjs";
import { expect } from "chai";
var event, context;

describe("Tests index", function () {
  it("verifies successful response", async () => {
    const result = await handler(event, context);

    expect(result).to.be.an("object");
  });
});
