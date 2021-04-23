const knex = require("knex")(require("../../knexfile.js")["development"]);

module.exports = {
  getTreasureId: async function (gameID) {
    const treasure = (
      await knex("game")
        .first("treasure_id")
        .where("id", "=", gameID)
    );
    console.log(treasure);
    return treasure;
  },
};
