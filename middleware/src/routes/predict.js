const express = require("express");
const router = express.Router();

const PredictController = require("../controllers/predictController")
  .PredictController;
const PredictService = require("../service/predictService").PredictService;

router.get("/getSalesForecast", async (req, res) => {
  const predictController = new PredictController(new PredictService());

  await predictController.getSalesForecast();

  res.sendStatus(200);
});

module.exports = router;
