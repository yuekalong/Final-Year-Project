exports.up = function (knex) {
  return knex.schema.createTable("pattern_lock", (table) => {
    table.uuid("id").primary();
    table.text("order");
    table.timestamp("created_at", 6).defaultTo(knex.fn.now(6));
  });
};

exports.down = function (knex) {
  return knex.schema.dropTable("pattern_lock");
};
