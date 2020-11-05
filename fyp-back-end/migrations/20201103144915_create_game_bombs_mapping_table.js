
exports.up = function(knex) {
    return knex.schema.createTable("game_bombs_mapping", (table) => {
        table.uuid("game_id");
        table.uuid("bomb_id");
        table.double("loc_x");
        table.double("loc_y");

        table.primary(["game_id", "bomb_id"]);
        table.foreign('bomb_id').references('bomb.id');
    });
  };
  
  exports.down = function(knex) {
      return knex.schema.dropTable("game_bombs_mapping");
  };
  