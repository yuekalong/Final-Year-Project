// import the service
const hintServices = require("../services/HintServices.js");
const { standardServiceResponse } = require("../utils/ResponseHandler");

module.exports = {
  avaliableHints: function (req, res, next) {
    try {
      console.log("HintController.avaliableHints started!");

      const { gameID } = req.params;

      return standardServiceResponse(
        res,
        next,
        hintServices.avaliableHints(gameID)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(
        "Error: HintController.avaliableHints: " +
          JSON.parse(err.message)["message"]
      );
      next(err);
    }
  },
  hintDetail: function (req, res, next) {
    try {
      console.log("HintController.hintDetail started!");

      const { gameID, groupID } = req.params;

      return standardServiceResponse(
        res,
        next,
        hintServices.hintDetail(gameID, groupID)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(
        "Error: HintController.hintDetail: " +
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
