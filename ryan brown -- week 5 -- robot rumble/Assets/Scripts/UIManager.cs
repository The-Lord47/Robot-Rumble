using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    //----------------------PUBLIC VARIABLES----------------------
    [Header("Text References")]
    public TMP_Text score_txt;
    public TMP_Text lives_txt;
    public TMP_Text prestige_txt;
    public TMP_Text wave_txt;
    [Header("Passable Variables")]
    public int score = 0;

    //----------------------PRIVATE VARIABLES----------------------
    playerMovement _playerScript;
    spawnManager _spawnScript;


    //----------------------START----------------------
    void Start()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        _spawnScript = GameObject.FindGameObjectWithTag("enemySpawner").GetComponent<spawnManager>();
    }

    //----------------------UPDATE----------------------
    void Update()
    {
        //displays text with updates values in them
        lives_txt.text = "Lives: " + _playerScript.lives;
        prestige_txt.text = "Prestige: " + _spawnScript.prestigeMultiplier;
        score_txt.text = "Score: " + score;
        if(_spawnScript.waveIndex == 0)
        {
            wave_txt.text = "Wave 10";
        }
        else
        {
            wave_txt.text = "Wave " + (_spawnScript.waveIndex);
        }
    }
}
