const { PythonShell } = require("python-shell");

class PythonService {
  runScript(requestParams, script) {
    return new Promise((resolve, reject) => {
    //     console.log(__dirname)
    //   PythonShell.run("/python/main.py", null, function (err, results) {
    //       console.log(err)
    //     if (err) return reject(err);
    //     return resolve(results);
    //   });
    
    resolve(200)
    });
  }
}

module.exports = { PythonService };
