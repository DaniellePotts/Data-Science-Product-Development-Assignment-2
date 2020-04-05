const GenerateId = require("../utils/generateId").GenerateId;
const Player = require("../models/player");

class PlayerService {
  constructor(dbClient) {
    this.dbClient = dbClient;
  }
  async createPlayer(name) {
    if (!name) throw "No name exception...";

    const generateId = new GenerateId();
    const now = new Date();

    const player = new Player({
      id: generateId.generateGuid(),
      gameId: generateId.generateGuid(),
      name: name,
      created: now,
      lastSaved: now,
    });

    await player.save()
  }

  async getAllSaves(){
      try{
    return await Player.find()
      }catch(err){
          console.log(err)
      }
  }

  async saveFile(gameId){
     const query = {gameId : gameId}
     const set = {$set:{lastSaved: Date.now()}}

     await Player.findOneAndUpdate(query, set);
  }
}

module.exports = {
  PlayerService,
};
