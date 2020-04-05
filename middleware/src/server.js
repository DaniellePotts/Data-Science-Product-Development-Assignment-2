const express = require("express");
const cors = require("cors");
const bodyParser = require("body-parser");
const app = express();
const mongoose = require("mongoose");

app.use(bodyParser.urlencoded({ extended: false }));
app.use(cors());
const fs = require('fs');


const port = 3000;
mongoose.connect("mongodb://127.0.0.1/assignment2",{ useNewUrlParser: true, useUnifiedTopology: true})

app.use("/player", require("./routes/player"));
app.use("/forecast", require("./routes/predict"));

app.listen(port, () =>
  console.log(`Example app listening at http://localhost:${port}`)
);
