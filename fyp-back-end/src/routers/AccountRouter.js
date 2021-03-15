// import express
const express = require("express");
const router = express.Router();

// importing the controller to controll the action for specific route
const accountController = require("../controllers/AccountController.js");

router.get("/game-basic-info/:gameID/:userID", accountController.getGameInfo);

module.exports = router;
