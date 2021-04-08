const knex = require("knex")(require("../../knexfile.js")["development"]);
const patternLockServices = require("../services/PatternLockService.js");
const gpsServices = require("../services/gpsServices.js");

module.exports = {
  getAllHint: async function (gameID) {
    const hints = await knex("hint")
      .select("id", "loc_x", "loc_y", "group_id")
      .join("game_hints_mapping", "game_hints_mapping.hint_id", "=", "hint.id")
      .where("game_id", "=", gameID);

    // if check avaliable hints
    // .whereNull("group_id");

    return hints;
  },
  groupArchiveHint: async function (gameID, groupID) {
    const hints = await knex("hint")
      .select("id", "hint_words", "loc_x", "loc_y")
      .join("game_hints_mapping", "game_hints_mapping.hint_id", "=", "hint.id")
      .where("game_id", "=", gameID)
      .andWhere("group_id", "=", groupID);

    return hints;
  },
  patternLockOrder: async function (gameID, hintID) {
    const hint = await knex("hint")
      .first("hint.id", "order")
      .join("game_hints_mapping", "game_hints_mapping.hint_id", "=", "hint.id")
      .join(
        "pattern_lock",
        "pattern_lock.id",
        "=",
        "game_hints_mapping.pattern_lock_id"
      )
      .where("game_id", "=", gameID)
      .andWhere("hint_id", "=", hintID);

    if (!hint) {
      return null;
    }

    const details = {
      id: hint.id,
      start_pt: JSON.parse(hint.order)[0],
      order: JSON.parse(hint.order),
    };
    return details;
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

      await gpsServices.removeHintsLocation(hintID, gameID);
    }

    return isValid;
  },
};
