using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject credits;
    public TMP_Text credits_txt;
    public GameObject titleScreen;
    float timer;
    float creditsTime = 11.5f;
    bool creditsOver;
    float titleScreenScrollSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(creditsScene());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        credits_txt.color = new Color(credits_txt.color.r, credits_txt.color.g, credits_txt.color.b, /*Mathf.Pow(3, timer - creditsTime)*/ (timer/creditsTime));

        if (creditsOver)
        {
            titleScreen.transform.position += (titleScreenScrollSpeed * Time.deltaTime * Vector3.down);
            credits.transform.position += (titleScreenScrollSpeed * Time.deltaTime * Vector3.down);
        }

        if(titleScreen.transform.position.y <= 0)
        {
            creditsOver = false;
        }

    }


    IEnumerator creditsScene()
    {
        yield return new WaitForSeconds(creditsTime);
        creditsOver = true;
    }
}
