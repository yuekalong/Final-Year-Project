// import the service
const treasureServices = require("../services/TreasureServices.js");
const { standardServiceResponse } = require("../utils/ResponseHandler");

module.exports = {
  getTreasureId: function (req, res, next) {
    try {
      console.log("TreaasureController.getTreasureId started!");
      const { gameID } = req.params;

      return standardServiceResponse(
        res,
        next,
        treasureServices.getTreasureId(gameID)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(
        "Error: TreaasureController.getTreasureId: " +
          JSON.parse(err.message)["message"]
      );
      next(err);
    }
  },
};
