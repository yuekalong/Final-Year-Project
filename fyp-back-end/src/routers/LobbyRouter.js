// import express
const express = require("express");
const router = express.Router();

// importing the controller to controll the action for specific route
const lobbyContoller = require("../controllers/LobbyController.js");

// get all avaliable lobby
router.get("/", lobbyContoller.getRoom);

//  create
router.post("/create-room", lobbyContoller.createRoom);

// join room
router.put("/join", lobbyContoller.joinRoom);

module.exports = router;
