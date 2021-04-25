exports.seed = function (knex) {
  // Deletes ALL existing entries
  return knex("item")
    .del()
    .then(function () {
      // Inserts seed entries
      return knex("item").insert([
        // same location as hint
        {
          id: 1,
          type: "bomb",
          loc_x: 22.41982,
          loc_y: 114.202897,
        },
        {
          id: 2,
          type: "bomb",
          loc_x: 22.419334,
          loc_y: 114.202972,
        },
        {
          id: 3,
          type: "bomb",
          loc_x: 22.419175,
          loc_y: 114.204977,
        },
        {
          id: 4,
          type: "bomb",
          loc_x: 22.419165,
          loc_y: 114.204473,
        },
        {
          id: 5,
          type: "bomb",
          loc_x: 22.41856,
          loc_y: 114.204098,
        },
        {
          id: 6,
          type: "bomb",
          loc_x: 22.41983,
          loc_y: 114.205642,
        },
        {
          id: 7,
          type: "bomb",
          loc_x: 22.418724,
          loc_y: 114.204769,
        },
        {
          id: 8,
          type: "bomb",
          loc_x: 22.418098,
          loc_y: 114.20478,
        },

        // new location
        {
          id: 9,
          type: "bomb",
          loc_x: 22.419471,
          loc_y: 114.206609,
        },
        {
          id: 10,
          type: "bomb",
          loc_x: 22.41957,
          loc_y: 114.209242,
        },
        {
          id: 11,
          type: "bomb",
          loc_x: 22.419849,
          loc_y: 114.206476,
        },
        {
          id: 12,
          type: "bomb",
          loc_x: 22.41969,
          loc_y: 114.207184,
        },
        {
          id: 13,
          type: "bomb",
          loc_x: 22.419075,
          loc_y: 114.20798,
        },
        {
          id: 14,
          type: "bomb",
          loc_x: 22.419684,
          loc_y: 114.208861,
        },
        {
          id: 15,
          type: "bomb",
          loc_x: 22.419208,
          loc_y: 114.208689,
        },
      ]);
    });
};
