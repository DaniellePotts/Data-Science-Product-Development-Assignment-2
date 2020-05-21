import numpy as np
import datetime
from datetime import timedelta, date

class Utils:
    def __init__(self):
        super().__init__()
    def determine_season(self, date):
        parsed_date = datetime.datetime.strptime(date, "%Y-%m-%d").date()
        month = parsed_date.month
        
        if(month == 12 or month == 1 or month == 2):
            return 'Winter!'
        elif(month >= 3 and month <= 5):
            return 'Spring'
        elif(month >= 6 and month <= 8):
            return 'Summer'
        elif(month >= 9 and month <=11):
            return 'Fall'
    def save_Q(self, txt_file, Q):
        np.savetxt(txt_file, Q, fmt='%d')
    def load_Q(self, txt_file):
        return np.loadtxt(txt_file, dtype=int)