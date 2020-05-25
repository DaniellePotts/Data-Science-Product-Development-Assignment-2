import numpy as np

class RL:
    def run(self, iterations, Q):
        current_state = 0
        steps = [current_state]

        while current_state != iterations:
            next_step_index = np.asarray(np.where(Q[current_state, ] == np.max(Q[current_state, ])))[1]
            if next_step_index.shape[0] > 1:
                next_step_index = int(np.random.choice(next_step_index, size=1))
            else:
                next_step_index = int(next_step_index)

            steps.append(next_step_index)
            current_state = next_step_index
        return steps