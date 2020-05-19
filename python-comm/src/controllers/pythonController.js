class PythonController {
    constructor(service){
        this.service = service;
    }
    async runScript(scriptPath, script, params){
        return new Promise((resolve, reject) => {
            this.service.runScript(scriptPath,script, params).then((result)=> {
                resolve(result);
            }).catch((err)=> {
                reject(err)
            })
        })
    }
}

module.exports = {
    PythonController
}