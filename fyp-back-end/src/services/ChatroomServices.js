module.exports = {
  joinRoom: async function (connection) {
    connection.socket.join();
    console.log("Room Join!");
  },
  sendMsg: async function (msg, io) {
    connection.io.emit("receive-msg", msg);
    console.log("Send Message!");
  },
};
