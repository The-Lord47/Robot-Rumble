using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupSpawnManager : MonoBehaviour
{
    //----------------------PUBLIC VARIABLES----------------------
    [Header("Object References")]
    public GameObject[] powerups;
    //----------------------PRIVATE VARIABLES----------------------
    float spawnRange = 9;
    int powerUpCount;
    playerMovement _playerScript;

    //----------------------START----------------------
    void Start()
    {
        //----------------------REFERENCES----------------------
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
    }

    //----------------------UPDATE----------------------
    void Update()
    {
        //----------------------POWERUP SPAWNER----------------------
        //finds any powerups currently on the field
        powerUpCount = FindObjectsOfType<powerupRotation>().Length;

        //checks if there is currently a powerup on the field
        if (_playerScript.hasMissile == false && _playerScript.hasShield == false && powerUpCount == 0)
        {
            Spawner();
        }
    }

    //----------------------SPAWN FUNCTION----------------------
    void Spawner()
    {
        Instantiate(powerups[Random.Range(0, powerups.Length)], SpawnPos(), transform.rotation, transform);
    }

    //----------------------SPAWN POSITION RANDOMISER----------------------
    Vector3 SpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
