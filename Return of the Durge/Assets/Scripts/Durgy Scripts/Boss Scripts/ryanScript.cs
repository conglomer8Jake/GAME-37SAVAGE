using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ryanScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] eBoss;
    public Animator Anim;
    public string attack;

    public AudioSource cueSounds;
    public AudioClip cue1, cue2;
    public GameObject slashsoundsObject;

    public bool slashingAtPlayer = false;
    public bool dashingAtPlayer = false;
    public Transform targetToFace;
    private void Start()
    {
        Anim = this.gameObject.GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        eBoss = GameObject.FindGameObjectsWithTag("E");
        Player = GameObject.FindGameObjectWithTag("Player");
        if (eBoss.Length <= 0)
        {
            this.gameObject.GetComponent<bossScript>().partnerAlive = false;
        }
        if (transform.position.x > Player.transform.position.x)
        {
            flipSrite();
        }
        if (transform.position.x < Player.transform.position.x)
        {
            flipBack();
        }

        if (slashingAtPlayer == true)
        {
            targetToFace = Player.transform;
            Vector3 playerPos = targetToFace.position;
            Vector2 direction = new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y);

            transform.up = -direction;

            ForwardThrust(1.6f);
        }
        else if (slashingAtPlayer == false)
        {
            transform.right = new Vector2(0.0f, 0.0f);
        }
    }
    public void prepAttack()
    {
        Anim.SetBool("Finished", false);
        Anim.SetBool("IsAttacking", true);
        Invoke("findAttack", 0.1f);
        cueSounds.clip = cue1;
        cueSounds.Play();
    }
    public void findAttack()
    {
        if (attack == "slashStorm")
        {
            slashStorm();
        }
        if (attack == "dashAttack")
        {
            dashAttack();
        }
    }
    public void slashStorm()
    {
        slashingAtPlayer = true;
        Invoke("slashSounds", 0.5f);
        Anim.SetBool("SlashStorm", true);
        Invoke("finish", 3.5f);
    }
    public void dashAttack()
    {
        Anim.SetBool("DashAttacking", true);
        ForwardThrust(2.3f);
        Invoke("finish", 1.0f);
    }
    public void finish()
    {
        this.gameObject.GetComponent<FlockerScript>().SpeedPerSecond = 4.0f;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        Anim.SetBool("Finished", true);
        Anim.SetBool("SlashStorm", false);
        Anim.SetBool("DashAttacking", false);
        Anim.SetBool("IsAttacking", false);
        slashingAtPlayer = false;
    }
    public void flipSrite()
    {
        this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }
    public void flipBack()
    {
        this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }
    public void slashSounds()
    {
        GameObject flurryKnives = Instantiate(slashsoundsObject, this.gameObject.transform.position, Quaternion.identity);
        Destroy(flurryKnives, 3.4f);
    }
    private void ForwardThrust(float amount)
    {
        Vector3 force = transform.up * -amount;

        this.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
    }
}
