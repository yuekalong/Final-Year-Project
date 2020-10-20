require('dotenv').config();

module.exports = {
  development: {
    client: "mysql",
    version: "8.0.19",
    connection: {
      host: "127.0.0.1",
      port: 3306,
      user: process.env.DB_DEVELOPMENT_USER,
      password: process.env.DB_DEVELOPMENT_PASSWORD,
      database: process.env.DB_DEVELOPMENT_DBNAME,
    },
    debug: true,
    migrations: {
      tableName: "knex_migrations",
    },
  },
};
