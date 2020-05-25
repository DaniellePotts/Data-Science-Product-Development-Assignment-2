const should = require("should");

const CsvToJson = require("../src/utils/csvToJson").CsvToJson;
const DateUtils = require("../src/utils/dateUtils").DateUtils;
const JsonToCsv = require("../src/utils/jsonToCsv").JsonToCsv;
const FileHelper = require("../src/utils/fileHelper").FileHelper;

const fs = require("fs-extra");

describe("Csv Converter Tests", function () {
  before(async () => {
    const testCsv = ",title,name\n0,test,danielle";
    await fs.writeFileSync("test.csv", testCsv);
  });
  it("Should convert csv string to a JSON", async () => {
    const csvConverter = new CsvToJson();
    const testCsv = ",title,name\n0,test,danielle";

    const expected = { title: "test", name: "danielle" };
    const actual = await csvConverter.convertString(testCsv);

    should.exist(actual[0]);
    should.exist(actual[0].name);
    should.exist(actual[0].title);

    should.equal(
      actual[0].name,
      expected.name,
      `Expected name to equal ${expected.name} but it was ${actual[0].name}`
    );
    should.equal(
      actual[0].title,
      expected.title,
      `Expected title to equal ${expected.title} but it was ${actual[0].title}`
    );
  });

  it("Should convert csv file to json", async () => {
    const csvConverter = new CsvToJson();

    const expected = { title: "test", name: "danielle" };
    const actual = await csvConverter.convertFile("test.csv");

    should.exist(actual[0]);
    should.exist(actual[0].name);
    should.exist(actual[0].title);

    should.equal(
      actual[0].name,
      expected.name,
      `Expected name to equal ${expected.name} but it was ${actual[0].name}`
    );
    should.equal(
      actual[0].title,
      expected.title,
      `Expected title to equal ${expected.title} but it was ${actual[0].title}`
    );
  });

  after(async () => {
    await fs.removeSync("test.csv");
  });
});

describe("Date Utils Tests",  () =>{
    it("Should convert a date to the expected format",  (done) => {
        
        const dateUtils = new DateUtils();
        
        const day = "22";
        const month = "5";
        const year = "2020";

        const expected = "2020-05-22";
        const actual = dateUtils.convertDateFormat(day, month, year);

        should.exist(actual);
        should.equal(actual,expected, `Expected date format is ${expected} but got ${actual}`)
        
        done();
    })
})

describe("Json to Csv Tests",  () =>{
    it("Should convert a json to the expected csv format",  async () => {
        
        const jsonToCsv = new JsonToCsv();
        
        const testJson = {
            "title": "test",
            "name":"danielle"
        }

        const actual = await jsonToCsv.parseJson(testJson);

        should.exist(actual);
    })
})

describe("Testing file utils",  () =>{
    it("Should write a file to a folder",  async  () => {
        const path = __dirname + "/testFiles";
        const testFileName = "test.csv";

        const testContents = "test,test\ntest,test"

        const fileHelper = new FileHelper();

        await fileHelper.write(path, testFileName, testContents);
    });
    it("Should return true when folder exists",  async  () => {
        const path = __dirname + "/testFiles";

        const fileHelper = new FileHelper();

        const expected = true;
        const actual = await fileHelper.folderExists(path);

        should.exist(actual);
        should.equal(actual, expected, `Expected folder to exist but response was ${actual}`)
    });
    it("Should return false when folder exists",  async  () => {
        const path = __dirname + "/testFiles/testFiles";

        const fileHelper = new FileHelper();

        const expected = false;
        const actual = await fileHelper.folderExists(path);

        should.exist(actual);
        should.equal(actual, expected, `Expected folder to exist but response was ${actual}`)
    });
    it("Should remove all folder contents",  async  () => {
        const path = __dirname + "/testFiles/";

        const fileHelper = new FileHelper();

        await fileHelper.deleteAll(path);
        const folderContents = await fs.readdirSync(path);

        const expected = 0
        const actual = folderContents.length;

        should.equal(actual, expected, `Expected folder contents length to be ${expected} but it was ${actual}`)
    });
})
