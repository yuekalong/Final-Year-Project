
exports.up = function(knex) {
    return knex.schema.createTable("game_items_mapping", (table) => {
        table.uuid("game_id");
        table.uuid("item_id");

        table.primary(["game_id", "item_id"]);
        table.foreign('item_id').references('item.id');
    });
  };
  
  exports.down = function(knex) {
      return knex.schema.dropTable("game_items_mapping");
  };
  