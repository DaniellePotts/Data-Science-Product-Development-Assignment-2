import tensorflow as tf
import keras

class Predict:
    def __init__(self, model_path):
        self.model = tf.keras.models.load_model(model_path)
    def predict(self, data):
        return self.model.predict(data,batch_size=1)