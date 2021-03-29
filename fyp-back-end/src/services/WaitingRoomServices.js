const knex = require("knex")(require("../../knexfile.js")["development"]);
const { v4: uuid } = require("uuid");
require("dotenv").config();

module.exports = {
  playerCount: async function (user) {
    // update player count
    const hunterQuery = await knex("game")
      .count("user_id as count")
      .join("group", "group.id", "=", "hunter_group_id")
      .groupBy("group.id")
      .where("game.id", "=", user.game_id);

    const protectorQuery = await knex("game")
      .count("user_id as count")
      .join("group", "group.id", "=", "protector_group_id")
      .groupBy("group.id")
      .where("game.id", "=", user.game_id);

    const hunterCount = hunterQuery.length > 0 ? hunterQuery[0].count : 0;
    const protectorCount =
      protectorQuery.length > 0 ? protectorQuery[0].count : 0;

    const totalCount = hunterCount + protectorCount;
    return totalCount;
  },

  joinRoom: async function (connection, user) {
    console.log("WaitingRoomServices.joinRoom started!");

    const roomID = user.game_id;

    // join the room
    connection.socket.join(roomID);
    console.log(`Room ${roomID} Join!`);

    // calculate the total player count in the room
    const totalCount = await this.playerCount(user);

    connection.io.to(roomID).emit("player-count", {
      count: totalCount,
      maximum_count: parseInt(process.env.MAXIMUM_NUMBER_OF_PLAYER),
    });

    if (totalCount == parseInt(process.env.MAXIMUM_NUMBER_OF_PLAYER)) {
      // change the room status
      await knex("game").update({ status: "playing" }).where("id", "=", roomID);

      connection.io.to(roomID).emit("start-game");
    }
  },

  getCurrentPlayerCount: async function (connection, user) {
    console.log("WaitingRoomServices.getCurrentPlayerCount started!");

    // calculate the total player count in the room
    const totalCount = await this.playerCount(user);

    connection.io.to(roomID).emit("player-count", {
      count: totalCount,
      maximum_count: parseInt(process.env.MAXIMUM_NUMBER_OF_PLAYER),
    });
  },
};
