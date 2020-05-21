import datetime
import calendar

class Helpers:
    def add_months(self, sourcedate, months):
        month = sourcedate.month - 1 + months
        year = sourcedate.year + month // 12
        month = month % 12 + 1
        day = min(sourcedate.day, calendar.monthrange(year,month)[1])
        return datetime.date(year, month, day)
    def get_last_date(self, data):
        return list(data[-7:].date)[len(data[-7:]) - 1]