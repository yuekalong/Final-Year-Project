const knex = require("knex")(require("../../knexfile.js")["development"]);

module.exports = {
  validateTreasure: async function (gameID, input) {
    const treasure = (
      await knex("treasure")
        .first("code")
        .join("game", "game.treasure_id", "=", "treasure.id")
        .where("game.id", "=", gameID)
    ).code;

    return treasure == input;
  },
};
