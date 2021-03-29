// import express
const express = require("express");
const app = express();

// import socket
const server = app.listen(3000, () =>
  console.log("server started at port 3000!")
);
const io = require("socket.io")(server);

// import bodyParser for get the request body
const bodyParser = require("body-parser");

// import cors to set the CORS
const cors = require("cors");
const corsOptions = {
  origin: true,
  methods: "GET,HEAD,PUT,PATCH,POST,DELETE,OPTIONS",
  allowedHeaders: ["Content-Type", "Authorization"],
};

// make express app use the plugins
app.use(cors(corsOptions));
app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

// import passport and set the passport
const auth = require("./init-passport");

// import routers
const authRouter = require("./routers/AuthRouter.js");
const lobbyRouter = require("./routers/LobbyRouter.js");
const waitingRoomRouter = require("./routers/WaitingRoomRouter.js");
const accountRouter = require("./routers/AccountRouter.js");
const chatroomRouter = require("./routers/ChatroomRouter.js");
const hintRouter = require("./routers/HintRouter.js");
const bombRouter = require("./routers/BombRouter.js");
const treasureRouter = require("./routers/TreasureRouter.js");
const gpsRouter = require("./routers/GpsRouter.js");

// make express app use the router
app.use("/auth", authRouter);
app.use("/lobby", lobbyRouter);
app.use("/gps", gpsRouter);
// app.use("/account", auth.authenticate, accountRouter);
app.use("/account", accountRouter);
app.use("/hint", hintRouter);
app.use("/bomb", bombRouter);
app.use("/treasure", treasureRouter);
app.use(chatroomRouter(io));
app.use(waitingRoomRouter(io));

require("dotenv").config();

// testing route
app.get("/", async (req, res) => {
  // const result = await knex("chatroom_history").select("*");
  res.send({
    status: true,
    data: "Connection ready",
    MAXIMUM_NUMBER_OF_PLAYER: process.env.MAXIMUM_NUMBER_OF_PLAYER,
  });
});
