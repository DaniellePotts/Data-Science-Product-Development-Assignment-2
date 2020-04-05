const mongoose = require("mongoose");

const Schema = mongoose.Schema;

const player = new Schema({
  lastSaved: Date,
  name: String,
  gameId: String,
  created: Date,
});

module.exports = mongoose.model("players", player);
