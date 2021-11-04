import numpy as np
from tensorflow.keras.models import load_model
import cereja as cj
reloaded = load_model('./static/translator_DL/snake_DL.h5')

to_predict = []

for i in range(2**12):
    state = list(np.zeros(12-len([int(number) for number in str(bin(i)).replace('0b','')]), dtype=int)) +  [int(number) for number in str(bin(i)).replace('0b','')]
    to_predict.append(state)

to_predict = np.array(to_predict)
results = reloaded.predict(to_predict)

dict_dl = {}


for i in range(2**12):
    dict_dl[''.join([str(state) for state in to_predict[i]])] = int(np.argmax(results[i]))
print(len(dict_dl))
cj.FileIO.create('dict_dl.json', dict_dl).save(exist_ok=True)