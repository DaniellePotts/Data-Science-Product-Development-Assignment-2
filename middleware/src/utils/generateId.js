const { v4: uuidv4 } = require('uuid');

class GenerateId{
    generateGuid(){
        return uuidv4();
    }
}

module.exports = {
    GenerateId
}