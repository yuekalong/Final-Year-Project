exports.seed = function (knex) {
  // Deletes ALL existing entries
  return knex("item")
    .del()
    .then(function () {
      // Inserts seed entries
      return knex("item").insert([
        {
          id: 1,
          type: "bomb",
          loc_x: 22,
          loc_y: 22,
        },
      ]);
    });
};
