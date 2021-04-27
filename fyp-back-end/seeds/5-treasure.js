exports.seed = function (knex) {
  // Deletes ALL existing entries
  return knex("treasure")
    .del()
    .then(function () {
      // Inserts seed entries
      return knex("treasure").insert([
        {
          id: 1,
          code: "1",
          map_number: "1",
        },
        {
          id: 2,
          code: "2",
          map_number: "1",
        },
        {
          id: 3,
          code: "3",
          map_number: "1",
        },
        {
          id: 4,
          code: "4",
          map_number: "2",
        },
        {
          id: 5,
          code: "5",
          map_number: "2",
        },
        {
          id: 6,
          code: "6",
          map_number: "2",
        },
      ]);
    });
};
