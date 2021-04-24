const knex = require("knex")(require("../../knexfile.js")["development"]);

module.exports = {
  postLocation: async function (userID, locx, locy, visible) {

    const group = await knex("user")
      .update("loc_x", locx)
      .update("loc_y", locy)
      .update("visible", visible)
      .where("id", "=", userID);

    return group;
  },

  postStatus: async function (userID, game_status) {

    const group = await knex("user")
      .update("game_status", game_status)
      .where("id", "=", userID);

    return group;
  },

  triggerBomb: async function (userID,visible) {

    const group = await knex("user")
      .update("visible", visible)
      .where("id", "=", userID);

    return group;
  },

  getTeamLocation: async function (playerid, groupid) {
    const teammates_id = await knex
      .select("user_id")
      .from("group")
      .where("id", groupid)
      .whereNot("user_id", playerid);

    // required number of player is 6

    const teammates_loc = await knex
      .select("loc_x", "loc_y","game_status")
      .from("user")
      .where("id", teammates_id[0].user_id);
    //  .orWhere("id", teammates_id[1].user_id);

    return teammates_loc;
    //return teammates_id;
  },

  getOppLocation: async function (groupid) {
    const opps_id = await knex
      .select("user_id")
      .from("group")
      .where("id", groupid);

    // required number of player is 6
    const opps_loc = await knex
      .select("loc_x", "loc_y", "visible")
      .from("user")
      .where("id", opps_id[0].user_id);
    // .orWhere("id", opps_id[1].user_id)
    // .orWhere("id", opps_id[2].user_id);
    //console.log(opps_loc);

    return opps_loc;
  },
  getHintsLocation: async function (gameid) {
    var hints_id;
    var count;

    hints_id = await knex("game_hints_mapping")
      .where("game_hints_mapping.game_id", gameid)
      .join("hint", "hint.id", "=", "game_hints_mapping.hint_id")
      .select("id", "hint_words", "pattern_lock_id", "loc_x", "loc_y");

    if (hints_id[0] == undefined) {
      hints_id[0] = {
        count: 0,
      };
    }

    try {
      count = await knex("game_hints_mapping")
        .where("game_hints_mapping.game_id", gameid)
        .join("hint", "hint.id", "=", "game_hints_mapping.hint_id")
        .count("*");

      hints_id[0]["count"] = count[0]["count(*)"];
    } catch (err) {
      console.log(err);
      hints_id[0]["count"] = 0;
    }
    //console.log(hints_id);
    return hints_id;
  },
  removeHintsLocation: async function (index, game_id) {
    const hints_id = await knex("game_hints_mapping")
      .where("game_id", game_id)
      .andWhere("hint_id", index)
      .del();

    return hints_id;
  },

  getItemsLocation: async function (gameid) {
    const item_id = await knex("game_items_mapping")
      .where("game_items_mapping.game_id", gameid)
      .join("item", "item.id", "=", "game_items_mapping.item_id")
      .select("item.id", "loc_x", "loc_y");
    if (item_id[0] == undefined) {
      item_id[0] = {
        count: 0,
      };
    }
    try {
      count = await knex("game_items_mapping")
        .where("game_items_mapping.game_id", gameid)
        .join("item", "item.id", "=", "game_items_mapping.item_id")
        .count("*");

      item_id[0]["count"] = count[0]["count(*)"];
    } catch (err) {
      console.log(err);
      item_id[0]["count"] = 0;
    }
    //console.log(item_id);
    return item_id;
  },

  removeItemsLocation: async function (index, game_id) {
    const hints_id = await knex("game_items_mapping")
      .where("game_id", game_id)
      .andWhere("item_id", index)
      .del();

    return hints_id;
  },
};
