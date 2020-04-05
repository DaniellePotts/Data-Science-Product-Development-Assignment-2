class PredictController{
    constructor(service){
        this.service = service;
    }
    async getSalesForecast(){
        return await this.service.getSalesForecast()
    }
}

module.exports = {
    PredictController
}