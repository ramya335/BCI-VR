// Chris Howard
// Capstone: 12 Apr 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleTCP; // Networking
using System; // Events

public class NetworkManager : MonoBehaviour
{
    // Singleton
    static NetworkManager instance;

    [SerializeField]
    const int port = 12345;
    [SerializeField]
    const string ip = "127.0.0.1";

    string result;
    private bool bciEnabled = false;

    // Mental Command Signals
    public static Action dataReceived;
    public static Action neutralReceived;
    public static Action jumpReceived;
    public static Action leftReceived;
    public static Action rightReceived;

    // Attention Signals
    public static Action thetaReceived;
    public static Action alphaReceived;
    public static Action lBetaReceived;
    public static Action hBetaReceived;
    public static Action gammaReceived;

    void Start()
    {
        // Singleton
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            // Connect to Python on Local IP
            var client = new SimpleTcpClient().Connect(ip, port);
            client.DataReceived += onDataReceived;
            //client.Write("From Client"); WORKS BUT NOT NEEDED
        }
    }

    void onDataReceived(object sender, Message e)
    {
        var temp = e.Data;
        result = System.Text.Encoding.UTF8.GetString(temp);

        if (bciEnabled == true)
        {
            Debug.Log("Received: " + result);
        }
        
        // Send Data to BCI Manager
        dataReceived?.Invoke();

        if (bciEnabled == true)
        {
            Debug.Log("Result: " + result);
            switch (result)
            {
                // Action (Mental Command)
                case "Neutral":
                    neutralReceived?.Invoke();
                    break;

                case "Left":
                    leftReceived?.Invoke();
                    break;

                case "Right":
                    rightReceived?.Invoke();
                    break;

                case "Lift":
                    jumpReceived?.Invoke();
                    break;

                // Attention (Brain Waves)
                case "theta":
                    thetaReceived?.Invoke();
                    break;

                case "alpha":
                    alphaReceived?.Invoke();
                    break;

                case "lBeta":
                    lBetaReceived?.Invoke();
                    break;

                case "hBeta":
                    hBetaReceived?.Invoke();
                    break;

                case "gamma":
                    gammaReceived?.Invoke();
                    break;

                default:
                    Debug.Log("Error in Network Manager switch statement.");
                    break;
            }
        }
    }

    private void onCustomEnable()
    {
        bciEnabled = true;
    }

    private void onCustomDisable()
    {
        bciEnabled = false;
    }


    private void OnEnable()
    {
        NewBCIMenu.enableCustomClicked += onCustomEnable;
        NewBCIMenu.disableCustomClicked += onCustomDisable;
    }

    private void OnDisable()
    {
        NewBCIMenu.enableCustomClicked -= onCustomEnable;
        NewBCIMenu.disableCustomClicked -= onCustomDisable;
    }
}
