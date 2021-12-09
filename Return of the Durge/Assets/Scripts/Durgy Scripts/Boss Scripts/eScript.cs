using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] ryan;
    public Animator anim;

    public GameObject ReticleTarget;
    public AudioClip reticleFire;

    public float slaughtTimer = 5.0f;
    public bool firingOnPlayer = false;

    public AudioSource eSource;
    public AudioClip eAttack1, eAttack2, eIdle1, eIdle2;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        ryan = GameObject.FindGameObjectsWithTag("Ryan");
        Player = GameObject.FindGameObjectWithTag("Player");
        if (ryan.Length <= 0)
        {
            this.gameObject.GetComponent<bossScript>().partnerAlive = false;
        }
        if (this.gameObject.GetComponentInChildren<ParticleSystem>().isPlaying)
            {
                anim.SetBool("AttackAnim1", true);
                Invoke("revertAnim", 4.5f);
            }
            else
            {
                anim.SetBool("AttackAnim1", false);
            }
        if (transform.position.x > Player.transform.position.x)
        {
            flipSrite();
        }
        if (transform.position.x < Player.transform.position.x)
        {
            flipBack();
        }

    }
    public void revertAnim()
    {
        anim.SetBool("Finished", true);
        Invoke("newAbility", 5.5f);
    }
    public void newAbility()
    {
        anim.SetBool("Finished", false);
        anim.SetBool("AttackAnim1", false);
        anim.SetBool("AttackAnim2", false);
    }
    public void flipSrite()
    {
        this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }
    public void flipBack()
    {
        this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

    public void SplurshAttack()
    {
        anim.SetBool("AttackAnim2", true);
        anim.SetBool("Finished", false);
        InvokeRepeating("FireOnPlayer", 1.0f, 1.0f);
        Invoke("dont", 7.0f);
        firingOnPlayer = true;
        slaughtTimer = 7.0f;
        Debug.Log("Firing On Player");
        Invoke("revertAnim", 7.0f);

        eSource.clip = eAttack1;
        eSource.Play();
    }

    public void FireOnPlayer()
    {
        GameObject TargetFire = Instantiate(ReticleTarget, Player.transform.position, Quaternion.identity);
        Destroy(TargetFire, 1.3f);
    }

    public void dont()
    {
        CancelInvoke("FireOnPlayer");
    }
}
