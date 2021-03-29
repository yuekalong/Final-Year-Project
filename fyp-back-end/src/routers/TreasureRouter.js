// import express
const express = require("express");
const router = express.Router();

// importing the controller to controll the action for specific route
const treasureController = require("../controllers/TreasureController.js");

router.get("/:gameID", treasureController.validateTreasure);

module.exports = router;
