# Chris Howard
# Date Last Modified 28 Feb 2022

import numpy as np
from sklearn.preprocessing import StandardScaler  # Used for scaling of data
from sklearn import preprocessing
from sklearn import utils
from sklearn.metrics import confusion_matrix
from sklearn.tree import DecisionTreeClassifier
from sklearn.metrics import accuracy_score
from sklearn.metrics import classification_report

class DecisionTree:
    def __init__(self, X_train, X_val_and_test, Y_train, Y_val_and_test, X_val, X_test, Y_val, Y_test):
        self.X_train = X_train
        self.X_val_and_test = X_val_and_test
        self.Y_train = Y_train
        self.Y_val_and_test = Y_val_and_test
        self.X_val = X_val
        self.X_test = X_test
        self.Y_val = Y_val
        self.Y_test = Y_test
        self.model = DecisionTreeClassifier(criterion="gini", random_state=100, max_depth=3, min_samples_leaf=5)

    def train_Decision_Tree(self):
        # Decision Tree Classifier
        # Creating the classifier object
        # Reference: https://www.geeksforgeeks.org/decision-tree-implementation-python/
        
        label_encoder = preprocessing.LabelEncoder()
        self.Y_train = label_encoder.fit_transform(self.Y_train)
        self.X_train = label_encoder.fit_transform(self.Y_train)
        self.Y_test = label_encoder.fit_transform(self.Y_test)
        
        self.X_train = np.ravel(self.X_train)
        self.Y_train = np.ravel(self.Y_train)
        self.Y_test = np.ravel(self.Y_test)

        self.model.fit(self.X_train.reshape(-1,1), self.Y_train.reshape(-1,1))

        # Make Predictions
        # Prediction on test with giniIndex
        # Reference: https://www.geeksforgeeks.org/decision-tree-implementation-python/
        y_pred = self.model.predict(self.Y_test.reshape(-1,1))

        # Decision Tree Metrics
        # Reference: https://www.geeksforgeeks.org/decision-tree-implementation-python/
        print("Confusion Matrix: ", confusion_matrix(self.Y_test, y_pred))
        print("Accuracy : ", accuracy_score(self.Y_test, y_pred) * 100)
        print("Report : ", classification_report(self.Y_test, y_pred))

    def realtime_classification(self, freq, AF3, F7, F3, FC5, T7, P7, O1, O2, P8, T8, FC6, F4, F8, AF4):
        # Double to make 2D array.
        # https://stackoverflow.com/questions/45554008/error-in-python-script-expected-2d-array-got-1d-array-instead
        self.X1 = [[freq, AF3, F7, F3, FC5, T7, P7, O1, O2, P8, T8, FC6, F4, F8, AF4]]
        prediction = self.model.predict(self.X1)

        # Reference: https://stackoverflow.com/questions/54167910/keras-how-to-use-argmax-for-predictions
        CATEGORIES = ["Neutral", "Jump", "Left", "Right"]  # enumerated type
        output = int(prediction[0][0])
        pred_name = CATEGORIES[round(output)]

        return pred_name