
exports.up = function(knex) {
    return knex.schema.createTable("game", (table) => {
        table.uuid("id");
        table.double("loc_x");
        table.string("linetxt");
        table.timestamp("created_at", 6).defaultTo(knex.fn.now(6));

        table.primary(["group_id", "user_id"]);
        table.foreign('group_id').references('group.id');
        table.foreign('user_id').references('user.id');
    });
  };
  
  exports.down = function(knex) {
      return knex.schema.dropTable("game");
  };
  