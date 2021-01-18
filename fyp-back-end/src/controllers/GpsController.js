// import the service
const gpsServices = require("../services/GpsServices.js");
const { standardServiceResponse } = require("../utils/ResponseHandler");

module.exports = {
postLocation: function (req, res, next) {
    try {
      console.log("GpsController.getGameInfo started!");
     // const  {userID}  = req.params;
      
      console.log(req.params);
      return standardServiceResponse(
        res,
        next,
        gpsServices.postLocation(req.params.id,req.params.locx,req.params.locy)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(err);
      next(err);
    }
  },
};
