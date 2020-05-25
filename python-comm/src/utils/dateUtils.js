class DateUtils{
    convertDateFormat(day, month, year){
        return `${year}-${month >= 10 ? month : "0"+ month}-${day >= 10 ? day : "0"+ day}`
    }
}

module.exports = {
    DateUtils
}