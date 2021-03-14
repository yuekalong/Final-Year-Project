exports.up = function (knex) {
  return knex.schema.alterTable("game_bombs_mapping", (table) => {
    table.string("group_id");
    table.foreign("group_id").references("group.id");
  });
};

exports.down = function (knex) {
  return knex.schema.alterTable("game_bombs_mapping", (table) => {
    table.dropForeign("group_id");
    table.dropColumn("group_id");
  });
};
