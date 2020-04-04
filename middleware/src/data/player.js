const mongoose = require('mongoose');

const Schema = mongoose.Schema;

const player = new Schema({
    id: String,
    gameId: String,
    started: Date,
    lastSaved: Date,
    name: String
})

const PlayerModel = mongoose.model('Player', Player );