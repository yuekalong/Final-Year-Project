// import the service
const lobbyServices = require("../services/LobbyServices.js");
const { standardServiceResponse } = require("../utils/ResponseHandler");

module.exports = {
  getRoom: function (req, res, next) {
    try {
      console.log("LobbyContoller.getRoom started!");

      return standardServiceResponse(res, next, lobbyServices.getRoom());
    } catch (err) {
      // catch exception and shows the error message
      console.log(
        "Error: LobbyContoller.getRoom: " + JSON.parse(err.message)["message"]
      );
      next(err);
    }
  },
  createRoom: function (req, res, next) {
    try {
      console.log("LobbyContoller.createRoom started!");

      return standardServiceResponse(res, next, lobbyServices.createRoom());
    } catch (err) {
      // catch exception and shows the error message
      console.log(
        "Error: LobbyContoller.createRoom: " +
          JSON.parse(err.message)["message"]
      );
      next(err);
    }
  },
  joinRoom: function (req, res, next) {
    try {
      console.log("LobbyContoller.joinRoom started!");

      const { userID, gameID } = req.body;

      console.log(userID, gameID);
      return standardServiceResponse(
        res,
        next,
        lobbyServices.joinRoom(userID, gameID)
      );
    } catch (err) {
      // catch exception and shows the error message
      console.log(
        "Error: LobbyContoller.joinRoom: " + JSON.parse(err.message)["message"]
      );
      next(err);
    }
  },
};
