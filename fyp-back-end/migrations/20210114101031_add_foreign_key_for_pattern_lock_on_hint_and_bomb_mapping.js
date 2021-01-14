exports.up = function (knex) {
  return Promise.all([
    knex.schema.alterTable("game_hints_mapping", (table) => {
      table.uuid("pattern_lock_id");
      table.foreign("pattern_lock_id").references("pattern_lock.id");
    }),
    knex.schema.alterTable("game_bombs_mapping", (table) => {
      table.uuid("pattern_lock_id");
      table.foreign("pattern_lock_id").references("pattern_lock.id");
    }),
  ]);
};

exports.down = function (knex) {
  return Promise.all([
    knex.schema.alterTable("game_hints_mapping", (table) => {
      table.dropForeign("pattern_lock_id");
      table.dropColumn("pattern_lock_id");
    }),
    knex.schema.alterTable("game_bombs_mapping", (table) => {
      table.dropForeign("pattern_lock_id");
      table.dropColumn("pattern_lock_id");
    }),
  ]);
};
