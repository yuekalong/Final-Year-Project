// import express
const express = require("express");
const router = express.Router();

// importing the controller to controll the action for specific route
const gpsContoller = require("../controllers/GpsController.js");

//  this route is for logging in our app
router.post("/location/:id", gpsContoller.postLocation);
router.get("/locationTeammates/:playerid/:groupid", gpsContoller.getTeamLocation);
router.get("/locationOpps/:groupid", gpsContoller.getOppLocation);

router.post("/status/:id", gpsContoller.postStatus);

router.get("/location/trigger/:playerid", gpsContoller.triggerBomb);

router.get("/hints/:gameid", gpsContoller.getHintsLocation);
router.post("/hintRemove", gpsContoller.removeHintsLocation);

router.get("/items/:gameid", gpsContoller.getItemsLocation);
router.post("/itemRemove", gpsContoller.removeItemsLocation);

module.exports = router;
