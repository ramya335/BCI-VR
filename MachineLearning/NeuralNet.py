# Chris Howard
# Date Last Modified 28 Feb 2022

from keras import Sequential
from keras.layers import Dense, Dropout
from keras import regularizers
from sklearn.preprocessing import StandardScaler  # Used for scaling of data
import numpy as np

class NeuralNet:
    def __init__(self, X_train, X_val_and_test, Y_train, Y_val_and_test, X_val, X_test, Y_val, Y_test):
        self.X_train = X_train
        self.X_val_and_test = X_val_and_test
        self.Y_train = Y_train
        self.Y_val_and_test = Y_val_and_test
        self.X_val = X_val
        self.X_test = X_test
        self.Y_val = Y_val
        self.Y_test = Y_test

        #Indicates model used.
        self.active_model = 1

        # Define Model
        self.model1 = Sequential([
            Dense(32, activation='relu', input_shape=(15,)),
            Dense(32, activation='relu'),
            Dense(1, activation='sigmoid'),
        ])

        self.model2 = Sequential([
            Dense(1000, activation='relu', input_shape=(15,)),
            Dense(1000, activation='relu'),
            Dense(1000, activation='relu'),
            Dense(1000, activation='relu'),
            Dense(1, activation='sigmoid'),
        ])

        self.model3 = Sequential([
            Dense(1000, activation='relu', kernel_regularizer=regularizers.l2(0.03), input_shape=(15,)),
            Dropout(0.3),
            Dense(1000, activation='relu', kernel_regularizer=regularizers.l2(0.03)),
            Dropout(0.3),
            Dense(1000, activation='relu', kernel_regularizer=regularizers.l2(0.03)),
            Dropout(0.3),
            Dense(1000, activation='relu', kernel_regularizer=regularizers.l2(0.03)),
            Dropout(0.3),
            Dense(1, activation='sigmoid', kernel_regularizer=regularizers.l2(0.03)),
        ])

    """
    Build Neural Network Model
            Pre-conditions:
                1. Expects data in the dataset format.
                2. Model is the simplest and most effective.
                3. Model2 adds extra layers.
                4. Model3 is resistant to overfitting. (Dropout)
            Post-conditions:
                1. Trained Neural Net.
    """
    def compile_model1(self):
        self.model1.compile(optimizer='adam', loss='binary_crossentropy', metrics=['accuracy'])

    def compile_model2(self):
        self.model2.compile(optimizer='adam', loss='binary_crossentropy', metrics=['accuracy'])

    def compile_model3(self):
        self.model3.compile(optimizer='adam', loss='binary_crossentropy', metrics=['accuracy'])

    """
    Train and Evaluate
        Preconditions:
            1. Model in model.fit and model.evaluate must match the intended model for use.
            Example: model2.fit and model2.evaluate
            2. Epochs is the number of times the entire dataset is tested on.
            3. Batch size is arbitrary. 
            4. Validation data is labeled as X_val and Y_val

        Post conditions:
            1. Trained model.
            2. Evaluated model.
    """
    def fit_model1(self):
        # Run Model
        self.model1.fit(self.X_train, self.Y_train, batch_size=32, epochs=20, validation_data=(self.X_val, self.Y_val))

    def evaluate_model1(self):
        print("Model Evaluation: ")
        self.model1.evaluate(self.X_test, self.Y_test)

    def fit_model2(self):
        # Run Model
        self.model2.fit(self.X_train, self.Y_train, batch_size=32, epochs=20, validation_data=(self.X_val, self.Y_val))

    def evaluate_model2(self):
        print("Model Evaluation: ")
        self.model2.evaluate(self.X_test, self.Y_test)

    def fit_model3(self):
        # Run Model
        self.model3.fit(self.X_train, self.Y_train, batch_size=32, epochs=20, validation_data=(self.X_val, self.Y_val))

    def evaluate_model3(self):
        print("Model Evaluation: ")
        self.model3.evaluate(self.X_test, self.Y_test)

    def realtime_classification(self, freq, AF3, F7, F3, FC5, T7, P7, O1, O2, P8, T8, FC6, F4, F8, AF4):
        # Double to make 2D array.
        # https://stackoverflow.com/questions/45554008/error-in-python-script-expected-2d-array-got-1d-array-instead
        self.X1 = [[freq, AF3, F7, F3, FC5, T7, P7, O1, O2, P8, T8, FC6, F4, F8, AF4]]

        if self.active_model == 1:
            prediction = self.model1.predict(self.X1)

        if self.active_model == 2:
            prediction = self.model2.predict(self.X1)

        if self.active_model == 3:
            prediction = self.model3.predict(self.X1)

        # Reference: https://stackoverflow.com/questions/54167910/keras-how-to-use-argmax-for-predictions
        CATEGORIES = ["Neutral", "Jump", "Left", "Right"]  # enumerated type
        output = int(prediction[0][0])
        pred_name = CATEGORIES[round(output)]

        return pred_name