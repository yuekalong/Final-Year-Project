const knex = require("knex")(require("../../knexfile.js")["development"]);

module.exports = {
  postLocation: async function (userID, locx, locy, visible) {
    const group = await knex("user")
      .update("loc_x", locx)
      .update("loc_y", locy)
      .update("visible", visible)
      .where("id", "=", userID);
  },

  getTeamLocation: async function (playerid, groupid) {
    const teammates_id = await knex
      .select("user_id")
      .from("group")
      .where("id", groupid)
      .whereNot("user_id", playerid);
    const teammates_loc = await knex
      .select("loc_x", "loc_y")
      .from("user")
      .where("id", teammates_id[0].user_id)
      .orWhere("id", teammates_id[1].user_id);
    console.log(teammates_loc);

    return teammates_loc;
  },

  getOppLocation: async function (groupid) {
    const opps_id = await knex
      .select("user_id")
      .from("group")
      .where("id", groupid);

    const opps_loc = await knex
      .select("loc_x", "loc_y", "visible")
      .from("user")
      .where("id", opps_id[0].user_id)
      .orWhere("id", opps_id[1].user_id)
      .orWhere("id", opps_id[2].user_id);
    console.log(opps_loc);

    return opps_loc;
  },
  getHintsLocation: async function (gameid) {
    const hints_id = await knex("game_hints_mapping")
      .where("game_hints_mapping.game_id", gameid)
      .join("hint", "hint.id", "=", "game_hints_mapping.hint_id")
      .select("id", "hint_words", "loc_x", "loc_y");

    return hints_id;
  },
  removeHintsLocation: async function (index) {
    const hints_id = await knex("hint")
      .update("loc_x", 0)
      .update("loc_y", 0)
      .where("id", index);
  },
};
