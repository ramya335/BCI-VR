// Chris Howard
// Capstone: 12 Apr 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventRestart : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    Vector3 playerStart;

    public static Action restartGame;

    private void Start()
    {
        playerStart = new Vector3(474.47f, 5.08f, 8.48f);
    }

    void newGame()
    {
        //Reset Player Position.
        player.transform.position = playerStart;
        restartGame?.Invoke();
    }

    void restart()
    {
        restartGame?.Invoke();
    }

    private void OnEnable()
    {
        MenuManager.newGame += newGame;
        MenuManager.menuClosed += restart;
    }

    private void OnDisable()
    {
        MenuManager.newGame -= newGame;
        MenuManager.menuClosed -= restart;
    }
}
