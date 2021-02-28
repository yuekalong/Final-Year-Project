exports.up = function (knex) {
  return knex.schema.alterTable("game", (table) => {
    table.string("status").defaultTo("waiting").after("id");
  });
};

exports.down = function (knex) {
  return knex.schema.alterTable("game", (table) => {
    table.dropColumn("status");
  });
};
