# Chris Howard
# Date Last Modified 28 Feb 2022

class Data:
    def __init__(self):
        # Default Path and File
        self.user_path = "C:\\Users\\ramya\\OneDrive\\Documents\\"
        self.user_file = "Emulator.csv"
        self.training_file = self.user_path + self.user_file
        self.isTraining = False
        self.isOpen = False

    def training_file_menu(self):
        # Get Path and filename
        while True:
            user_input = input(
                "******** Training File *********\n"
                "Enter Path             (1)\n"
                "Enter Filename         (2)\n"
                "Use Default            (3)\n"
                "Exit                   (4)\n"
                "********************************\n"
                "Enter Selection: ")
            # Enter File Path
            if user_input == "1":
                self.user_path = input(
                    r'Example: C:\Users\user\ ' + "\n" +
                    "Enter File PATH: ")
            # Enter File Name
            if user_input == "2":
                self.user_file = input(
                    "Example: Emulator.csv" + "\n" +
                    "Enter File Name: ")
            # Use Default
            if user_input == "3":
                self.user_path = "C:\\Users\\ramya\\Downloads\\"
                self.user_file = "Emulator.csv"
            # Exit
            if user_input == "4":
                if self.isOpen == False:
                    self.isNewFile()
                print("Returning")
                break

    def isNewFile(self):
        self.isOpen = True
        user_input = input("Is this a new file? (y/n) ")
        self.training_file = self.user_path + self.user_file
        print("Opening " + self.training_file)
        # write
        if user_input == "y":
            self.fstream = open(self.training_file, "w")
            print("Opening: " + self.training_file)
        # append
        if user_input == "n":
            self.fstream = open(self.training_file, "a")
            print("Opening: " + self.training_file)

    def train_mental_commands(self):
        # Create / Open File in Append Mode
        if self.isOpen == False:
            self.training_file_menu()

        while True:
            user_input = input(
                "**** Train Mental Commands *****\n"
                "Train Neutral          (1)\n"
                "Train Jump             (2)\n"
                "Train Left             (3)\n"
                "Train Right            (4)\n"
                "Exit                   (5)\n"
                "********************************\n"
                "Enter Selection: ")
            # Train Neutral
            if user_input == "1":
                # Write EEG Array separated by commas with 0 at the end.
                self.classification = 0
                # Tell server to start writing stream to file
                self.isTraining = True
                break
            # Train Jump
            if user_input == "2":
                # Write EEG Array separated by commas with 1 at the end.
                self.classification = 1
                # Tell server to start writing stream to file
                self.isTraining = True
                break
            # Train Left
            if user_input == "3":
                # Write EEG Array separated by commas with 2 at the end.
                self.classification = 2
                # Tell server to start writing stream to file
                self.isTraining = True
                break
            # Train Right
            if user_input == "4":
                # Write EEG Array separated by commas with 3 at the end.
                self.classification = 3
                # Tell server to start writing stream to file
                self.isTraining = True
                break
            # Exit
            if user_input == "5":
                self.fstream.close()
                print("Returning")
                self.isTraining = False
                break

    def write_stream2file(self, freq, AF3, F7, F3, FC5, T7, P7, O1, O2, P8, T8, FC6, F4, F8, AF4):
        print("File: " + self.training_file)
        self.fstream.write(str(freq) + "," + str(AF3) + "," + str(F7) + "," + str(F3) + "," + str(FC5) + "," + str(T7)
                                    + "," + str(P7) + "," + str(O1) + "," + str(O2) + "," + str(P8) + "," + str(T8)
                                    + "," + str(FC6) + "," + str(F4) + "," + str(F8) + "," + str(AF4)
                                    + "," + str(self.classification) + "\n")
