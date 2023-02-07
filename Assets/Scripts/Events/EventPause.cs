// Chris Howard
// Capstone: 12 Apr 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventPause : MonoBehaviour
{
    public static Action pauseGame;

    void pause()
    {
        pauseGame?.Invoke();
    }

    private void OnEnable()
    {
        MenuManager.menuOpen += pause;
        EventEnd.endGame += pause;
    }

    private void OnDisable()
    {
        MenuManager.menuOpen -= pause;
        EventEnd.endGame -= pause;
    }
}
