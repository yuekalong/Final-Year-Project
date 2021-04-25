exports.up = function (knex) {
  return knex.schema.alterTable("treasure", (table) => {
    table.dropColumn("loc_x");
    table.dropColumn("loc_y");
    table.string("map_number");
  });
};

exports.down = function (knex) {
  return knex.schema.alterTable("treasure", (table) => {
    table.dropColumn("map_number");
    table.double("loc_x");
    table.double("loc_y");
  });
};
