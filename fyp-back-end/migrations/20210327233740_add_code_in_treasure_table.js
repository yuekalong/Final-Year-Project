exports.up = function (knex) {
  return knex.schema.alterTable("treasure", (table) => {
    table.string("code").after("id");
  });
};

exports.down = function (knex) {
  return knex.schema.alterTable("treasure", (table) => {
    table.dropColumn("code");
  });
};
