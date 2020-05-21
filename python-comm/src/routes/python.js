const express = require("express");
const router = express.Router();
const path = require("path");

const JsonToCsv = require("../utils/jsonToCsv").JsonToCsv;
const CsvToJson = require("../utils/csvToJson").CsvToJson;
const DateUtils = require("../utils/dateUtils").DateUtils;

const PythonController = require("../controllers/pythonController")
  .PythonController;
const PythonService = require("../service/pythonService").PythonService;
const FileHelper = require("../utils/fileHelper").FileHelper;

router.post("/getPrediction", async (req, res) => {
  try {
    const fileHelper = new FileHelper();

    const pythonController = new PythonController(new PythonService());
    const jsonToCsv = new JsonToCsv();
    if (req.body.data) {
      const parsedJson = JSON.parse(req.body.data);
      if (parsedJson.Items) {
        const items = parsedJson.Items;

        if (items.length > 0) {
          let toCsv = [];

          let dateUtils = new DateUtils();
          items.forEach((item) => {
            i = JSON.parse(item);
            i.Date = dateUtils.convertDateFormat(i.Month, i.Day, i.Year);
            toCsv.push(i);
          });
          const csv = await jsonToCsv.parseJson(toCsv);

          const storageFolder = path.join(__dirname, "../dataFiles/forecast/");

          await fileHelper.write(storageFolder, "request.csv", csv);

          const scriptPath = path.join(
            __dirname,
            "../python/TimeSeriesForecast"
          );
          const script = "forecast.py";

          await pythonController.runScript(scriptPath, script);
          const csvToJson = new CsvToJson();

          const result = await csvToJson.convertFile(
            path.join(__dirname, "../dataFiles/forecast/forecast_response.csv")
          );

          await fileHelper.deleteAll(path.join(__dirname, "../dataFiles/forecast/"));
          res.status(200).send(result);
        }
      }
    }
  } catch (err) {
    console.log(err);
  }
});

router.get("/shoppers/:season", async (req, res) => {
  try {
    const pythonController = new PythonController(new PythonService());
    const fileHelper = new FileHelper();
    const rounds = parseInt(Math.floor(Math.random() * Math.floor(7)));
    const scriptPath = path.join(__dirname, "../python/RL/shopping");
    const script = "main.py";
    let options = {
      args: [req.params.season, rounds],
    };
    console.log(options);
    await pythonController.runScript(scriptPath, script, options);

    const csvToJson = new CsvToJson();
    let csvConverted = await csvToJson.convertFile(
      path.join(__dirname, "../dataFiles/RL/rl_response.csv")
    );

    await fileHelper.deleteAll(path.join(__dirname, "../dataFiles/RL/"));

    res.status(200).send(csvConverted);
  } catch (err) {
    console.log(err);
  }
});

module.exports = router;
