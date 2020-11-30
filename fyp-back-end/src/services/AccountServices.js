const knex = require("knex")(require("../../knexfile.js")["development"]);

module.exports = {
  getGameInfo: async function (userID) {
    const group = await knex("group")
      .first("id", "type")
      .where("user_id", "=", userID);

    let game;
    switch (group.type) {
      case "hunter":
        game = await knex("game")
          .first("*")
          .where("hunter_group_id", "=", group.id);
        break;
      case "protector":
        game = await knex("game")
          .first("*")
          .where("protector_group_id", "=", group.id);
        break;
    }

    const result = {
      game: {
        id: game.id,
        area: game.area_id,
        tresure: game.treasure_id,
      },
      group: {
        id: group.id,
        type: group.type,
      },
      opponent: {
        type: group.type == "hunter" ? "protector" : "hunter",
        id:
          group.type == "hunter"
            ? game.protector_group_id
            : game.hunter_group_id,
      },
    };

    return result;
  },
};
