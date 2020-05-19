const { PythonShell } = require("python-shell");
class PythonService {
  runScript(scriptPath, script, params) {
    return new Promise((resolve, reject) => {
      PythonShell.defaultOptions = {
        scriptPath: scriptPath
      };
      PythonShell.run(script, params, function (err, results) {
        if (err) return reject(err);
        return resolve(results);
      });
    }).catch((err) => {
      Promise.reject(err);
    });
  }
}

module.exports = { PythonService };
