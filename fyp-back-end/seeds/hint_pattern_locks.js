exports.seed = function (knex) {
  // Deletes ALL existing entries
  return knex("pattern_lock")
    .del()
    .then(function () {
      // Inserts seed entries
      return knex("pattern_lock").insert([
        {
          id: "hint82ea-ab8f-4c31-9d56-7dbd5f946c9d",
          type: "hint",
          order: "[0,1,3,4,5,7,8,6,2]",
        },
        {
          id: "hint849a-dd41-4d85-9ae5-eb1813090970",
          type: "hint",
          order: "[1,5,9,4,8,2,6,3,7]",
        },
        {
          id: "hint67f2-8f2e-4e20-846b-ed835a51de52 ",
          type: "hint",
          order: "[1,6,8,4,2,9,3,7]",
        },
      ]);
    });
};
