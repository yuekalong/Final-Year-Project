// import the service
const waitingRoomServices = require("../services/WaitingRoomServices.js");

module.exports = {
  joinRoom: function (connection, user) {
    waitingRoomServices.joinRoom(connection, user);
  },
  getCurrentPlayerCount: function (connection, user) {
    waitingRoomServices.getCurrentPlayerCount(connection, user);
  },
};
