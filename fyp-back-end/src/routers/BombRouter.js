// import express
const express = require("express");
const router = express.Router();

// importing the controller to controll the action for specific route
const bombController = require("../controllers/BombController.js");

// get all bombs
router.get("/:gameID", bombController.allBombs);

// create bomb
router.post("/create-bomb", bombController.createBomb);

// get specific bomb pattern lock order
router.get("/:lockID/pattern-lock", bombController.patternLockOrder);

//  validate the input pattern
router.get("/:lockID/validate-pattern", bombController.validatePattern);

module.exports = router;