exports.up = function (knex) {
  return knex.schema.dropTableIfExists("game_area_location");
};

exports.down = function (knex) {
  return knex.schema.createTable("game_area_location", (table) => {
    table.uuid("id").primary();
    table.double("loc_x");
    table.double("loc_y");
    table.double("hunter_gather_loc_x");
    table.double("hunter_gather_loc_y");
    table.double("protector_gather_loc_x");
    table.double("protector_gather_loc_y");
  });
};
