// Chris Howard
// Capstone: 12 Apr 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; // Events

public class ControlsMenu : MonoBehaviour
{
    // Events
    public static Action backClicked;

    // Make sure to attach these Buttons in the Inspector
    public Button back;

    // Singleton
    static ControlsMenu instance;

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
        back.onClick.AddListener(TaskOnClickBack);
    }

    void TaskOnClickBack()
    {
        // Let the MenuManager know.
        backClicked?.Invoke();
    }
}
