// import the service
const hintServices = require("../services/HintServices.js");
const { standardServiceResponse } = require("../utils/ResponseHandler");

module.exports = {
  getAllHint: function (req, res, next) {
    try {
      console.log("HintController.getAllHint started!");

      const { gameID } = req.params;

      return standardServiceResponse(
        res,
        next,
        hintServices.getAllHint(gameID)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(
        "Error: HintController.getAllHint: " +
          JSON.parse(err.message)["message"]
      );
      next(err);
    }
  },
  groupArchiveHint: function (req, res, next) {
    try {
      console.log("HintController.groupArchiveHint started!");

      const { gameID, groupID } = req.params;

      return standardServiceResponse(
        res,
        next,
        hintServices.groupArchiveHint(gameID, groupID)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(
        "Error: HintController.groupArchiveHint: " +
          JSON.parse(err.message)["message"]
      );
      next(err);
    }
  },
  patternLockOrder: function (req, res, next) {
    try {
      console.log("HintController.patternLockOrder started!");

      const { gameID, hintID } = req.params;

      return standardServiceResponse(
        res,
        next,
        hintServices.patternLockOrder(gameID, hintID)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(
        "Error: HintController.patternLockOrder: " +
          JSON.parse(err.message)["message"]
      );
      next(err);
    }
  },
  validatePattern: function (req, res, next) {
    try {
      console.log("HintController.validatePattern started!");

      const { gameID } = req.params;
      const { groupID, hintID, input } = req.query;

      return standardServiceResponse(
        res,
        next,
        hintServices.validatePattern(gameID, groupID, hintID, input)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(
        "Error: HintController.validatePattern: " +
          JSON.parse(err.message)["message"]
      );
      next(err);
    }
  },
};
