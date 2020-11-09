exports.up = function (knex) {
  return knex.schema.createTable("game", (table) => {
    table.uuid("id").primary();
    table.uuid("area_id");
    table.uuid("hunter_group_id");
    table.uuid("protector_group_id");
    table.uuid("treasure_id");
    table.timestamp("created_at", 6).defaultTo(knex.fn.now(6));

    table.foreign("area_id").references("game_area_location.id");
    table.foreign("hunter_group_id").references("group.id");
    table.foreign("protector_group_id").references("group.id");
    table.foreign("treasure_id").references("treasure.id");
  });
};

exports.down = function (knex) {
  return knex.schema.dropTable("game");
};
