class PlayerController {
  constructor(playerService) {
    this.playerService = playerService;
  }

  async getSavedFiles() {
    let files = [];

    const saves = await this.playerService.getAllSaves();

    if (saves && Array.isArray(saves) && saves.length > 0) {
      saves.forEach((save) => {
        files.push({
          name: save.name,
          lastSaved: save.lastSaved,
          gameId: save.gameId
        });
      });

      return files;
    }

    return files;
  }
  async createSaveFile(fileName) {
    await this.playerService.createPlayer(fileName);
  }
  async saveFile(gameId) {
      await this.playerService.saveFile(gameId)
  }
}

module.exports = { PlayerController };
