
exports.up = function(knex) {
    return knex.schema.createTable("treasure", (table) => {
        table.uuid("id").primary();
        table.double("loc_x");
        table.double("loc_y");
    });
  };
  
  exports.down = function(knex) {
      return knex.schema.dropTable("treasure");
  };
  