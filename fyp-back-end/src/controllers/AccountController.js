// import the service
const accountServices = require("../services/AccountServices.js");
const { standardServiceResponse } = require("../utils/ResponseHandler");

module.exports = {
  getGameInfo: function (req, res, next) {
    try {
      console.log("AccountController.getGameInfo started!");
      const { userID } = req.params;
      return standardServiceResponse(
        res,
        next,
        accountServices.getGameInfo(userID)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(
        "Error: AccountController.getGameInfo: " +
          JSON.parse(err.message)["message"]
      );
      next(err);
    }
  },
};
