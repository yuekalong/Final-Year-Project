
exports.up = function(knex) {
    return knex.schema.createTable("item", (table) => {
        table.uuid("id").primary();
        table.string("type");
        table.double("loc_x");
        table.double("loc_y");
    });
  };
  
  exports.down = function(knex) {
      return knex.schema.dropTable("item");
  };
  