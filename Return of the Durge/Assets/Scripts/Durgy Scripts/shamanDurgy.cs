using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shamanDurgy : MonoBehaviour
{
    public ParticleSystem PS;
    public AudioSource shamSource;
    public AudioClip shamShoot, shamDeath;
    void Start()
    {
        PS = gameObject.GetComponentInChildren<ParticleSystem>();
    }


    void Update()
    {
        if (PS.isPlaying)
        {
            shamSource.clip = shamShoot;
            shamSource.Play();
            Debug.Log("SUI");
        }
    }
}
