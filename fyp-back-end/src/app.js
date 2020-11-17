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
const chatroomRouter = require("./routers/ChatroomRouter.js");

// make express app use the router
app.use("/auth", authRouter);
app.use(chatroomRouter(io));

// testing route
app.get("/", async (req, res) => {
  const result = await knex("chatroom_history").select("*");
  res.send({ status: true, data: result });
});
