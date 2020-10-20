
exports.up = function(knex) {
  return knex.schema.createTable("chatroom_history", (table) => {
      table.uuid("id").primary();
      table.uuid("group_id");
      table.string("linetxt");
      table.timestamp("created_at", 6).defaultTo(knex.fn.now(6));
  });
};

exports.down = function(knex) {
    return knex.schema.dropTable("chatroom_history");
};
