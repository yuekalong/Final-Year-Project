
exports.up = function(knex) {
    return knex.schema.createTable("hint", (table) => {
        table.uuid("id").primary();
        table.text("hint_words");
        table.double("loc_x");
        table.double("loc_y");
    });
  };
  
  exports.down = function(knex) {
      return knex.schema.dropTable("hint");
  };
  