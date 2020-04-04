const express = require('express');
const router = express.Router();

const PlayerController = require('../controllers/playerController').PlayerController;
const PlayerService = require('../service/playerService').PlayerService;
const MongoClient = require("../database/mongoClient").MongoClient;

router.get("/createSave/:name", (req, res) => {
    const playerController = new PlayerController(new PlayerService(new MongoClient()));

    playerController.createSaveFile(req.params.name);

    res.status(200).send({message: "Save file created!!"})
})

module.exports = router;