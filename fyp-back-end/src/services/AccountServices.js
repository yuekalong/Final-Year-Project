const knex = require("knex")(require("../../knexfile.js")["development"]);

module.exports = {
  getGameInfo: async function (gameID, userID) {
    // old method
    // const group = await knex("group")
    //   .first("id", "type")
    //   .where("user_id", "=", userID);

    // let game;
    // switch (group.type) {
    //   case "hunter":
    //     game = await knex("game")
    //       .first("*")
    //       .where("hunter_group_id", "=", group.id);
    //     break;
    //   case "protector":
    //     game = await knex("game")
    //       .first("*")
    //       .where("protector_group_id", "=", group.id);
    //     break;
    // }

    // new method
    const game = await knex("game").first("*").where("id", "=", gameID);

    const hunters = await knex("game")
      .select("user_id")
      .join("group", "group.id", "=", "hunter_group_id")
      .where("game.id", "=", gameID);

    const protectors = await knex("game")
      .select("user_id")
      .join("group", "group.id", "=", "protector_group_id")
      .where("game.id", "=", gameID);

    const groupType = hunters.some((hunter) => hunter.user_id == userID)
      ? "hunter"
      : protectors.some((protector) => protector.user_id == userID)
      ? "protector"
      : undefined;

    const result = {
      game: {
        id: game.id,
        map_number: game.map_number,
        tresure: game.treasure_id,
      },
      huneters: hunters,
      protectors: protectors,
      group: {
        type: groupType,
        id:
          groupType == "hunter"
            ? game.hunter_group_id
            : groupType == "protector"
            ? game.protector_group_id
            : undefined,
      },
      opponent: {
        type:
          groupType == "hunter"
            ? "protector"
            : groupType == "protector"
            ? "hunter"
            : undefined,
        id:
          groupType == "hunter"
            ? game.protector_group_id
            : groupType == "protector"
            ? game.hunter_group_id
            : undefined,
      },
    };

    return result;
  },
};
