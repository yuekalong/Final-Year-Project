const knex = require("knex")(require("../../knexfile.js")["development"]);
const { v4: uuid } = require("uuid");

module.exports = {
  getHistory: async function (connection) {
    const records = await knex("chatroom_history")
      .select("user.name", "chatroom_history.linetxt")
      .join("user", "user.id", "=", "chatroom_history.user_id");

    records.forEach((record) => {
      connection.io.emit("receive-msg", {
        name: record.name,
        msg: record.linetxt,
      });
    });
  },

  joinRoom: async function (connection) {
    const roomID = "1234";

    // join the room
    connection.socket.join(roomID);
    console.log(`Room${roomID} Join!`);
    this.getHistory(connection);
  },
  sendMsg: async function (connection, data) {
    console.log(data);
    await knex("chatroom_history").insert({
      id: uuid(),
      group_id: "123",
      user_id: "123",
      linetxt: `${data.msg}`,
    });
    connection.io.emit("receive-msg", { name: data.name, msg: data.msg });
    console.log("Send Message!");
  },
};
