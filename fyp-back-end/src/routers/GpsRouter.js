// import express
const express = require("express");
const router = express.Router();

// importing the controller to controll the action for specific route
const gpsContoller = require("../controllers/GpsController.js");

//  this route is for logging in our app
router.post("/location/:id", gpsContoller.postLocation);
router.get("/locationTeammates/:playerid/:groupid", gpsContoller.getTeamLocation);
router.get("/locationOpps/:groupid", gpsContoller.getOppLocation);
router.get("/hints/:gameid", gpsContoller.getHintsLocation);

module.exports = router;
