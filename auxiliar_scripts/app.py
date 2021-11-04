import tensorflow as tf
from tensorflow.keras.models import load_model

from flask import Flask, request, jsonify

app = Flask(__name__)

#Deep Reinforced Learning
reloaded_DRL = tf.saved_model.load('./static/translator_DRL')


#Deep Learning
reloaded_DL = load_model('./static/translator_DL/snake_DL.h5')


@app.route('/incomes', methods=['POST'])
def add_income():
    array = request.get_json()['array']
    snake_IA = request.get_json()['snake']
    if snake_IA == 1:
        movement = reloaded_DRL(array)
        return jsonify({"movement1": str(movement.numpy()[0][0]), "movement2": str(movement.numpy()[0][1]),
                        "movement3": str(movement.numpy()[0][2]), "movement4": str(movement.numpy()[0][3])})
    else:
        movement = reloaded_DL.predict(array)
        return jsonify({"movement1": str(movement[0][0]), "movement2": str(movement[0][1]),
                        "movement3": str(movement[0][2])})