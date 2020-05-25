import numpy as np
import pandas as pd
import json

from Utils import Utils
from RL import RL

utils = Utils()
rl = RL()

items = pd.read_csv("./items.csv")
print(items)
Q = utils.load_Q("./models/spring_Q.txt")
Q = np.asmatrix(Q)
steps = rl.run(7, Q)

purchased_items = {
    "items":[]
}

for step in steps:
    purchased_items['items'].append(items.loc[step])

df = pd.DataFrame(purchased_items)

df.to_json("response2.json")