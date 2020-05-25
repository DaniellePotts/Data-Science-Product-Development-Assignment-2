const csv = require("csvtojson/v2");

class CsvToJson {
  async convertFile(file) {
    return new Promise((resolve, reject) => {
      try {
        csv()
          .fromFile(file)
          .then((jsonObj) => {
            resolve(jsonObj);
          });
      } catch (err) {
        console.log(err);
      }
    });
  }
  async convertString(string) {
    return new Promise((resolve, reject) => {
      try {
        csv()
          .fromString(string.toString())
          .then((jsonObj) => {
            resolve(jsonObj);
          });
      } catch (err) {
        reject(err);
      }
    }).catch((err) =>{
      Promise.reject(err)
    });
  }
}

module.exports = {
  CsvToJson,
};
