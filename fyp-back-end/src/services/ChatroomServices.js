const knex = require("knex")(require("../../knexfile.js")["development"]);
const { v4: uuid } = require('uuid');

module.exports = {
  joinRoom: async function (connection) {
    connection.socket.join();
    console.log("Room Join!");
    const records = await knex('chatroom_history').select("*");
    records.forEach(record => {
      connection.io.emit("receive-msg", { name: record.linetxt.split(":")[0], msg: record.linetxt.split(":")[1] });
    })
  },
  sendMsg: async function (connection, data) {
    await knex('chatroom_history').insert({
      id: uuid(),
      group_id: "123",
      linetxt: `${data.name}: ${data.msg}`
    });
    connection.io.emit("receive-msg", { name: data.name, msg: data.msg });
    console.log("Send Message!");
  },
};
