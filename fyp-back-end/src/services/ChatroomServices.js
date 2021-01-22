const knex = require("knex")(require("../../knexfile.js")["development"]);
const { v4: uuid } = require("uuid");

module.exports = {
  getHistory: async function (connection, user) {
    const roomID = (
      await knex("group").first("id").where("user_id", "=", user.id)
    ).id;

    const records = await knex("chatroom_history")
      .select("user.name", "chatroom_history.linetxt")
      .join("user", "user.id", "=", "chatroom_history.user_id")
      .where("group_id", "=", roomID)
      .orderBy("chatroom_history.created_at", "asc");

    records.forEach((record) => {
      connection.io.to(user.socketID).emit("receive-msg", {
        name: record.name,
        msg: record.linetxt,
      });
    });
  },

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
  sendMsg: async function (connection, data) {
    const roomID = (
      await knex("group").first("id").where("user_id", "=", data.id)
    ).id;

    await knex("chatroom_history").insert({
      id: uuid(),
      group_id: roomID,
      user_id: data.id,
      linetxt: `${data.msg}`,
    });

    connection.io
      .to(roomID)
      .emit("receive-msg", { name: data.name, msg: data.msg });
    console.log("Send Message!");
  },
};
