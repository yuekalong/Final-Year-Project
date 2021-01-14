const knex = require("knex")(require("../../knexfile.js")["development"]);
const patternLockServices = require("../services/PatternLockService.js");

module.exports = {
  validatePattern: async function (id, input) {
    const patternLockID = (
      await knex("game_hints_mapping")
        .first("pattern_lock_id")
        .where("hint_id", "=", id)
    ).pattern_lock_id;

    return await patternLockServices.validInput(patternLockID, input);
  },
};
