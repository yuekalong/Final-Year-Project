module.exports = {
  joinRoom: async function (connection) {
    connection.socket.join();
    console.log("Room Join!");
  },
  sendMsg: async function (connection, data) {
    console.log(data.msg);
    connection.io.emit("receive-msg", { name: data.name, msg: data.msg });
    console.log("Send Message!");
  },
};
