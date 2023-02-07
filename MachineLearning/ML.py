# Chris Howard
# Date Last Modified 28 Feb 2022

# Python ML libraries
import pandas
import glob
import utils
from sklearn.model_selection import train_test_split
from sklearn import preprocessing

# Custom Machine Learning Models
from NeuralNet import NeuralNet
from DecisionTree import DecisionTree
from SVM import SVM

class ML:

    def __init__(self):
        # Tells which model prediction to return
        self.select_nn = False
        self.select_dt = False
        self.select_svm = False

        # Action prediction by ML model.
        self.action = " "
        self.modeltrained = False

    def train_model(self, user_path, user_file):
        # Must Format Data Before Training Model
        print("USer_PAth",user_path)
        print("User_file",user_file)
        self.format_data(user_path, user_file)

        while True:
            user_input = input(
                "********** ML Model Selection **********\n"
                "Neural Net                 (1)\n"
                "Decision Tree              (2)\n"
                "Support Vector Machine     (3)\n"
                "Return to Config Menu      (4)\n"
                "****************************************\n"
                "Enter Selection: ")
            # Neural Net
            if user_input == "1":
                self.select_nn = True
                user_input = input(
                    "********** Neural Net Model Selection **********\n"
                    "Model 1 (1)\n"
                    "Model 2 (2)\n"
                    "Model 3 (3)\n"
                    "Return to ML Model Selection Menu (4)\n"
                    "*************************************************\n"
                    "Enter Selection: ")
                # Model 1
                if user_input == "1":
                    self.prepare_NeuralNet1()
                # Model 2
                if user_input == "2":
                    self.prepare_NeuralNet2()
                # Model 3
                if user_input == "3":
                    self.prepare_NeuralNet3()
                # Exit
                if user_input == "4":
                    print("Returning")
                    break
            # Decision Tree
            if user_input == "2":
                self.select_dt = True
                self.prepare_DecisionTree()
                print("Returning")
                break
            # Support Vector Machine
            if user_input == "3":
                self.select_svm = True
                self.prepare_SVM()
                print("Returning")
                break
            # Return to Config Menu
            if user_input == "4":
                print("Returning")
                break

    def format_data(self, user_path, user_file):
        """#Format Data for Processing

        Preconditions:
        1. Data placed in Google Drive.
        2. Google Drive mounted to Colaboratory.
        3. Copy path in PATH variable.

        Postconditions:
        1. Creates a dictionary "list" containing all the CSV files appended to each other.
        2. Adds "list" to a Pandas dataframe "df".
        """

        # Load CSV using Pandas reference "https://machinelearningmastery.com/load-machine-learning-data-python/"
        names = ['EEG.Counter', 'EEG.AF3', 'EEG.F7', 'EEG.F3', 'EEG.FC5', 'EEG.T7', 'EEG.P7', 'EEG.O1', 'EEG.O2',
             'EEG.P8', 'EEG.T8', 'EEG.FC6', 'EEG.F4', 'EEG.F8', 'EEG.AF4', 'Classification']

        # Reference
        # https://intellipaat.com/community/17913/import-multiple-csv-files-into-pandas-and-concatenate-into-one-dataframe
        # Load multiple CSV files into a single Pandas dataframe.

        """
        # If we want to ingest multiple files
        # PATH = self.user_path
        # files = glob.glob(PATH + "/*")
        """

        user_file = "/" + user_file
        files = glob.glob(user_path + user_file)

        list = []

        for filename in files:
            df = pandas.read_csv(filename, names=names)
            list.append(df)

        frame = pandas.concat(list, axis=0, ignore_index=True)


        """
        Separate Testing and Training Data
        
        Preconditions:
        1. Takes the dataframe "df"
        
        Postconditions:
        1. Turns data frame into a dataset named "dataset".
        2. Normalized values.
        3. Data is split 50% Train and 30% Test.
        4. Column 16 contains the classification (0 for neutral or 1 for push). 
        5. Column 16 is removed from test data set. 
        """

        # Reference:
        # https://www.freecodecamp.org/news/how-to-build-your-first-neural-network-to-predict-house-prices-with-keras-f8db83049159/

        # Create Dataset
        dataset = frame.values  # Create an array from the data frame.

        X = dataset[:, 0:15]  # [rows, columns] Not splitting up rows, Columns 0 through 15 (Not 16) go into X. Features.
        Y = dataset[:, 15]  # Only column 16 goes into Y.

        # Normalize values: scale down, but keep meaning.
        min_max_scaler = preprocessing.MinMaxScaler()
        print("min_max",min_max_scaler)
        X_scale = min_max_scaler.fit_transform(X)

        # Split data into 50% Train Data and 30% Test Data
        self.X_train, self.X_val_and_test, self.Y_train, self.Y_val_and_test = train_test_split(X, Y, test_size=0.3,
                                                                                                shuffle=True)
        self.X_val, self.X_test, self.Y_val, self.Y_test = train_test_split(self.X_val_and_test, self.Y_val_and_test,
                                                                            test_size=0.5, shuffle=True)

        print(self.X_train.shape, self.X_val.shape, self.X_test.shape, self.Y_train.shape, self.Y_val.shape,
              self.Y_test.shape)
        print(frame.shape)  # Double check that the dimensions are inline.

    def prepare_NeuralNet1(self):
        self.select_nn = True
        self.nn = NeuralNet(self.X_train, self.X_val_and_test, self.Y_train, self.Y_val_and_test,
                            self.X_val, self.X_test, self.Y_val, self.Y_test)
        self.nn.compile_model1()
        self.nn.fit_model1()
        self.nn.evaluate_model1()
        self.modeltrained = True

    def prepare_NeuralNet2(self):
        self.select_nn = True
        self.nn = NeuralNet(self.X_train, self.X_val_and_test, self.Y_train, self.Y_val_and_test,
                            self.X_val, self.X_test, self.Y_val, self.Y_test)
        self.nn.compile_model2()
        self.nn.fit_model2()
        self.nn.evaluate_model2()
        self.modeltrained = True

    def prepare_NeuralNet3(self):
        self.select_nn = True
        self.nn = NeuralNet(self.X_train, self.X_val_and_test, self.Y_train, self.Y_val_and_test,
                            self.X_val, self.X_test, self.Y_val, self.Y_test)
        self.nn.compile_model3()
        self.nn.fit_model3()
        self.nn.evaluate_model3()
        self.modeltrained = True

    def prepare_DecisionTree(self):
        self.select_dt = True
        self.dt = DecisionTree(self.X_train, self.X_val_and_test, self.Y_train, self.Y_val_and_test,
                               self.X_val, self.X_test, self.Y_val, self.Y_test)
        self.dt.train_Decision_Tree()
        self.modeltrained = True

    def prepare_SVM(self):
        self.select_svm = True
        self.svm = SVM(self.X_train, self.X_val_and_test, self.Y_train, self.Y_val_and_test,
                                self.X_val, self.X_test, self.Y_val, self.Y_test)
        self.svm.train_SVM()
        self.modeltrained = True

    def predict(self, freq, AF3, F7, F3, FC5, T7, P7, O1, O2, P8, T8, FC6, F4, F8, AF4):
        if self.select_nn:
            self.action = self.nn.realtime_classification(freq, AF3, F7, F3, FC5, T7, P7, O1, O2, P8, T8, FC6, F4, F8, AF4)

        if self.select_svm:
            self.action = self.svm.realtime_classification(freq, AF3, F7, F3, FC5, T7, P7, O1, O2, P8, T8, FC6, F4, F8, AF4)

        if self.select_dt:
            self.action = self.dt.realtime_classification(freq, AF3, F7, F3, FC5, T7, P7, O1, O2, P8, T8, FC6, F4, F8, AF4)

        return self.action
