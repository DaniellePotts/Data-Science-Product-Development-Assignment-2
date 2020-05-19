import numpy as np

class TrainingMethods:
    def __init__(self, R):
        self.R = R

    def available_actions(self, state):
        current_state_row = self.R[state,]
        av_act = np.where(current_state_row >= 0)[1]
        return av_act

    def sample_next_action(self, available_actions_range, available_act):
        next_action = int(np.random.choice(self.available_act,1))
        return next_action

    def update(self, Q, current_state, action, gamma):
        max_index = np.where(Q[action, ] == np.max(Q[action, ]))[1]

        if max_index.shape[0] > 1:
            max_index = int(np.random.choice(max_index, size=1))
        else:
            max_index = int(max_index)
        max_value = Q[action, max_index]

        Q[current_state, action] = R[current_state, action] + gamma * max_value

        if (np.max(Q) > 0):
            return(np.sum(Q/np.max(Q)*100))
        else:
            return (0)