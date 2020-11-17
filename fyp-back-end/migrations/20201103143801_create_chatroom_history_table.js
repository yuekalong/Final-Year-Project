exports.up = function (knex) {
  return knex.schema.createTable("chatroom_history", (table) => {
    table.uuid("id").primary();
    table.uuid("group_id");
    table.uuid("user_id");
    table.string("linetxt");
    table.timestamp("created_at", 6).defaultTo(knex.fn.now(6));

    table.foreign("group_id").references("group.id");
    table.foreign("user_id").references("user.id");
  });
};

exports.down = function (knex) {
  return knex.schema.dropTable("chatroom_history");
};
