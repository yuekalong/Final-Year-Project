// import express
const express = require("express");
const router = express.Router();

// importing the controller to controll the action for specific route
const gpsContoller = require("../controllers/GpsController.js");

//  this route is for logging in our app
router.post("/location/:id/:locx/:locy", gpsContoller.postLocation);

module.exports = router;
