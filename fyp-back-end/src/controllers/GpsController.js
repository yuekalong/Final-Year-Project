// import the service
const gpsServices = require("../services/GpsServices.js");
const { standardServiceResponse } = require("../utils/ResponseHandler");

module.exports = {
  postLocation: function (req, res, next) {
    try {
      console.log("GpsController.postLocation started!");

      const { Lat, Lng, Visible } = req.body;
      return standardServiceResponse(
        res,
        next,
        gpsServices.postLocation(req.params.id, Lat, Lng, Visible)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(err);
      next(err);
    }
  },
  getTeamLocation: function (req, res, next) {
    try {
      console.log("GpsController.getTeamLocation started!");

      return standardServiceResponse(
        res,
        next,
        gpsServices.getTeamLocation(req.params.playerid, req.params.groupid)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(err);
      next(err);
    }
  },
  getOppLocation: function (req, res, next) {
    try {
      console.log("GpsController.getOppLocation started!");

      return standardServiceResponse(
        res,
        next,
        gpsServices.getOppLocation(req.params.groupid)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(err);
      next(err);
    }
  },
  getHintsLocation: function (req, res, next) {
    try {
      console.log("GpsController.getHintsLocation started!");

      return standardServiceResponse(
        res,
        next,
        gpsServices.getHintsLocation(req.params.gameid)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(err);
      next(err);
    }
  },
  removeHintsLocation: function (req, res, next) {
    try {
      console.log("GpsController.removeHintsLocation started!");

      const index = req.body.index;
      const game_id = req.body.game_id;

      return standardServiceResponse(
        res,
        next,
        gpsServices.removeHintsLocation(index,game_id)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(err);
      next(err);
    }
  },
  getItemsLocation: function (req, res, next) {
    try {
      console.log("GpsController.getItemsLocation started!");

      return standardServiceResponse(
        res,
        next,
        gpsServices.getItemsLocation(req.params.gameid)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(err);
      next(err);
    }
  },
  removeItemsLocation: function (req, res, next) {
    try {
      console.log("GpsController.removeItemsLocation started!");

      const index = req.body.index;
      const game_id = req.body.game_id;

      return standardServiceResponse(
        res,
        next,
        gpsServices.removeItemsLocation(index,game_id)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(err);
      next(err);
    }
  },
};
