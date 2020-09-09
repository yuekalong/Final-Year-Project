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

// import routers
const chatroomRouter = require("./routers/ChatroomRouter.js");

// make express app use the router
app.use(chatroomRouter(io));

// testing route
app.get("/", (req, res) => {
  res.send({ status: true });
});
