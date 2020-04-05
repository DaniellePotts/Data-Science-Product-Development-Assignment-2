class PythonController {
    constructor(service){
        this.service = service;
    }
    async runScript(params, script){
        const result = await this.service.runScript(params,script)
    }
}

module.exports = {
    PythonController
}