exports.up = function (knex) {
  return knex.schema.alterTable("game_hints_mapping", (table) => {
    table.string("group_id").after("pattern_lock_id");
  });
};

exports.down = function (knex) {
  return knex.schema.alterTable("game_hints_mapping", (table) => {
    table.dropColumn("group_id");
  });
};
