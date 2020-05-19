const express = require('express')
const bodyParser = require('body-parser')
const cors = require('cors')
const app = express()
const port = 3001

app.use(bodyParser.json({ type: 'application/json' }))
app.use(bodyParser.urlencoded({ extended: true }));
app.use(cors())

app.use('/models', require("./routes/python"))

app.listen(port, () => console.log(`Example app listening at http://localhost:${port}`))