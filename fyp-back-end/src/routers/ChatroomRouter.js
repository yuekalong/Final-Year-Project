// import express
const express = require("express");
const router = express.Router();

// import controller
const chatroomController = require("../controllers/ChatroomController.js");

module.exports = function (io) {
  // when connect the socket.io service
  io.on("connection", (socket) => {
    console.log("Connect Socket.io!");

    const connection = {
      socket: socket,
      io: io,
    };

    // join room
    socket.on("join-room", () => {
      chatroomController.joinRoom(connection);
    });

    // send message
    socket.on("send-msg", (msg) => {
      chatroomController.sendMsg(connection, msg);
    });

    // when disconnect
    socket.on("disconnect", function () {
      console.log("Disconnect Socket.io!");
    });
  });
  return router;
};
