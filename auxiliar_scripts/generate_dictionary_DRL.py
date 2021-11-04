import tensorflow as tf
import numpy as np
from tensorflow.keras.models import load_model
import cereja as cj
reloaded = tf.saved_model.load('./static/translator_DRL')

to_predict = []

for i in range(2**12):
    state = list(np.zeros(12-len([int(number) for number in str(bin(i)).replace('0b','')]), dtype=int)) +  [int(number) for number in str(bin(i)).replace('0b','')]
    to_predict.append(state)

results = reloaded(to_predict)

dict_drl = {}

for i in range(2**12):
    dict_drl[''.join([str(state) for state in to_predict[i]])] = int(np.argmax(results.numpy()[i]))

cj.FileIO.create('dict_drl.json', dict_drl).save(exist_ok=True)