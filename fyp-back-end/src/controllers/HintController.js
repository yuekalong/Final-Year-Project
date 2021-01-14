// import the service
const hintServices = require("../services/HintServices.js");
const { standardServiceResponse } = require("../utils/ResponseHandler");

module.exports = {
  validatePattern: function (req, res, next) {
    try {
      console.log("HintController.validatePattern started!");

      const { id, input } = req.query;

      return standardServiceResponse(
        res,
        next,
        hintServices.validatePattern(id, input)
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
