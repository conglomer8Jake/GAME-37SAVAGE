using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ninjaDurgy : MonoBehaviour
{
    public Animator anim;
    public GameObject Ninja;
    public ninjaDurgy ninjaScript;
    public ParticleSystem ninjaSystem;
    public AudioSource ninjaSource;
    public AudioClip ninjaThrow, ninjaPoof, ninjaDeath;

    public float shootTimer = 6.0f;
    public bool timerOn;
    public int health = 5;
    void Start()
    {
        ninjaScript = GameObject.FindObjectOfType<ninjaDurgy>();
        Ninja = ninjaScript.gameObject;
        ninjaSystem = Ninja.GetComponentInChildren<ParticleSystem>();
        anim = gameObject.GetComponent<Animator>();
    }
    public void Update()
    {
        if (ninjaSystem.GetComponentInChildren<ParticleSystem>().isStopped)
        {
            shadowWalk();
            timerOn = true;
        }
        if (timerOn)
        {
            shootTimer -= 1 * Time.deltaTime;
        }
        if (shootTimer <= 0)
        {
            shoot();
            timerOn = false;
            shootTimer = 6.0f;
        }
        if (anim.GetBool("isShooting"))
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (health <= 5)
        {
            Destroy(this.gameObject);
        }
    }
    public void shadowWalk()
    {
        anim.SetBool("isShooting", false);
        disappear();
    }
    public void shoot()
    {
        ninjaSystem.GetComponentInChildren<ParticleSystem>().Play();
        anim.SetBool("isShooting", true);
        ninjaSource.clip = ninjaThrow;
        ninjaSource.Play();
    }
    public void disappear()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        ninjaSource.clip = ninjaPoof;
        ninjaSource.Play();
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("pBullet"))
        {
            health--;
        }
    }
}