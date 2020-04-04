const express = require("express");
const cors = require("cors");
const bodyParser = require("body-parser");
const app = express();

app.use(bodyParser.urlencoded({ extended: false }));
app.use(cors());

const port = 3000;

app.use("/player", require("./routes/player"));

app.listen(port, () =>
  console.log(`Example app listening at http://localhost:${port}`)
);
