exports.up = function (knex) {
  return knex.schema.alterTable("game", (table) => {
    table.string("map_number").after("status");
  });
};

exports.down = function (knex) {
  return knex.schema.alterTable("game", (table) => {
    table.dropColumn("map_number");
  });
};
