
exports.up = function(knex) {
    return knex.schema.createTable("bomb", (table) => {
        table.uuid("id").primary();
        table.string("type");
        table.double("range");
    });
  };
  
  exports.down = function(knex) {
      return knex.schema.dropTable("bomb");
  };
  