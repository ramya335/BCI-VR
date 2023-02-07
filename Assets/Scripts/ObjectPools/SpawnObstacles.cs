// Chris Howard
// Capstone: 12 Apr 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles: MonoBehaviour
{
    [SerializeField]
    GameObject obstacle;

    GameObject[] obstaclePool;

    int maxObstacles;

    // Random Range between left 520 and right 431. (On Road).
    private Vector3 spawnPos;

    bool spawn = false;


    // Start is called before the first frame update
    void Start()
    {
        maxObstacles = 20;
        obstaclePool = new GameObject[maxObstacles];

        for (int i=0; i<maxObstacles; i++)
        {
            obstaclePool[i] = Instantiate(obstacle);
            obstaclePool[i].SetActive(false);
        }
        InvokeRepeating("SpawnObstacle", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        float randomX = Random.Range(430, 520);
        float randomZ = Random.Range(-40, 2000);
        spawnPos = new Vector3(randomX, 1, randomZ);
    }

    void SpawnObstacle ()
    {
        if (spawn == true)
        {
            GameObject temp = GetObstacle();
            if (temp != null)
            {
                temp.transform.position = spawnPos;
            }
        }
    }

    GameObject GetObstacle()
    {
        int j = 0;
        foreach (GameObject i in obstaclePool)
        {
            if (!obstaclePool[j].activeSelf)
            {
                obstaclePool[j].SetActive(true);
                return obstaclePool[j];
            }
            j++;
        }
        return null;
    }

    void pauseSpawning()
    {
        spawn = false;
    }

    void startSpawning()
    {
        spawn = true;
    }

    private void OnEnable()
    {
        EventPause.pauseGame += pauseSpawning;
        EventRestart.restartGame += startSpawning;
        EventEnd.endGame += pauseSpawning;
    }

    private void OnDisable()
    {
        EventPause.pauseGame -= pauseSpawning;
        EventRestart.restartGame -= startSpawning;
        EventEnd.endGame -= pauseSpawning;
    }
}
