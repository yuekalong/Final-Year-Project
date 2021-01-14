const knex = require("knex")(require("../../knexfile.js")["development"]);

module.exports = {
  validInput: async function (lockID, input) {
    const order = (
      await knex("pattern_lock").first("order").where("id", "=", lockID)
    ).order;

    return order == input;
  },
};
