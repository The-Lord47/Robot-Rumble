using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonStart : MonoBehaviour
{
    public void OnButtonPress()
    {
        SceneManager.LoadScene(1);
    }
}
