// import the service
const gpsServices = require("../services/GpsServices.js");
const { standardServiceResponse } = require("../utils/ResponseHandler");

module.exports = {
postLocation: function (req, res, next) {
    try {
      console.log("GpsController.getGameInfo started!");
     // const  {userID}  = req.params;
      
      console.log(req.params);
      const { Lat, Lng } = req.body;
      return standardServiceResponse(
        res,
        next,
        gpsServices.postLocation(req.params.id,Lat,Lng)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(err);
      next(err);
    }
  },
};
