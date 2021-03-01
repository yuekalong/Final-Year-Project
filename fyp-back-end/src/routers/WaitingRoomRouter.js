// import express
const express = require("express");
const router = express.Router();

// import controller
const waitingRoomController = require("../controllers/WaitingRoomController.js");

module.exports = function (io) {
  // when connect the socket.io service
  io.on("connection", (socket) => {
    // console.log("Connect Socket.io!");

    const connection = {
      socket: socket,
      io: io,
    };

    // join room
    socket.on("join-waiting-room", (user) => {
      waitingRoomController.joinRoom(connection, user);
    });

    // when disconnect
    socket.on("disconnect", function () {
      console.log("Disconnect Socket.io!");
    });
  });
  return router;
};
