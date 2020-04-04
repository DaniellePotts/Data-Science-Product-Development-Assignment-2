class PlayerController {
    constructor(playerService){
        this.playerService = playerService
    }

    getSavedFiles(){

    }
    loadSaveFile(){

    }
    createSaveFile(fileName){
        const newFile = this.playerService.createPlayer(fileName);
        console.log(newFile)
    }
    saveFile(){

    }
}

module.exports = { PlayerController };
