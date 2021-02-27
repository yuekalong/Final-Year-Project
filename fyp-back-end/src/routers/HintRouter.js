// import express
const express = require("express");
const router = express.Router();

// importing the controller to controll the action for specific route
const hintController = require("../controllers/HintController.js");

// get all avaliable hints
router.get("/:gameID", hintController.avaliableHints);

// get group archive hints
router.get("/:gameID/group/:groupID", hintController.hintDetail);

// validate the input pattern
router.get("/validate-pattern", hintController.validatePattern);

module.exports = router;
