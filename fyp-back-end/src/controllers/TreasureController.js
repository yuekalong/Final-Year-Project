// import the service
const treasureServices = require("../services/TreasureServices.js");
const { standardServiceResponse } = require("../utils/ResponseHandler");

module.exports = {
  validateTreasure: function (req, res, next) {
    try {
      console.log("TreaasureController.validateTreasure started!");
      const { gameID } = req.params;
      const { input } = req.query;
      console.log(input);
      return standardServiceResponse(
        res,
        next,
        treasureServices.validateTreasure(gameID, input)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(
        "Error: TreaasureController.validateTreasure: " +
          JSON.parse(err.message)["message"]
      );
      next(err);
    }
  },
};
