exports.seed = function (knex) {
  // Deletes ALL existing entries
  return knex("bomb")
    .del()
    .then(function () {
      // Inserts seed entries
      return knex("bomb").insert([
        {
          id: 1,
          type: "normal",
          range: 5,
        },
        {
          id: 2,
          type: "large",
          range: 10,
        },
      ]);
    });
};
