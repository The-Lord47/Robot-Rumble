using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupSpawnManager : MonoBehaviour
{

    public GameObject[] powerups;
    public float spawnRange = 9;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawner", 2, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawner()
    {
        Instantiate(powerups[Random.Range(0, powerups.Length)], SpawnPos(), transform.rotation, transform);
    }

    Vector3 SpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
