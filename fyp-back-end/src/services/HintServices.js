const knex = require("knex")(require("../../knexfile.js")["development"]);
const patternLockServices = require("../services/PatternLockService.js");

module.exports = {
  avaliableHints: async function (gameID) {
    const hints = await knex("hint")
      .select("id", "pattern_lock_id", "loc_x", "loc_y")
      .join("game_hints_mapping", "game_hints_mapping.hint_id", "=", "hint.id")
      .where("game_id", "=", gameID)
      .whereNull("group_id");

    return hints;
  },
  hintDetail: async function (gameID, groupID) {
    const hints = await knex("hint")
      .select("id", "hint_words", "loc_x", "loc_y")
      .join("game_hints_mapping", "game_hints_mapping.hint_id", "=", "hint.id")
      .where("game_id", "=", gameID)
      .andWhere("group_id", "=", groupID);

    return hints;
  },
  validatePattern: async function (gameID, groupID, hintID, input) {
    const patternLockID = (
      await knex("game_hints_mapping")
        .first("pattern_lock_id")
        .where("hint_id", "=", hintID)
        .andWhere("game_id", "=", gameID)
    ).pattern_lock_id;

    const isValid = await patternLockServices.validInput(patternLockID, input);

    if (isValid) {
      await knex("game_hints_mapping")
        .where("hint_id", "=", hintID)
        .andWhere("game_id", "=", gameID)
        .update({ group_id: groupID });
    }

    return isValid;
  },
};
