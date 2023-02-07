// Chris Howard
// Capstone: 12 Apr 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacles : MonoBehaviour
{
    [SerializeField]
    public float speed = 5.0f;
    bool pause = false;

    // Update is called once per frame
    void Update()
    {
        if (pause == false)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }

        if (transform.position.z < 0)
        {
            this.gameObject.SetActive(false);
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
