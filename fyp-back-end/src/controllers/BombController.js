// import the service
const bombServices = require("../services/BombServices.js");
const { standardServiceResponse } = require("../utils/ResponseHandler");

module.exports = {
  validatePattern: function (req, res, next) {
    try {
      console.log("BombController.validatePattern started!");

      const { id, input } = req.query;

      return standardServiceResponse(
        res,
        next,
        bombServices.validatePattern(id, input)
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
