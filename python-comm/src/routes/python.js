const express = require('express');
const router = express.Router();

const PythonController = require('../controllers/pythonController').PythonController;
const PythonService = require('../service/pythonService').PythonService;

router.get("/getPrediction/:name", async (req, res) => {
    const pythonController = new PythonController(new PythonService());

    const response = await pythonController.runScript(1,"../python/hello.py");

    res.sendStatus(200);
})

module.exports = router