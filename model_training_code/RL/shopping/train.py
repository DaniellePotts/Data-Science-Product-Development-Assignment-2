from Item import Item
from Utils import Utils
from training_methods import TrainingMethods
from random import randint
import numpy as np

utils = Utils()


seasons = ["Winter","Spring","Summer","Autumn","None"]
item_types = ["Technology","Home/Furniture","Kitchen","Sports/Fitness","Outdoor"]

items_count = 1000
items_array = []

for i in range(items_count):
    season = randint(0, len(seasons) - 1)
    product_type = randint(0, len(item_types) - 1)
    new_item = Item(seasons[season], item_types[product_type])
    items_array.append(new_item)

MATRIX_SIZE = items_count

R = np.matrix(np.ones(shape=(MATRIX_SIZE, MATRIX_SIZE)))
R *= -1



start_date = "2020-5-2"
season = utils.determine_season(start_date)

for i in range(len(items_array)):
    if items_array[i].season == season or items_array[i].season == 'None':
        R[i] = 100
    else:
        R[i] = 0

training_methods = TrainingMethods(R)

Q = np.matrix(np.zeros([MATRIX_SIZE, MATRIX_SIZE]))
gamma = 0.8
initial_state = 1
available_act = []
available_act = training_methods.available_actions(initial_state,available_act)
action = training_methods.sample_next_action(available_act)

training_methods.update(Q, initial_state, action, gamma)