const knex = require("knex")(require("../../knexfile.js")["development"]);
const patternLockServices = require("./PatternLockService.js");

const { v4: uuidv4 } = require("uuid");

module.exports = {
  allBombs: async function (gameID) {
    const bombs = await knex("game_bombs_mapping")
      .select("type", "range", "loc_x", "loc_y", "order")
      .join("bomb", "bomb.id", "=", "game_bombs_mapping.bomb_id")
      .join(
        "pattern_lock",
        "pattern_lock.id",
        "=",
        "game_bombs_mapping.pattern_lock_id"
      );

    return bombs;
  },
  createBomb: async function (gameID, input, bombID, locX, locY) {
    const lockID = uuidv4();
    // insert lock order in `pattern_lock` table
    await knex("pattern_lock").insert({ id: lockID, order: input });

    // insert in `game_bombs_mapping` table
    return await knex("game_bombs_mapping").insert({
      game_id: gameID,
      bomb_id: bombID,
      loc_x: locX,
      loc_y: locY,
      pattern_lock_id: lockID,
    });
  },
  patternLockOrder: async function (lockID) {
    const bomb = await knex("pattern_lock").first("*").where("id", "=", lockID);

    if (!bomb) {
      return null;
    }

    const details = {
      id: bomb.id,
      start_pt: JSON.parse(bomb.order)[0],
      order: JSON.parse(bomb.order),
    };
    return details;
  },
  validatePattern: async function (lockID, input) {
    const isValid = await patternLockServices.validInput(lockID, input);

    if (isValid) {
      // delete in `game_bombs_mapping`
      await knex("game_bombs_mapping")
        .where("pattern_lock_id", "=", lockID)
        .delete();

      // delete in `pattern_lock`
      await knex("pattern_lock").where("id", "=", lockID).delete();
    }

    return isValid;
  },
};
