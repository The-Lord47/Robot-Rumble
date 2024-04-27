using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //----------------------PUBLIC VARIABLES----------------------
    [Header("Object References")]
    public TMP_Text score_txt;
    public TMP_Text lives_txt;
    public TMP_Text prestige_txt;
    public TMP_Text wave_txt;
    public Image dashCooldown;
    public GameObject gunUI;
    public Image gunCooldown;
    public GameObject shieldUI;
    public Image shieldCooldown;
    public GameObject gameplayUI;
    public GameObject gameoverUI;
    public Button playAgain;
    public Button mainMenu;
    [Header("Passable Variables")]
    public int score = 0;

    //----------------------PRIVATE VARIABLES----------------------
    playerMovement _playerScript;
    spawnManager _spawnScript;
    float missileTimer;
    float shieldTimer;
    bool playAgainBool;


    //----------------------START----------------------
    void Start()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        _spawnScript = GameObject.FindGameObjectWithTag("enemySpawner").GetComponent<spawnManager>();
        Time.timeScale = 1f;
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
        dashCooldown.rectTransform.sizeDelta = new Vector2(199.203f, 215.054f * _playerScript.timer);

        gunUI.SetActive(_playerScript.hasMissile);
        if (_playerScript.hasMissile)
        {
            missileTimer += Time.deltaTime;
            gunCooldown.rectTransform.sizeDelta = new Vector2(199.203f, 215.054f * ((5 - missileTimer)/5));
        }
        else
        {
            missileTimer = 0;
        }

        shieldUI.SetActive(_playerScript.hasShield);
        if(_playerScript.hasShield)
        {
            shieldTimer += Time.deltaTime;
            shieldCooldown.rectTransform.sizeDelta = new Vector2(199.203f, 215.054f * ((5 - shieldTimer) / 5));
        }
        else
        {
            shieldTimer = 0;
        }


        //----------------------GAME OVER SYSTEM----------------------
        if (_playerScript.lives <= 0)
        {
            gameplayUI.SetActive(false);
            gameoverUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
