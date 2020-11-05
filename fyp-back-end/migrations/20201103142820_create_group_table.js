
exports.up = function(knex) {
    return knex.schema.createTable("group", (table) => {
        table.uuid("id");
        table.uuid("user_id");
        table.string("type");
        table.timestamp("created_at", 6).defaultTo(knex.fn.now(6));

        table.primary(["id", "user_id"]);
        table.foreign('user_id').references('user.id');
    });
  };
  
  exports.down = function(knex) {
      return knex.schema.dropTable("group");
  };
  