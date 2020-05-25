const { parse } = require('json2csv');

class JsonToCsv {
  async parseJson(data) {
      return new Promise((resolve, reject) => {
        try {
            const csv = parse(data);
            resolve(csv);
          } catch (err) {
            reject(err);
          }
      })
  }
}

module.exports = {
    JsonToCsv
}