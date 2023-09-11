using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotivUnityPlugin;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //EmotivExample
        string ClientId = "DbWQkptTlZ1Lix2Pyjga0guPCE7dSQV61I2QynrA";
        string ClientSecret = "AUk5iZQaVs7vC7LprApA9j1VJpCNfRw7ZSluLoj83s4w6CG4LhaJ9OzRjVttrqAuuG2aWXwuggimDZCG7ZkCCmcWPcrjbQi3of1XuT9iKawt3uiYcx0HdGayXsoEYDXj";
        string TmpAppDataDir = "UnityApp";
        string AppUrl = "wss://localhost:6868";


        // setup App configuration
        DataStreamManager.Instance.SetAppConfig(ClientId, ClientSecret, "1.0.0", "UnityApp", TmpAppDataDir, AppUrl); 

        // start connect and authorize
        DataStreamManager.Instance.StartAuthorize();

        // ... 
        // authorize successfully then find headsets

        // creating session and subscribe data
        List<string> dataStreamList = new List<string>() { DataStreamName.DevInfos };
        DataStreamManager.Instance.StartDataStream(dataStreamList, "69A0EF4C");
        
        // You also can suscribe more data
        DataStreamManager.Instance.SubscribeMoreData(new List<string>() { DataStreamName.MentalCommands});

        // Or unsubscribe data
        DataStreamManager.Instance.UnSubscribeData(new List<string>() { DataStreamName.MentalCommands});

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
