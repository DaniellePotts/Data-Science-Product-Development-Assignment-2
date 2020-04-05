const express = require('express');
const router = express.Router();

const PlayerController = require('../controllers/playerController').PlayerController;
const PlayerService = require('../service/playerService').PlayerService;
const MongoClient = require("../database/mongoClient").MongoClient;

router.post("/createSave/:name", async (req, res) => {
    const playerController = new PlayerController(new PlayerService(new MongoClient()));

    await playerController.createSaveFile(req.params.name);

    res.sendStatus(200);
})

router.get("/allSaves", async (req, res) => {
    const playerController = new PlayerController(new PlayerService(new MongoClient()));

    const response = await playerController.getSavedFiles();

    res.status(200).send(JSON.stringify(response))
})

router.put("/saveFile/:gameId", async (req, res) => {
    const playerController = new PlayerController(new PlayerService(new MongoClient()));

    await playerController.saveFile(req.params.gameId);

    res.sendStatus(200)
})

module.exports = router;