// Chris Howard
// Capstone: 12 Apr 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotivUnityPlugin;
using System;
using System.Linq;

public class EmotivManager : MonoBehaviour
{
    public static EmotivManager instance;
    private bool bciEnabled = false;
    bool mentalCmdRcvd = false;
    bool powRcvd = false;
    private bool emulatorEnabled = false;
    string mentalCommand;

    double theta; // Meditation
    double alpha; // Relaxed and Alert
    double betaLow; // Active Concentration
    double betaHigh;
    double gamma; // Fight or Flight
    List<double> bandList;
    List<string> bandNames;
    List<int> scoreBoard;
    string dominant;
    int thetaCount, alphaCount, betaLCount, betaHCount, gammaCount;

    BCITraining _bciTraining = new BCITraining();

    [SerializeField]
    GameObject player;

    public static Action neutralReceived;
    public static Action leftReceived;
    public static Action rightReceived;
    public static Action jumpReceived;

    List<string> dataStreamList = new List<string>() { DataStreamName.DevInfos, DataStreamName.MentalCommands,
                                                       DataStreamName.SysEvents, DataStreamName.BandPower};
    string profileName = "eddy";
    string headsetId = "EPOCX-69A0EF4C";
    string emulatorName = "EPOCX-69A0EF4C";
    string EpocxName = "EPOCX-69A0EF4C";


    // Start is called before the first frame update
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
            Debug.Log("Emotiv Manager Instance Created");

            // start connect and authorize
            DataStreamManager.Instance.StartAuthorize();
            _bciTraining.Init(); //IAW email from Emotiv
            bandList = new List<double>();
            bandNames = new List<string>();
            scoreBoard = new List<int>();
            thetaCount = 0;
            alphaCount = 0;
            betaLCount = 0;
            betaHCount = 0;
            gammaCount = 0;
        }
    }

    private void Update()
    {
        if (mentalCmdRcvd)
        {
            mentalCommand = DataStreamManager.Instance.getMentalCommandAction();

            //Debug.Log("Works: " + mentalCommand);

            if (bciEnabled == true)
            {
                Debug.Log("Result: " + mentalCommand);
                switch (mentalCommand)
                {
                    // Action (Mental Command)
                    case "neutral":
                        neutralReceived?.Invoke();
                        break;

                    case "left":
                        leftReceived?.Invoke();
                        break;

                    case "right":
                        rightReceived?.Invoke();
                        break;

                    case "lift":
                        jumpReceived?.Invoke();
                        break;

                    default:
                        Debug.Log("Error in Network Manager switch statement.");
                        break;
                }
            }
        }

        if (powRcvd)
        {
            theta = DataStreamManager.Instance.GetThetaData(Channel_t.CHAN_F7);
            alpha = DataStreamManager.Instance.GetAlphaData(Channel_t.CHAN_F7);
            betaLow = DataStreamManager.Instance.GetLowBetaData(Channel_t.CHAN_F7);
            betaHigh = DataStreamManager.Instance.GetHighBetaData(Channel_t.CHAN_F7);
            gamma = DataStreamManager.Instance.GetGammaData(Channel_t.CHAN_F7);

            bandList.Add(theta);
            bandNames.Add("theta");
            bandList.Add(alpha);
            bandNames.Add("alpha");
            bandList.Add(betaLow);
            bandNames.Add("betaL");
            bandList.Add(betaHigh);
            bandNames.Add("betaH");
            bandList.Add(gamma);
            bandNames.Add("gamma");

            //Find index of dominant power band.
            dominant = bandNames[bandList.IndexOf(bandList.Max())];

            //Every 2 seconds
            // Find max
            // if theta is max then theta++
            // if alpha is max then alpha++
            // if betaL is max then betaL++
            // if betaH is max then betaH++
            // if gamma is max then gamma++
            switch (dominant)
            {
                case "theta":
                    thetaCount = thetaCount++;
                    break;
                case "alpha":
                    alphaCount = alphaCount++;
                    break;
                case "betaL":
                    betaLCount = betaLCount++;
                    break;
                case "betaH":
                    betaHCount = betaHCount++;
                    break;
                case "gamma":
                    gamma = gammaCount++;
                    break;
            }
        }
    }

    public string finalScore()
    {
        scoreBoard.Add(thetaCount);
        scoreBoard.Add(alphaCount);
        scoreBoard.Add(betaLCount);
        scoreBoard.Add(betaHCount);
        scoreBoard.Add(gammaCount);

        //Find index of dominant power band.
        dominant = bandNames[scoreBoard.IndexOf(scoreBoard.Max())];

        return dominant;
    }

    private void onMentalCommandReceived(double time, string act, double pow)
    {
        // Stub required for CortexAPI
    }

    private void onBCIEnable()
    {
        bciEnabled = true;

        if (emulatorEnabled == true)
        {
            Debug.Log("Emulator Enabled");
            DataStreamManager.Instance.StartDataStream(dataStreamList, emulatorName);
        }
        else
        {
            Debug.Log("Actual Device Connected.");
            DataStreamManager.Instance.StartDataStream(dataStreamList, EpocxName);
        }
        
        mentalCmdRcvd = true;
        powRcvd = true;

        _bciTraining.QueryProfile();
        _bciTraining.LoadProfileWithHeadset(profileName, headsetId);
        mentalCmdRcvd = true;
    }

    private void onBCIDisable()
    {
        bciEnabled = false;
        _bciTraining.UnLoadProfile(profileName, headsetId);
        DataStreamManager.Instance.UnSubscribeData(dataStreamList);
        DataStreamManager.Instance.Stop();
        mentalCmdRcvd = false;
        powRcvd = false;
    }

    private void onEmulatorEnable()
    {
        emulatorEnabled = true;
    }

    private void onEmulatorDisable()
    {
        emulatorEnabled = false;
    }

    private void OnEnable()
    {
        NewBCIMenu.enableEmotivClicked += onBCIEnable;
        NewBCIMenu.disableEmotivClicked += onBCIDisable;
        NewBCIMenu.enableEmulatorClicked += onEmulatorEnable;
        NewBCIMenu.disableEmulatorClicked += onEmulatorDisable;
    }

    private void OnDisable()
    {
        NewBCIMenu.enableEmotivClicked -= onBCIEnable;
        NewBCIMenu.disableEmotivClicked -= onBCIDisable;
        NewBCIMenu.enableEmulatorClicked -= onEmulatorEnable;
        NewBCIMenu.disableEmulatorClicked -= onEmulatorDisable;
    }
}
