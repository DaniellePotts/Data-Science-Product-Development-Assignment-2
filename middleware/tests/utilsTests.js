const should = require('should');
const GenerateId = require("../src/utils/generateId").GenerateId;

describe('Testing utils', () => {
  describe('Testing ID generation', () => {
    it('Should generate a random GUID', (done) => {
      const generateId = new GenerateId();
      const id = generateId.generateGuid()

      should.exist(id);

      done()
    });
    it('Should generate two GUIDs that do not match', (done) => {
        const generateId = new GenerateId();
        const guid1 = generateId.generateGuid();
        const guid2 = generateId.generateGuid();

        should.notEqual(guid1, guid2, `Expected GUIDs to be different.\nGUID 1: ${guid1}\nGUID 2: ${guid2}`)
        done();
    })
  });
});