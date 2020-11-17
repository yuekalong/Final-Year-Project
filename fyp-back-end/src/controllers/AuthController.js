// import ‘passport.js’ for handling the login request
const passport = require("passport");

// as we use jwt token for our login, we import jwt module
const jwt = require("jsonwebtoken");
const secret_key = "secret";

module.exports = {
  //  this is for log in request
  login: async function (req, res, next) {
    try {
      console.log("AuthController.login started!");
      // we use passport for our authentication
      return passport.authenticate(
        "local",
        { session: false },
        (err, user, info) => {
          // if there have error occur we immediately send log out signal to our request
          if (err) {
            req.logOut();
            return next(err);
          }

          // if the user is null we immediately send log out signal to our request
          if (!user) {
            req.logOut();
            return res.json({
              success: false,
              message: info.message,
            });
          }

          // send the login signal to our request
          req.login(user, { session: false }, async (err) => {
            // if error occur assign error to next function
            if (err) {
              return next(err);
            }

            // if no error occur
            try {
              // assign the jwt token
              const token = jwt.sign(
                {
                  sub: user,
                  exp: Math.floor(Date.now() / 1000) + 60 * 360,
                },
                secret_key
              );
              // return the response
              return res.json({
                success: true,
                data: {
                  userInfo: user,
                  token: token,
                },
              });
            } catch (e) {
              // if error occur assign error to next function
              return next(e);
            }
          });
        }
      )(req, res, next);
    } catch (err) {
      // if error occur console.log the error
      console.log(
        "Error: AuthController.login: " + JSON.parse(err.message)["message"]
      );

      // assign error to next function
      next(err);
    }
  },
};
