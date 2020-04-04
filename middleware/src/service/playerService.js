const GenerateId = require("../utils/generateId").GenerateId;

class PlayerService{
    constructor(dbClient){
        this.dbClient = dbClient;
    }
    createPlayer(name){
        if (!name) throw "No name exception...";

        const generateId = new GenerateId();
        const now = new Date();
        this.dbClient.connect("127.0.0.1","assignment2");
        return {
            id: generateId.generateGuid(),
            gameId: generateId.generateGuid(),
            name: name, 
            created: now,
            lastSaved : now
        }
    }
}

module.exports = {
    PlayerService
}