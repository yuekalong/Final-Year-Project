exports.up = function (knex) {
  return knex.schema.alterTable("user", (table) => {
    table.string("game_status").defaultTo("free").alter();
  });
};

exports.down = function (knex) {
  return knex.schema.alterTable("user", (table) => {
    table.string("game_status").defaultTo("waiting").alter();
  });
};
