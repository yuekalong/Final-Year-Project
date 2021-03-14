exports.up = function (knex) {
  return knex.schema.alterTable("user", (table) => {
    table.boolean("visible").default(false);
  });
};

exports.down = function (knex) {
  return knex.schema.alterTable("user", (table) => {
    table.dropColumn("visible");
  });
};
