# Chris Howard
# Date Last Modified 28 Feb 2022

import socket
from cortex import Cortex
from ML import ML
from Data import Data
import time

class Server:
    def __init__(self):
        """
        Constructs cortex client and bind a function to handle subscribed data streams
        If you do not want to log request and response message , set debug_mode = False. The default is True
        """
        self.c = Cortex(user, debug_mode=True)
        # self.c.bind(new_data_labels=self.on_new_data_labels)
        self.c.bind(new_eeg_data=self.on_new_eeg_data)
        self.c.bind(new_pow_data=self.on_new_pow_data)

        # File Handling
        self.d = Data()

        # Timer
        self.stime = time.perf_counter()

        # Training Time: Default is 8 seconds
        self.training_time = 8
        # Only send unsubscribe json object to Cortex once.
        self.unsub_flag = False

        # Instantiate Machine Learning Controller
        self.ml = ML()

        # START SERVER
        # Set Host to local machine and port to anything above 1000.
        self.HOST = '127.0.0.1'
        #self.HOST = socket.gethostname()#'localhost'
        self.PORT = 12345
        self.connected2unity = False

        # AF_INET is IPv4, SOCK_STREAM is TCP
        self.s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        
        

    def connect_unity(self):
        self.connected2unity = True
        # Connect to Unity
        self.s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        print("Socket Created")

        # Bind to local host on port 12345.
        self.s.bind((self.HOST, self.PORT))
        print("Socket Bind Complete")

        # Only allow 1 connection.
        self.s.listen(1)
        print("Socket now listening")

        # Wait for a connection.
        #while True:
        self.connection, self.addr = self.s.accept()

        print("Connection Established!")

        # Send to Unity
        self.connection.send(b"Connection Successful (sent from server).\n")

    def do_prepare_steps(self):
        """
        Do prepare steps before training.
        Step 1: Connect a headset. For simplicity, the first headset in the list will be connected in the example.
                If you use EPOC Flex headset, you should connect the headset with a proper mappings
                via EMOTIV Launcher first
        Step 2: requestAccess: Request user approval for the current application for first time.
                       You need to open EMOTIV Launcher to approve the access
        Step 3: authorize: to generate a Cortex access token which is required parameter of many APIs
        Step 4: Create a working session with the connected headset
        Returns
        -------
        None
        """
        self.c.do_prepare_steps()

    def sub(self, streams):
        """
        To subscribe to one or more data streams
        'eeg': EEG
        'mot' : Motion
        'dev' : Device information
        'met' : Performance metric
        'pow' : Band power

        Parameters
        ----------
        streams : list, required
            list of streams. For example, ['eeg', 'mot']

        Returns
        -------
        None
        """
        # Start Timer
        self.stime = time.perf_counter()
        self.c.sub_request(streams)
        self.unsub_flag = False

    def unsub(self, streams):
        """
        To subscribe to one or more data streams
        'eeg': EEG
        'mot' : Motion
        'dev' : Device information
        'met' : Performance metric
        'pow' : Band power

        Parameters
        ----------
        streams : list, required
            list of streams. For example, ['eeg', 'mot']

        Returns
        -------
        None
        """
        # Start Time
        self.stime = self.stime - self.stime
        self.c.unsub_request(streams)

    def on_new_eeg_data(self, *args, **kwargs):
        """
        To handle eeg data emitted from Cortex

        Returns
        -------
        data: dictionary
             The values in the array eeg match the labels in the array labels return at on_new_data_labels
        For example:
           {'eeg': [99, 0, 4291.795, 4371.795, 4078.461, 4036.41, 4231.795, 0.0, 0], 'time': 1627457774.5166}
        """
        data = kwargs.get('data')
        # 2 to 15 is EEG data.
        freq = data.get("eeg")[0]
        AF3 = data.get("eeg")[2]
        F7 = data.get("eeg")[3]
        F3 = data.get("eeg")[4]
        FC5 = data.get("eeg")[5]
        T7 = data.get("eeg")[6]
        P7 = data.get("eeg")[7]
        O1 = data.get("eeg")[8]
        O2 = data.get("eeg")[9]
        P8 = data.get("eeg")[10]
        T8 = data.get("eeg")[11]
        FC6 = data.get("eeg")[12]
        F4 = data.get("eeg")[13]
        F8 = data.get("eeg")[14]
        AF4 = data.get("eeg")[15]

        # If connected to Unity Client and a valid model is trained. Then send data.
        if self.connected2unity & self.ml.modeltrained:
            # Classify EEG data and send to Unity
            active_action = self.ml.predict(freq, AF3, F7, F3, FC5, T7, P7, O1, O2, P8, T8, FC6, F4, F8, AF4)
            #self.connection.send(active_action.encode(encoding='utf-8', errors='strict'))

        if self.d.isTraining:
            self.d.write_stream2file(freq, AF3, F7, F3, FC5, T7, P7, O1, O2, P8, T8, FC6, F4, F8, AF4)
            # Current Time
            ctime = time.perf_counter()
            # Run for 8 seconds
            print(ctime - self.stime)
            if (ctime - self.stime) >= self.training_time:
                if self.unsub_flag == False:
                    self.unsub('eeg')
                    self.unsub_flag = True

    # The band power of each EEG sensor. It includes the alpha, low beta, high beta, gamma, and theta bands.
    def on_new_pow_data(self, *args, **kwargs):
            data = kwargs.get('data')
            power = data.get('pow')
            time = data.get('time')

            temp = {'theta': 0,
                    'alpha': 0,
                    'lBeta': 0,
                    'hBeta': 0,
                    'gamma': 0}

            temp['theta'] = power[0]
            temp['alpha'] = power[1]
            temp['lBeta'] = power[2]
            temp['hBeta'] = power[3]
            temp['gamma'] = power[4]

            max_key = max(temp, key=temp.get)
            #print("Max_KEy",max_key)
            # self.connection.send(max_key.encode(encoding='utf-8', errors='strict'))

            # pow_data = {}
            # pow_data['pow'] = result_dic['pow']
            # pow_data['time'] = result_dic['time']
"""
    client_id, client_secret:
    To get a client id and a client secret, you must connect to your Emotiv account on emotiv.com and create a Cortex app
    To subscribe eeg you need to put a valid license (PRO license)
"""

user = {
    "license": "bac620b9-9474-4474-8ac7-1e01fbe10c7d",
    "client_id": "DbWQkptTlZ1Lix2Pyjga0guPCE7dSQV61I2QynrA",
    "client_secret": "AUk5iZQaVs7vC7LprApA9j1VJpCNfRw7ZSluLoj83s4w6CG4LhaJ9OzRjVttrqAuuG2aWXwuggimDZCG7ZkCCmcWPcrjbQi3of1XuT9iKawt3uiYcx0HdGayXsoEYDXj",
    "debit": 100
}
#C:\Users\ramya\OneDrive\Documents pandas.errors.ParserError: Error tokenizing data. C error: Expected 16 fields in line 2, saw 59