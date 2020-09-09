// import express
const express = require("express");
const router = express.Router();

// import controller
const chatroomController = require("../controllers/ChatroomController.js");

module.exports = function (io) {
  // when connect the socket.io service
  io.on("connection", (socket) => {
    chatroomController.socket(socket, io);
  });

  return router;
};
