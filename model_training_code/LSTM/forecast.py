import pandas as pd
import numpy as np
import tensorflow as tf 
from forecast_data_processing import ForecastDataProcessing
from predict import Predict
#get data 
data = pd.read_csv("./train.csv")

if not data.empty:
    fdp = ForecastDataProcessing()
    data = data.tail(5000)
    data = fdp.aggregate_monthly(data)
    diff = fdp.get_diff(data)
    lagged = fdp.add_lag(diff, 13)

    scaled = fdp.scale_and_reshape(lagged)

    model = Predict("./models/test.h5")

    output = model.predict(scaled)
   
    parsed_output = fdp.process_output(output, data, scaled) 
    parsed_output.to_csv("test.csv")
    