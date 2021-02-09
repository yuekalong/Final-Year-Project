const knex = require("knex")(require("../../knexfile.js")["development"]);

module.exports = {
    postLocation: async function (userID,locx,locy) {
    const group = await knex("user")
      .update("loc_x",locx)
      .update("loc_y",locy)
      .where("id", "=", userID);
    },

    getTeamLocation: async function (playerid,groupid) {
      const teammates_id = await knex.select("user_id")
        .from("group")
        .where("id", groupid)
        .whereNot("user_id", playerid)
      const teammates_loc = await knex.select("loc_x","loc_y")
        .from("user")
        .where("id", teammates_id[0].user_id)
        .orWhere("id", teammates_id[1].user_id)
        console.log(teammates_loc)

        return teammates_loc;
    },

    getOppLocation: async function (groupid) {
      const opps_id = await knex.select("user_id")
        .from("group")
        .where("id", groupid)

      const opps_loc = await knex.select("loc_x","loc_y")
        .from("user")
        .where("id", opps_id[0].user_id)
        .orWhere("id", opps_id[1].user_id)
        .orWhere("id", opps_id[2].user_id)
        console.log(opps_loc)

        return opps_loc;
    },
};
