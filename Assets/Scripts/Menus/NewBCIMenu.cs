// Chris Howard
// Capstone: 12 Apr 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class NewBCIMenu : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI emotivBtnText;
    bool emotivEnabled = false;

    [SerializeField]
    TextMeshProUGUI customBtnText;
    bool customEnabled = false;

    [SerializeField]
    TextMeshProUGUI emulatorBtnText;
    bool emulatorEnabled = false;

    // Events
    public static Action enableEmotivClicked;
    public static Action disableEmotivClicked;
    public static Action enableCustomClicked;
    public static Action disableCustomClicked;
    public static Action enableEmulatorClicked;
    public static Action disableEmulatorClicked;
    public static Action backClicked;

    // Make sure to attach these Buttons in the Inspector
    public Button emotiv, custom, emulator, back;

    // Singleton
    static NewBCIMenu instance;

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

        //Calls the TaskOnClick method when you click the Button
        emotiv.onClick.AddListener(TaskOnClickEmotiv);
        custom.onClick.AddListener(TaskOnClickCustom);
        emulator.onClick.AddListener(TaskOnClickEmulator);
        back.onClick.AddListener(TaskOnClickBack);
    }

    void TaskOnClickEmotiv()
    {   
        if (customEnabled == false && emotivEnabled == false)
        {
            // Let the BCIManager know.
            enableEmotivClicked?.Invoke();
            emotivBtnText.text = "Disable";
            emotivEnabled = true;
        } 
        else if (customEnabled == false && emotivEnabled == true)
        {
            // Let the BCIManager know.
            disableEmotivClicked?.Invoke();

            // Flip flop for button text.
            emotivBtnText.text = "Emotiv";
            emotivEnabled = false;
        }
        else if (customEnabled == true)
        {
            Debug.Log("ERROR: Disable custom first!");
        }
    }

    void TaskOnClickCustom()
    {
        if (customEnabled == false && emotivEnabled == false)
        {
            // Let the BCIManager know.
            enableCustomClicked?.Invoke();

            customBtnText.text = "Disable";
            customEnabled = true;
        }
        else if (customEnabled == true && emotivEnabled == false)
        {
            // Let the BCIManager know.
            disableCustomClicked?.Invoke();
            customBtnText.text = "Custom";
            customEnabled = false;
        }
        else if (emotivEnabled == true)
        {
            Debug.Log("ERROR: Disable Emotiv first!");
        }
    }

    void TaskOnClickEmulator()
    {
        if (emulatorEnabled == false)
        {
            // Let the BCIManager know.
            enableEmulatorClicked?.Invoke();

            // Flip flop for button text.
            emulatorBtnText.text = "Emulator";
            emulatorEnabled = true;
        } 
        else
        {
            // Let the BCIManager know.
            disableEmulatorClicked?.Invoke();

            // Flip flop for button text.
            emulatorBtnText.text = "Disable";
            emulatorEnabled = false;
        }
    }

    void TaskOnClickBack()
    {
        // Let the MenuManager know.
        backClicked?.Invoke();
    }
}
