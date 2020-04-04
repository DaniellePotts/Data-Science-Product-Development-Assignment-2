const mongoose = require("mongoose");

class MongoClient {
  connect(host, database) {
      const connectionString = `mongodb://${host}/${database}`
      mongoose.connect(connectionString, { useNewUrlParser: true})

      let db = mongoose.connection;
      db.once('open', function() {
        return db;
      });

      db.on('error', console.error.bind(console, 'MongoDB connection error:'));

  }
}

module.exports = { MongoClient };
