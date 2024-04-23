using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnRange = 9;
    public int spawnCount = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("Spawner", 5, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnCount < 3)
        {
            Spawner();
            spawnCount++;
        }
    }

    void Spawner()
    {
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], SpawnPos(), transform.rotation, transform);
    }

    Vector3 SpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3 (spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
