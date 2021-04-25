// import express
const express = require("express");
const router = express.Router();

// import controller
const chatroomController = require("../controllers/ChatroomController.js");

module.exports = function (io) {
  // when connect the socket.io service
  io.on("connection", (socket) => {
    // console.log("Connect Socket.io!");

    const connection = {
      socket: socket,
      io: io,
    };

    // join room
    socket.on("join-chatroom", (user) => {
      chatroomController.joinRoom(connection, user);
    });

    // send message
    socket.on("send-msg", (data) => {
      chatroomController.sendMsg(connection, data);
    });

    // send endgame message
    socket.on("send-winlose-team", (data) => {
      chatroomController.sendWinLoseTeam(connection, data);
    });

    // send endgame message
    socket.on("send-winlose-opp", (data) => {
      chatroomController.sendWinLoseOpp(connection, data);
    });
    // get history
    socket.on("get-history", (user) => {
      chatroomController.getHistory(connection, user);
    });

    // when disconnect
    socket.on("disconnect", function () {
      console.log("Disconnect Socket.io!");
    });
  });
  return router;
};
