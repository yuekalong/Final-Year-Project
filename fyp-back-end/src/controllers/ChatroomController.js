// import the service
const chatroomServices = require("../services/ChatroomServices.js");
const { joinRoom } = require("../services/ChatroomServices.js");

module.exports = {
  joinRoom: function (connection) {
    chatroomServices.joinRoom(connection);
  },
  sendMsg: function (connection, data) {
    chatroomServices.sendMsg(connection, data);
  },
};
