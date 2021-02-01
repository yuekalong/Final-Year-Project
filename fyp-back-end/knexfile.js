require('dotenv').config();

module.exports = {
  development: {
    client: "mysql",
    version: "8.0.19",
    connection: {
      host: "127.0.0.1",
      port: 3306,
      user: "root",
      password: "1234",
      database: "fypdb",
    },
    debug: true,
    migrations: {
      tableName: "knex_migrations",
    },
  },
};
