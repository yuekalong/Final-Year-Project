exports.up = function (knex) {
  return knex.schema.alterTable("user", (table) => {
    table.double("loc_x").after("game_status");
    table.double("loc_y").after("loc_x");
  });
};

exports.down = function (knex) {
  return knex.schema.alterTable("user", (table) => {
    table.dropColumn("loc_x");
    table.dropColumn("loc_y");
  });
};
