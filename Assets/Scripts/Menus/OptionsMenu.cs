// Chris Howard
// Capstone: 12 Apr 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; // Events

public class OptionsMenu : MonoBehaviour
{
    // Events
    public static Action traditionalClicked;
    public static Action mixedClicked;
    public static Action bciClicked;
    public static Action backClicked;

    // Make sure to attach these Buttons in the Inspector
    public Button traditional, mixed, bci, back;

    // Singleton
    static OptionsMenu instance;

    // Display an image of the VR binding interface, any key should return to the main menu.
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

        //  Calls the TaskOnClick method when you click the Button
        traditional.onClick.AddListener(TaskOnClickTraditional);
        mixed.onClick.AddListener(TaskOnClickMixed);
        bci.onClick.AddListener(TaskOnClickBCI);
        back.onClick.AddListener(TaskOnClickBack);
    }

    void TaskOnClickTraditional()
    {
        //  Let the MenuManager know.
        traditionalClicked?.Invoke();
    }

    void TaskOnClickMixed()
    {
        //  Let the MenuManager know.
        mixedClicked?.Invoke();
    }

    void TaskOnClickBCI()
    { 
        //  Let the MenuManager know.
       bciClicked?.Invoke();
    }

    void TaskOnClickBack()
    {
        //  Let the MenuManager know.
        backClicked?.Invoke();
    }
}
