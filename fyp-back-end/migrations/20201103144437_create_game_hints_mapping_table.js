
exports.up = function(knex) {
    return knex.schema.createTable("game_hints_mapping", (table) => {
        table.uuid("game_id");
        table.uuid("hint_id");

        table.primary(["game_id", "hint_id"]);
        table.foreign("hint_id").references("hint.id");
    });
  };
  
  exports.down = function(knex) {
      return knex.schema.dropTable("game_hints_mapping");
  };
  