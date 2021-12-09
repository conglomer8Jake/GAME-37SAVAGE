using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundScript : MonoBehaviour
{
    public GameObject soundObj;
    public bool isFadingOut, isFadingIn;
    public AudioSource audio;
    void Start()
    {
        audio = this.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        float startVolume = audio.volume;
        if (isFadingOut)
        {
            audio.volume -= startVolume * Time.deltaTime;
        }
        if (isFadingIn)
        {
            audio.volume += startVolume * Time.deltaTime;
        }
    }
    public void fadeOut()
    {
        isFadingOut = true;
        isFadingIn = false;
    }
    public void fadeIn()
    {
        isFadingIn = true;
        isFadingOut = false;
    }
}
