import pandas as pd
import numpy as np
import sys
from forecast_data_processing import ForecastDataProcessing
from predict import Predict

import os
os.environ['TF_CPP_MIN_LOG_LEVEL'] = '2'

#get data 
data_file_path = os.path.abspath(os.path.join(os.path.dirname( __file__ ), '../..', 'dataFiles/forecast/request.csv'))
print(data_file_path)
data = pd.read_csv(data_file_path)

if not data.empty:
    data.columns = ["Month","Year","Day","date","Season","sales", "ProductType"]
    fdp = ForecastDataProcessing()
    data = fdp.aggregate_monthly(data)
    diff = fdp.get_diff(data)
    lagged = fdp.add_lag(diff, 13)

    scaled = fdp.scale_and_reshape(lagged)

    model_path = os.path.abspath(os.path.join(os.path.dirname( __file__ ), '..', 'TimeSeriesForecast/models/forecast.h5'))
    print("modelpath: ", model_path)
    model = Predict(model_path)

    output = model.predict(scaled)
   
    parsed_output = fdp.process_output(output, data, scaled)

    response_path = os.path.abspath(os.path.join(os.path.dirname( __file__ ), '../..', 'dataFiles/forecast/forecast_response.csv'))

    parsed_output.to_csv(response_path)    


    