const fs = require("fs-extra")
const path = require("path")

class FileHelper{
    async write(filePath, fileName, data){
        if(await this.folderExists(filePath)){
            await fs.writeFileSync(`${filePath}/${fileName}`, data)
        }
    }
    async deleteAll(folder){
        if(await this.folderExists(folder)){
            const files = await fs.readdirSync(folder);
            await Promise.all(files.map(async (file) => {
                if(path.extname(file).toLowerCase() == ".csv") await fs.removeSync(folder + file)
            }))
        }
    }
    async folderExists(folder){
        return await fs.existsSync(folder)
    }
}

module.exports = {
    FileHelper
}