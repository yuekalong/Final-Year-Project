const knex = require("knex")(require("../../knexfile.js")["development"]);

module.exports = {
    postLocation: async function (userID,locx,locy) {
    const group = await knex("user")
      .update("loc_x",locx)
      .update("loc_y",locy)
      .where("id", "=", userID);
    }
};
