const knex = require("knex")(require("../../knexfile.js")["development"]);
const { v4: uuid } = require("uuid");

module.exports = {
  joinRoom: async function (connection, user) {
    console.log(user.id);
    const roomID = (
      await knex("group").first("id").where("user_id", "=", user.id)
    ).id;

    // join the room
    connection.socket.join(roomID);
    console.log(`Room ${roomID} Join!`);

    // get history
    // this.getHistory(connection, roomID, user.socketID);
  },
};
