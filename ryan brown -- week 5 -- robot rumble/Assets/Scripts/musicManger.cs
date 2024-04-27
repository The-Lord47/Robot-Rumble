using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManger : MonoBehaviour
{
    public GameObject musicStart;
    public GameObject musicLoop;

    // Start is called before the first frame update
    void Start()
    {
        musicStart.GetComponent<AudioSource>().Play();
        StartCoroutine(changeTrack());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator changeTrack()
    {
        yield return new WaitForSeconds(15.765f);
        musicLoop.GetComponent<AudioSource>().Play();
    }
}
