// Chris Howard
// Capstone: 12 Apr 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventEnd : MonoBehaviour
{
    [SerializeField]
    GameObject audioSourceLose;

    [SerializeField]
    GameObject audioSourceWin;

    [SerializeField]
    string obstacleTag = "Obstacle";

    AudioSource audioLose;
    AudioSource audioWin;

    public static Action endGame;

    void Start()
    {
        audioLose = audioSourceLose.GetComponent<AudioSource>();
        audioWin = audioSourceWin.GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == obstacleTag)
        {
            // Calling Game Over here will not work due to Unity Game Loop.
            audioLose.Play();
            endGame?.Invoke();
        }
    }

    void GameWinner()
    {
        audioWin.Play();
        endGame?.Invoke();
    }

    private void OnEnable()
    {
        MovePlayer.gameWinner += GameWinner;
    }

    private void OnDisable()
    {
        MovePlayer.gameWinner -= GameWinner;
    }
}
