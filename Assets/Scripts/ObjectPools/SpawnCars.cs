// Chris Howard
// Capstone: 12 Apr 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCars : MonoBehaviour
{
    [SerializeField]
    GameObject car;

    GameObject[] carPool;

    int maxCars;

    // Random Range between left 520 and right 431. (On Road).
    private Vector3 spawnPos;

    bool spawn = false;

    // Start is called before the first frame update
    void Start()
    {
        maxCars = 10;
        carPool = new GameObject[maxCars];

        for (int i = 0; i < maxCars; i++)
        {
            carPool[i] = Instantiate(car);
            carPool[i].SetActive(false);
        }
        InvokeRepeating("SpawnObstacle", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        float randomX = Random.Range(430, 520);
        float randomY = Random.Range(12, 230);
        float randomZ = Random.Range(-40, 2000);
        spawnPos = new Vector3(randomX, randomY, randomZ);
    }

    void SpawnObstacle()
    {
        GameObject temp = GetObstacle();
        if (temp != null)
        {
            temp.transform.position = spawnPos;
        }
    }

    GameObject GetObstacle()
    {
        int j = 0;
        foreach (GameObject i in carPool)
        {
            if (!carPool[j].activeSelf)
            {
                carPool[j].SetActive(true);
                return carPool[j];
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
