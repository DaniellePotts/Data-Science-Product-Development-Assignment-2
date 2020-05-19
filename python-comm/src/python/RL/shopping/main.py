
import os
import numpy as np
import pandas as pd
import json

from Utils import Utils
from RL import RL

from random import randint
import sys

if sys.argv[1] and sys.argv[2]:
    rounds = int(sys.argv[2])
    season = sys.argv[1].lower()

    model_file = season + "_Q.txt"

    utils = Utils()
    rl = RL()

    items_path = os.path.abspath(os.path.join(os.path.dirname( __file__ ), '..', 'shopping/items.csv'))

    items = pd.read_csv(items_path)

    Q_path = os.path.abspath(os.path.join(os.path.dirname( __file__ ), '..', 'shopping/models/' + model_file))
    Q = utils.load_Q(Q_path)
    Q = np.asmatrix(Q)

    steps = rl.run(rounds, Q)

    purchased_items = []

    for step in steps:
        purchased_items.append(items.loc[step])

    df = pd.DataFrame(purchased_items)
    df.columns = ["index","Season","ProductType"]
    response_path = os.path.abspath(os.path.join(os.path.dirname( __file__ ), '../../..', 'dataFiles/RL/rl_response.csv'))
    df.to_csv(response_path)