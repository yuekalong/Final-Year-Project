exports.up = function (knex) {
  return Promise.all([
    knex.schema.alterTable("game_hints_mapping", (table) => {
      table.foreign("game_id").references("game.id");
    }),
    knex.schema.alterTable("game_items_mapping", (table) => {
      table.foreign("game_id").references("game.id");
    }),
    knex.schema.alterTable("game_bombs_mapping", (table) => {
      table.foreign("game_id").references("game.id");
    }),
  ]);
};

exports.down = function (knex) {
  return Promise.all([
    knex.schema.alterTable("game_hints_mapping", (table) => {
      table.dropForeign("game_id");
    }),
    knex.schema.alterTable("game_items_mapping", (table) => {
      table.dropForeign("game_id");
    }),
    knex.schema.alterTable("game_bombs_mapping", (table) => {
      table.dropForeign("game_id");
    }),
  ]);
};
