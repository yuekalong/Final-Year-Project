
exports.up = function(knex) {
    return knex.schema.createTable("user", (table) => {
        table.uuid("id").primary();
        table.string("name");
        table.string("password");
        table.string("game_status");
        table.timestamp("created_at", 6).defaultTo(knex.fn.now(6));
    });
  };
  
  exports.down = function(knex) {
      return knex.schema.dropTable("user");
  };
  