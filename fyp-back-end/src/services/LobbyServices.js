const knex = require("knex")(require("../../knexfile.js")["development"]);

// const { v4: uuidv4 } = require("uuid");
const { customAlphabet } = require("nanoid/async");
const alphabet = "0123456789";
const nanoid = customAlphabet(alphabet, 10);

const roomid = customAlphabet(alphabet, 6);

// random number
function random(max) {
  return Math.floor(Math.random() * max);
}

// random array
function getRandom(arr, n) {
  let result = new Array(n);
  let len = arr.length;
  let taken = new Array(len);

  if (n > len) throw new RangeError("n is larger than array's size");
  while (n--) {
    var x = Math.floor(Math.random() * len);
    result[n] = arr[x in taken ? taken[x] : x];
    taken[x] = --len in taken ? taken[len] : len;
  }
  return result;
}

module.exports = {
  getRoom: async function () {
    const gameInfo = await knex("game")
      .select(
        "game.id as game_id",
        "game.map_number as map_number",
        "hunter_group_id",
        "protector_group_id"
      )
      .where("status", "=", "waiting");

    let rooms = [];

    for (let room of gameInfo) {
      let hunterCount = 0;
      if (!!room.hunter_group_id) {
        hunterCount = (
          await knex("group")
            .count("user_id as count")
            .groupBy("id")
            .where("id", "=", room.hunter_group_id)
        )[0].count;
      }

      let protectorCount = 0;
      if (!!room.protector_group_id) {
        protectorCount = (
          await knex("group")
            .count("user_id as count")
            .groupBy("id")
            .where("id", "=", room.protector_group_id)
        )[0].count;
      }

      const lobbyInfo = {
        game_id: room.game_id,
        map_number: room.map_number,
        player_count: hunterCount + protectorCount,
        hunter_group_id: room.hunter_group_id,
        hunter_count: hunterCount,
        protector_group_id: room.protector_group_id,
        protector_count: protectorCount,
      };

      rooms.push(lobbyInfo);
    }

    return rooms;
  },
  createRoom: async function (mapNumber) {
    const roomID = await roomid();

    const hints = await knex("hint").select("*");
    const items = await knex("item").select("*");
    const treasure = await knex("treasure")
      .select("*")
      .where("map_number", "=", mapNumber);

    const selectedTreasureId = treasure[random(treasure.length)].id;

    // init game data
    await knex("game").insert({
      id: roomID,
      map_number: mapNumber,
      treasure_id: selectedTreasureId,
    });

    // init game hint mapping
    let randomizeHints = getRandom(hints, 11);
    randomizeHints = randomizeHints.map((hint) => {
      return {
        game_id: roomID,
        hint_id: hint.id,
      };
    });

    // TODO: change the schema.... actually big hint should contain critial word
    // init big hint
    const hint_pattern_locks = await knex("pattern_lock")
      .select("*")
      .where("type", "=", "hint");
    randomizeHints[0] = {
      ...randomizeHints[0],
      ...{
        pattern_lock_id:
          hint_pattern_locks[random(hint_pattern_locks.length)].id,
      },
    };

    await knex("game_hints_mapping").insert(randomizeHints);

    // init game item mapping
    let randomizeItems = getRandom(items, 10);
    randomizeItems = randomizeItems.map((item) => {
      return {
        game_id: roomID,
        item_id: item.id,
      };
    });

    await knex("game_items_mapping").insert(randomizeItems);

    return roomID;
  },
  joinRoom: async function (userID, gameID) {
    // TODO: get all user id and check already in room or not

    const roomDetail = await knex("game").first("*").where("id", "=", gameID);

    // if no hunter group
    if (!roomDetail.hunter_group_id) {
      // generate hunter id
      const hunterID = await nanoid();

      // insert hunter group
      await knex("group").insert({
        id: hunterID,
        user_id: userID,
        type: "hunter",
      });

      await knex("game")
        .where("id", "=", gameID)
        .update({ hunter_group_id: hunterID });

      await knex("user")
        .update({ game_status: "waiting" })
        .where("id", "=", userID);

      return {
        group_type: "hunter",
        group_id: hunterID,
      };
    }
    // if no protector group
    else if (!roomDetail.protector_group_id) {
      // generate protector id
      const protectorID = await nanoid();

      // insert protector group
      await knex("group").insert({
        id: protectorID,
        user_id: userID,
        type: "protector",
      });

      await knex("game")
        .where("id", "=", gameID)
        .update({ protector_group_id: protectorID });

      await knex("user")
        .update({ game_status: "waiting" })
        .where("id", "=", userID);

      return {
        group_type: "protector",
        group_id: protectorID,
      };
    }
    // if both group having at least 1 person
    else {
      const hunterCount = (
        await knex("group")
          .count("user_id as count")
          .groupBy("id")
          .where("id", "=", roomDetail.hunter_group_id)
      )[0].count;

      if (hunterCount < 3) {
        // insert hunter group
        await knex("group").insert({
          id: roomDetail.hunter_group_id,
          user_id: userID,
          type: "hunter",
        });

        await knex("user")
          .update({ game_status: "waiting" })
          .where("id", "=", userID);

        return {
          group_type: "hunter",
          group_id: roomDetail.hunter_group_id,
        };
      }

      const protectorCount = (
        await knex("group")
          .count("user_id as count")
          .groupBy("id")
          .where("id", "=", roomDetail.protector_group_id)
      )[0].count;

      if (protectorCount < 3) {
        // insert hunter group
        await knex("group").insert({
          id: roomDetail.protector_group_id,
          user_id: userID,
          type: "protector",
        });

        await knex("user")
          .update({ game_status: "waiting" })
          .where("id", "=", userID);

        return {
          group_type: "protector",
          group_id: roomDetail.protector_group_id,
        };
      }

      // if room full
      return false;
    }
  },
};
