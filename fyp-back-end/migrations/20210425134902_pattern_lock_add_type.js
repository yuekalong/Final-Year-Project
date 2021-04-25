exports.up = function (knex) {
  return knex.schema.alterTable("pattern_lock", (table) => {
    table.string("type").after("id").default("player");
  });
};

exports.down = function (knex) {
  return knex.schema.alterTable("pattern_lock", (table) => {
    table.dropColumn("type");
  });
};
