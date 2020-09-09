// import the service
const chatroomServices = require("../services/ChatroomServices.js");

module.exports = {
  socket: function (socket, io) {
    console.log("Connect Socket.io!");

    socket.on("joinRoom", () => {
      chatroomServices.joinRoom(socket, io);
    });
  },
};
