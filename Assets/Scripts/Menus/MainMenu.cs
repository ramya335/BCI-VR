// Chris Howard
// Capstone: 12 Apr 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; // Events

public class MainMenu : MonoBehaviour
{
    // Events
    public static Action resumeClicked;
    public static Action bciClicked;
    public static Action optionsClicked;
    public static Action controlsClicked;
    public static Action quitClicked;

    // Make sure to attach these Buttons in the Inspector
    public Button resume, bci, options, controls, quit;

    // Singleton
    static MainMenu instance;

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

            //Calls the TaskOnClick method when you click the Button
            resume.onClick.AddListener(TaskOnClickResume);
            bci.onClick.AddListener(TaskOnClickBCI);
            options.onClick.AddListener(TaskOnClickOptions);
            controls.onClick.AddListener(TaskOnClickControls);
            quit.onClick.AddListener(TaskOnClickQuit);
        }
    }

    void TaskOnClickResume()
    {
        // Let the MenuManager know.
        resumeClicked?.Invoke();
    }

    void TaskOnClickBCI()
    {
        // Let the MenuManager know.
        bciClicked?.Invoke();
    }

    void TaskOnClickOptions()
    {
        // Let the MenuManager know.
        optionsClicked?.Invoke();
    }

    void TaskOnClickControls()
    {
        // Let the MenuManager know.
        controlsClicked?.Invoke();
    }

    void TaskOnClickQuit()
    {
        // Let the MenuManager know.
        quitClicked?.Invoke();
    }
}
