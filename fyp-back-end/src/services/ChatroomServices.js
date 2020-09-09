module.exports = {
  joinRoom: async function (socket, io) {
    socket.join();
    console.log("room join!");
  },
};
