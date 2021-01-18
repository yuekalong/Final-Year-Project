// import express
const express = require("express");
const router = express.Router();

// importing the controller to controll the action for specific route
const authContoller = require("../controllers/AuthController.js");

//  this route is for logging in our app
router.post("/sign-up", authContoller.signUp);
router.post("/login", authContoller.login);

module.exports = router;
