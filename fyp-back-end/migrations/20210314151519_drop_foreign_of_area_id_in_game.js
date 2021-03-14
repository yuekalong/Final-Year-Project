exports.up = function (knex) {
  return knex.schema.alterTable("game", (table) => {
    table.dropForeign("area_id");
    table.dropColumn("area_id");
  });
};

exports.down = function (knex) {
  return knex.schema.alterTable("game", (table) => {
    table.string("area_id").after("status");
    table.foreign("area_id").references("game_area_location.id");
  });
};
