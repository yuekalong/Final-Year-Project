const knex = require("knex")(require("../../knexfile.js")["development"]);

// for encrypt the password
const bcrypt = require("bcrypt");

// for generate uuid
const { v4: uuidv4 } = require("uuid");

module.exports = {
  signUp: async function (username, password) {
    // const hashedPassword = await bcrypt.hash(password, 10);

    return await knex("user").insert({
      id: uuidv4(),
      name: username,
      password: password, //hashedPassword,
    });
  },
};
