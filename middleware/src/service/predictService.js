const rp = require('request-promise');

class PredictService{
    async getSalesForecast(){
       return await rp('http://localhost:3001/models/getPrediction/test')
    }
}

module.exports = {
    PredictService
}