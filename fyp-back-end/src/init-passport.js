const knex = require("knex")(require("../knexfile.js")["development"]);
const passport = require("passport");
const LocalStrategy = require("passport-local").Strategy;
const passportJWT = require("passport-jwt");

const JWTStrategy = passportJWT.Strategy;
const extractJWT = passportJWT.ExtractJwt;
// const bcrypt = require("bcrypt");
const secret_key = "secret";
const loginStrategy = new LocalStrategy(
  {
    usernameField: "username",
    passwordField: "password",
  },
  async function (username, password, done) {
    try {
      console.log("alrgiht");
      let users = await knex.raw(
        `
            SELECT *
            FROM user
            WHERE BINARY name = ?
      `,
        [username]
      );
      if (users.length == 0) {
        return done(null, false, { message: "Invalid Credentials" });
      }
      let dbUser = users[0][0];
      // if (await bcrypt.compare(password, dbUser.password)) {
      if (password == dbUser.password) {
        const userInfo = await knex("user")
          .first("id", "name", "game_status")
          .where({ name: username });
        return done(null, userInfo);
      }
      return done(null, false, { message: "Invalid Credentials" });
    } catch (err) {
      return done(err);
    }
  }
);

const jwtStrategy = new JWTStrategy(
  {
    jwtFromRequest: extractJWT.fromAuthHeaderAsBearerToken(),
    secretOrKey: secret_key,
  },
  (jwtPayload, done) => {
    if (jwtPayload.sub && jwtPayload.exp > Date.now() / 1000) {
      return done(null, jwtPayload.sub);
    } else {
      return done(
        new Error(JSON.stringify({ statusCode: 404, message: `jwt invalid` })),
        null
      );
    }
  }
);

passport.use("local", loginStrategy);
passport.use("jwt", jwtStrategy);

passport.serializeUser((user, done) => {
  done(null, user.id);
});

passport.deserializeUser(function (user, done) {
  done(null, user);
});

module.exports = {
  initialize: () => passport.initialize(),
  session: () => passport.session(),
  authenticate: function (req, res, next) {
    return passport.authenticate(
      "jwt",
      { session: false },
      (err, user, info) => {
        if (err) {
          return next(
            res.json({ statusCode: 500, message: "Passport failed" })
          );
        } else if (!user) {
          return next(
            res.json({
              statusCode: 401,
              message: `${info["message"]}`,
            })
          );
        } else {
          console.log(`Request made by: ${user}`);
          return next();
        }
      }
    )(req, res, next);
  },
};
