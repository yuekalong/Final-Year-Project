exports.seed = function (knex) {
  // Deletes ALL existing entries
  return knex("hint")
    .del()
    .then(function () {
      // Inserts seed entries
      return knex("hint").insert([
        // big hint
        {
          id: 1,
          hint_words: "hint_1",
          loc_x: 22,
          loc_y: 22,
        },

        // small hint
      ]);
    });
};
