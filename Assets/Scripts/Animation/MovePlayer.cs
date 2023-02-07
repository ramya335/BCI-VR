// Chris Howard
// Capstone: 12 Apr 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    public float speed = 5.0f;
    [SerializeField]
    public float leftBoundary = 435f;
    [SerializeField]
    public float rightBoundary = 525f;
    [SerializeField]
    public float finishBoundary = 2020f;
    [SerializeField]
    public float startBoundary = -63f;

    bool pause = false;

    public static Action gameWinner;

    // Update is called once per frame
    void Update()
    {
        //If game not paused and not hitting left or right boundary;
        if (pause == false && gameObject.transform.position.x > leftBoundary 
                && gameObject.transform.position.x < rightBoundary
                // Player not at end or before start.
                && gameObject.transform.position.z > startBoundary
                && gameObject.transform.position.z < finishBoundary)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        if (gameObject.transform.position.z >= finishBoundary)
        {
            gameWinner?.Invoke();
        }
    }

    void RestartGame()
    {
        pause = false;
    }

    void PauseGame()
    {
        pause = true;
    }

    private void OnEnable()
    {
        EventPause.pauseGame += PauseGame;
        EventRestart.restartGame += RestartGame;
    }

    private void OnDisable()
    {
        EventPause.pauseGame -= PauseGame;
        EventRestart.restartGame -= RestartGame;
    }
}
