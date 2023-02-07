# Chris Howard
# Date Last Modified 28 Feb 2022
# To Do:
    # 1) Let user adjust training time
    # 2) Match Python Naming Conventions
    # 3) Gracefully disconnect from Unity Client
    # 4) One Hot Array for Classification
    # 5) Subscribe to Band Power ( Alpha, Beta, Theta )
    # 6) Why is it printing to file while sending to Unity?

from Server import Server

# ************************ IMPORTANT ************************
# REDACT CLIENT ID and CLIENT SECRET BEFORE UPLOADING OR SHARING
# ************************ IMPORTANT ************************

# Instantiate Server
server = Server()
server.do_prepare_steps()
streams = ['eeg', 'pow']

while True:
    # Start Menu
    user_input = input(
        "********** CONFIGURATION *******\n"
        "Configure Data Store   (0)\n"
        "Train Mental Commands  (1)\n"
        "Train Model            (2)\n"
        "Start Streaming        (3)\n"
        "Exit                   (4)\n"
        "********************************\n"
        "Enter Selection: ")
    # Create Training Data
    if user_input == "0":
        server.d.training_file_menu()
    # Create Training Data
    if user_input == "1":
        server.d.train_mental_commands()
        # Subscribe to data stream ONLY if we are training.
        if server.d.isTraining:
            server.sub(streams)
    # Train Model
    if user_input == "2":
        if server.d.isOpen:
            server.ml.train_model(server.d.user_path, server.d.user_file)
        else:
            print("******************************************")
            print("WARNING: Must specify an ingest file first")
            print("******************************************")
    # Start Server and Streaming
    if user_input == "3":
        if server.ml.modeltrained:
            server.connect_unity()
            server.sub(streams)
        else:
            print("***************************")
            print("WARNING: Train Model First.")
            print("***************************")
    # Exit
    if user_input == "4":
        print("***************************")
        print("Goodbye")
        print("***************************")
        server.unsub(streams)
        break
