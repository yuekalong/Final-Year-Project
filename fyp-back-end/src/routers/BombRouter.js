// import express
const express = require("express");
const router = express.Router();

// importing the controller to controll the action for specific route
const bombController = require("../controllers/BombController.js");

//  this route is for logging in our app
router.get("/validate-pattern", hintController.validatePattern);

module.exports = router;
