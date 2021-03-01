// import the service
const bombServices = require("../services/BombServices.js");
const { standardServiceResponse } = require("../utils/ResponseHandler");

module.exports = {
  allBombs: function (req, res, next) {
    try {
      console.log("BombController.allBombs started!");

      const { gameID } = req.params;

      return standardServiceResponse(res, next, bombServices.allBombs(gameID));
    } catch (err) {
      // catch exception and shows the error message
      console.log(
        "Error: BombController.allBombs: " + JSON.parse(err.message)["message"]
      );
      next(err);
    }
  },
  createBomb: function (req, res, next) {
    try {
      console.log("BombController.createBomb started!");

      const { gameID, input, bombID, locX, locY } = req.body;

      return standardServiceResponse(
        res,
        next,
        bombServices.createBomb(gameID, input, bombID, locX, locY)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(
        "Error: BombController.createBomb: " +
          JSON.parse(err.message)["message"]
      );
      next(err);
    }
  },
  patternLockOrder: function (req, res, next) {
    try {
      console.log("BombController.patternLockOrder started!");

      const { lockID } = req.params;

      return standardServiceResponse(
        res,
        next,
        bombServices.patternLockOrder(lockID)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(
        "Error: BombController.patternLockOrder: " +
          JSON.parse(err.message)["message"]
      );
      next(err);
    }
  },
  validatePattern: function (req, res, next) {
    try {
      console.log("BombController.validatePattern started!");

      const { lockID } = req.params;
      const { input } = req.query;

      return standardServiceResponse(
        res,
        next,
        bombServices.validatePattern(lockID, input)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(
        "Error: BombController.validatePattern: " +
          JSON.parse(err.message)["message"]
      );
      next(err);
    }
  },
};
