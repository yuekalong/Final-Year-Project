exports.up = function (knex) {
  return knex.schema.alterTable("game_bombs_mapping", (table) => {
    table.dropForeign("game_id");
    table.dropForeign("bomb_id");
    table.dropPrimary();
    table.foreign("game_id").references("game.id");
    table.foreign("bomb_id").references("bomb.id");
    table.primary(["game_id", "pattern_lock_id"]);
  });
};

exports.down = function (knex) {
  return knex.schema.alterTable("game_bombs_mapping", (table) => {
    table.dropForeign("game_id");
    table.dropForeign("pattern_lock_id");
    table.dropPrimary();
    table.primary(["game_id", "bomb_id"]);
    table.foreign("game_id").references("game.id");
    table.foreign("pattern_lock_id").references("pattern_lock.id");
  });
};
