// Chris Howard
// Capstone: 12 Apr 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BCIManager : MonoBehaviour
{
    // Singleton
    static BCIManager instance;

    // Init Player GameObject
    [SerializeField]
    GameObject player;
    public float speed = 5.0f;

    // Locomotion System (Traditional Controls)
    [SerializeField]
    GameObject locomotionSystem;

    public static Action jumpReceived;
    public static Action disableJumpButton;
    public static Action enableJumpButton;

    bool bciEnabled = false;

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
        }
    }

    // Mental Command Actions
    private void neutralAction()
    {
        if (bciEnabled == true)
        {
            // Player Neutral
            Debug.Log("Neutral Action");
        }
    }

    private void jumpAction()
    {
        if (bciEnabled == true)
        {
            // Player Jump
            Debug.Log("Jump Action");
            jumpReceived?.Invoke();
        }
    }

    private void leftAction()
    {
        if (bciEnabled == true)
        {
            // Player Left
            Debug.Log("Left Action");
            player.transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }

    private void rightAction()
    {
        if (bciEnabled == true)
        {
            // Player Right
            Debug.Log("Right Action");
            player.transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }

    // Brain Wave Signals
    void thetaReceived()
    {
        if (bciEnabled == true)
        {
            Debug.Log("Theta Received, action taken.");
        }
    }

    private void alphaReceived()
    {
        if (bciEnabled == true)
        {
            Debug.Log("Alpha Received, action taken.");
        }
    }

    private void lBetaReceived()
    {
        if (bciEnabled == true)
        {
            Debug.Log("lBeta Received, action taken.");
        }
    }

    private void hBetaReceived()
    {
        if (bciEnabled == true)
        {
            Debug.Log("hBeta Received, action taken.");
        }
    }

    private void gammaReceived()
    {
        if (bciEnabled == true)
        {
            Debug.Log("Gamma Received, action taken.");
        }
    }

    private void onBCIEnable()
    {
        bciEnabled = true;
        locomotionSystem.SetActive(false);
        Debug.Log("BCI Enabled from BCI Manager.");
    }

    private void onBCIDisable()
    {
        bciEnabled = false;
        locomotionSystem.SetActive(true);
        Debug.Log("BCI disabled from BCI Manager.");
    }

    private void onMixedEnable()
    {
        // Disable jump button.
        disableJumpButton?.Invoke();
        // Enable BCI
        onBCIEnable();

    }

    private void onMixedDisable()
    {
        // Enable jump button
        enableJumpButton?.Invoke();
        // Disable BCI
        onBCIDisable();
    }

    private void OnEnable()
    {
        //ENABLE BCI
        NewBCIMenu.enableCustomClicked += onBCIEnable;
        NewBCIMenu.disableCustomClicked += onBCIDisable;
        NewBCIMenu.enableEmotivClicked += onBCIEnable;
        NewBCIMenu.disableEmotivClicked += onBCIDisable;

        //OPTIONS
        OptionsMenu.bciClicked += onBCIEnable;
        OptionsMenu.traditionalClicked += onBCIDisable;
        OptionsMenu.traditionalClicked += onMixedDisable;
        OptionsMenu.mixedClicked += onMixedEnable;

        //Custom Action
        NetworkManager.neutralReceived += neutralAction;
        NetworkManager.jumpReceived += jumpAction;
        NetworkManager.leftReceived += leftAction;
        NetworkManager.rightReceived += rightAction;

        //Emotiv Action
        EmotivManager.neutralReceived += neutralAction;
        EmotivManager.leftReceived += leftAction;
        EmotivManager.rightReceived += rightAction;
        EmotivManager.jumpReceived += jumpAction;

        //Attention
        NetworkManager.thetaReceived += thetaReceived;
        NetworkManager.alphaReceived += alphaReceived;
        NetworkManager.lBetaReceived += lBetaReceived;
        NetworkManager.hBetaReceived += hBetaReceived;
        NetworkManager.gammaReceived += gammaReceived;
    }

    private void OnDisable()
    {
        // DISABLE BCI
        NewBCIMenu.enableCustomClicked -= onBCIEnable;
        NewBCIMenu.disableCustomClicked -= onBCIDisable;
        NewBCIMenu.enableEmotivClicked -= onBCIEnable;
        NewBCIMenu.disableEmotivClicked -= onBCIDisable;

        //OPTIONS
        OptionsMenu.bciClicked -= onBCIEnable;
        OptionsMenu.traditionalClicked -= onBCIDisable;
        OptionsMenu.traditionalClicked -= onMixedDisable;
        OptionsMenu.mixedClicked -= onMixedEnable;

        //Custom Action
        NetworkManager.neutralReceived -= neutralAction;
        NetworkManager.jumpReceived += jumpAction;
        NetworkManager.leftReceived -= leftAction;
        NetworkManager.rightReceived -= rightAction;
        NetworkManager.jumpReceived -= jumpAction;

        //Emotiv Action
        EmotivManager.neutralReceived -= neutralAction;
        EmotivManager.leftReceived -= leftAction;
        EmotivManager.rightReceived -= rightAction;
        EmotivManager.jumpReceived -= jumpAction;

        //Attention
        NetworkManager.thetaReceived -= thetaReceived;
        NetworkManager.alphaReceived -= alphaReceived;
        NetworkManager.lBetaReceived -= lBetaReceived;
        NetworkManager.hBetaReceived -= hBetaReceived;
        NetworkManager.gammaReceived -= gammaReceived;
    }
}
