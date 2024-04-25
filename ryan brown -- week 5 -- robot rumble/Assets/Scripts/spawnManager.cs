using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    //-------------------PUBLIC VARIABLES-------------------
    [Header("Enemy Array")]
    public GameObject[] enemyPrefabs;
    [Header("Passable Variables")]
    public int prestigeMultiplier = 1;
    public int waveIndex = 0;

    //-------------------PRIVATE VARIABLES-------------------
    float spawnRange = 9;
    int enemyCount = 0;
    

    //-------------------WAVES-------------------
    int[][] allWaves = new int[][] 
    { 
        new int[]{ 1 }, //wave1
        new int[]{ 1, 1 }, //wave2
        new int[]{ 2 }, //wave 3
        new int[]{ 1, 1, 2 }, //wave 4
        new int[]{ 3 }, //wave 5
        new int[]{ 1, 2, 3 }, //wave6
        new int[]{ 1, 1, 2, 2, 3 }, // wave7
        new int[]{ 1, 2, 3, 3 }, //wave8
        new int[]{ 1, 1, 2, 2, 3, 3 }, //wave9
        new int[]{ 1, 1, 1, 2, 2, 2, 3, 3, 3} //wave10
    };

    //-------------------UPDATE-------------------
    void Update()
    {
        //finds the number of enemies in the game each frame
        enemyCount = FindObjectsOfType<enemyMovement>().Length;

        //when all enemies are knocked off, a new wave spawns
        if(enemyCount == 0)
        {
            //grabs each enemy index from the current wave and gives it to the spawner
            foreach (int prefabIndex in allWaves[waveIndex])
            {
                //multiplies the number of gameobjects spawned by the prestige multiplier
                for(int i = 0; i < prestigeMultiplier; i++)
                {
                    Spawner(prefabIndex - 1);
                }
            }
            //checks if the current wave is the final wave and increases the prestige count if it is as well as resetting the wave index to 0
            if(waveIndex == 9)
            {
                //multiplies the number of bosses spawned by the prestige multiplier
                for(int i = 0; i < prestigeMultiplier; i++)
                {
                    Spawner(3);
                }
                ++prestigeMultiplier;
                waveIndex = 0;
            }
            //increases the wave count
            else
            {
                ++waveIndex;
            }
        }
    }

    //-------------------SPAWNER SCRIPT-------------------
    void Spawner(int prefabIndex)
    {
        //spawns an enemy
        Instantiate(enemyPrefabs[prefabIndex], SpawnPos(), transform.rotation, transform);
    }

    //-------------------SPAWN POSITION RANDOMISER-------------------
    Vector3 SpawnPos()
    {
        //randomises two coordinates, x and z, inside the spawn range
        float spawnPosX = UnityEngine.Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = UnityEngine.Random.Range(-spawnRange, spawnRange);
        //combines the two coordinates into a vector3
        Vector3 randomPos = new Vector3 (spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
