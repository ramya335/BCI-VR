// Jason Smith and Chris Howard
// Capstone
// 13 Apr 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TimeManager : MonoBehaviour
{
    static TimeManager instance;

    // Start is called before the first frame update
    bool timerActive = false;
    float currentTime;

    int score;
    public float multiplier = 5;

    public static Action<int> sendFinalScore;

    void Start()
    {
        //Singleton
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            currentTime = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        score = Mathf.RoundToInt(currentTime * multiplier);
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        //Broadcast collect event. Allows you to pass in a value (FinalScore).
        sendFinalScore?.Invoke(score);
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    public void ResetTimer()
    {
        timerActive = false;
        currentTime = currentTime - currentTime;
        score = score - score;
    }

    private void OnEnable()
    {
        MainMenu.resumeClicked += StartTimer;
        MenuManager.menuOpen += StopTimer;
        EventEnd.endGame += StopTimer;
        MenuManager.newGame += ResetTimer;
    }

    private void OnDisable()
    {
        MainMenu.resumeClicked -= StartTimer;
        MenuManager.menuOpen -= StopTimer;
        EventEnd.endGame -= StopTimer;
        MenuManager.newGame -= ResetTimer;
    }
}

