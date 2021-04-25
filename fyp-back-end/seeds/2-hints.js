exports.seed = function (knex) {
  // Deletes ALL existing entries
  return knex("hint")
    .del()
    .then(function () {
      // Inserts seed entries
      return knex("hint").insert([
        {
          id: 1,
          hint_words: "hint_1",
          loc_x: 22.41982,
          loc_y: 114.202897,
        },
        {
          id: 2,
          hint_words: "hint_2",
          loc_x: 22.419334,
          loc_y: 114.202972,
        },
        {
          id: 3,
          hint_words: "hint_3",
          loc_x: 22.419175,
          loc_y: 114.204977,
        },
        {
          id: 4,
          hint_words: "hint_4",
          loc_x: 22.419165,
          loc_y: 114.204473,
        },
        {
          id: 5,
          hint_words: "hint_5",
          loc_x: 22.41856,
          loc_y: 114.204098,
        },
        {
          id: 6,
          hint_words: "hint_6",
          loc_x: 22.41983,
          loc_y: 114.205642,
        },
        {
          id: 7,
          hint_words: "hint_7",
          loc_x: 22.418724,
          loc_y: 114.204769,
        },
        {
          id: 8,
          hint_words: "hint_8",
          loc_x: 22.418098,
          loc_y: 114.20478,
        },
        {
          id: 9,
          hint_words: "hint_9",
          loc_x: 22.418366,
          loc_y: 114.205595,
        },
        {
          id: 10,
          hint_words: "hint_10",
          loc_x: 22.418952,
          loc_y: 114.206313,
        },
        {
          id: 11,
          hint_words: "hint_11",
          loc_x: 22.419849,
          loc_y: 114.206476,
        },
      ]);
    });
};
