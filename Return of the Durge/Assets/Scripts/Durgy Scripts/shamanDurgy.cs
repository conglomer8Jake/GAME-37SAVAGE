using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shamanDurgy : MonoBehaviour
{
    //public ParticleSystem PS;
    //Particle system keeps breaking because of this???
    public AudioSource shamSource;
    public AudioClip shamShoot, shamDeath;
    public int health = 5;

    public GameObject deathSoundObject;
    void Start()
    {
        //PS = gameObject.GetComponentInChildren<ParticleSystem>();
    }


    void Update()
    {
        /*if (PS.isPlaying)
        {
            shamSource.clip = shamShoot;
            shamSource.Play();
        }
        */
        if (health <= 0)
        {
            GameObject deathSound = Instantiate(deathSoundObject, this.gameObject.transform.position, Quaternion.identity);
            Destroy(deathSound, 0.7f);

            Destroy(this.gameObject);
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("pBullet"))
        {
            health--;
        }
    }
}
