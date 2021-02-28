// import express
const express = require("express");
const router = express.Router();

// importing the controller to controll the action for specific route
const hintController = require("../controllers/HintController.js");

// get all hints
router.get("/:gameID", hintController.getAllHint);

// get group archive hints
router.get("/:gameID/group/:groupID", hintController.groupArchiveHint);

// get specific hint pattern lock start point
router.get("/:gameID/pattern-lock/:hintID", hintController.patternLockStartPt);

// validate the input pattern
router.get("/:gameID/validate-pattern", hintController.validatePattern);

module.exports = router;
