import pandas as pd
import numpy as np
from sklearn.preprocessing import MinMaxScaler
from helpers import Helpers

class ForecastDataProcessing:
    def __init__(self):
        self.scaler = MinMaxScaler(feature_range=(-1,1))
        self.helpers = Helpers()

    def aggregate_monthly(self, data):
        if 'date' in data:
            data['date'] = pd.to_datetime(data['date'], format="%Y-%d-%m")
            print(data['date'])
            data['date'] = data['date'].dt.year.astype('str') + '-' + data['date'].dt.month.astype('str') + '-01'
            data['date'] = pd.to_datetime(data['date'])
            data = data.groupby('date').sales.sum().reset_index()
            return data
    def get_diff(self, data):
        df_diff = data.copy()
        df_diff['prev_sales'] = df_diff['sales'].shift(1)
        df_diff = df_diff.dropna()
        df_diff['diff'] = (df_diff['sales'] - df_diff['prev_sales'])
        
        return df_diff
    def add_lag(self, data, lag_limit):
        if 'prev_sales' in data:
            df_supervised = data.drop(['prev_sales'],axis=1)
            for inc in range(1,lag_limit):
                field_name = 'lag_' + str(inc)
                df_supervised[field_name] = df_supervised['diff'].shift(inc)
            df_supervised = df_supervised.dropna().reset_index(drop=True)
            df_supervised = df_supervised.drop(['sales','date'],axis=1)
            return df_supervised[-6:].values
    def scale_and_reshape(self, data):
        self.scaler = self.scaler.fit(data)
        data = data.reshape(data.shape[0], data.shape[1])
        scaled = self.scaler.transform(data)
        X, y = scaled[:, 1:], scaled[:, 0:1]
        X = X.reshape(X.shape[0], 1, X.shape[1])

        return X
    def process_output(self, data, og_data, X):
        processed_prediction = self.parse_model_output(data, X)
        last_date = self.helpers.get_last_date(og_data)
        result = self.parse_predictions(processed_prediction, last_date)
        return result
    def parse_model_output(self, output, og_data):
        predicted = output.reshape(output.shape[0], 1, output.shape[1])
        pred_test_set = []
        for index in range(0,len(predicted)):
            pred_test_set.append(np.concatenate([predicted[index],og_data[index]],axis=1))
        pred_test_set = np.array(pred_test_set)
        pred_test_set = pred_test_set.reshape(pred_test_set.shape[0], pred_test_set.shape[2])
        pred_test_set_inverted = self.scaler.inverse_transform(pred_test_set)
        return pred_test_set_inverted
    def parse_predictions(self, pred_test_set_inverted, last_date):
        result_list = []
        for index in range(0,len(pred_test_set_inverted)):
            result_dict = {}
            result_dict['pred_value'] = int(pred_test_set_inverted[index][0])
            if(index == 0):
                date_n = self.helpers.add_months(last_date, 1)
            else:
                date_n = self.helpers.add_months(date_n, 1)
            result_dict['date'] = date_n
            result_list.append(result_dict)
        return pd.DataFrame(result_list)