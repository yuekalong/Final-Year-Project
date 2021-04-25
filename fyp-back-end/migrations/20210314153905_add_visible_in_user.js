exports.up = function (knex) {
  return knex.schema.alterTable("user", (table) => {
    table.string("visible").default("n");
  });
};

exports.down = function (knex) {
  return knex.schema.alterTable("user", (table) => {
    table.dropColumn("visible");
  });
};
