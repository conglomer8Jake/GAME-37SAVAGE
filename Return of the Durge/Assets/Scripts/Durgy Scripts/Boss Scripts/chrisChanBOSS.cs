using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chrisChanBOSS : MonoBehaviour
{
    public Animator anim;
    public GameObject[] jakeBoss;
    public GameObject Player;
    public GameObject lFist, rFist, lFistHome, rFistHome;

    public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = this.gameObject.GetComponent<Animator>();

        lFist = GameObject.FindGameObjectWithTag("chrisFistL");
        rFist = GameObject.FindGameObjectWithTag("chrisFistR");
        lFistHome = GameObject.FindGameObjectWithTag("lFistHome");
        rFistHome = GameObject.FindGameObjectWithTag("rFistHome");
    }
    public void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        jakeBoss = GameObject.FindGameObjectsWithTag("DummyThicc");
        if (jakeBoss.Length <= 0)
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
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("healing"))
        {
            this.gameObject.GetComponent<bossScript>().health++;
        } else
        {

        }
    }
    public void jumpKick()
    {
        anim.SetBool("JumpAttacking", true);
        Invoke("kicking", 1.5f);
    }
    public void jumpSlam()
    {
        anim.SetBool("JumpAttacking", true);
        Invoke("slamming", 1.5f);
    }
    public void kicking()
    {
        anim.SetBool("Kicking", true);
        if (Player.transform.position.x > transform.position.x)
        {
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(500.0f, 0.0f));
        } else if (Player.transform.position.x < transform.position.x)
        {
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500.0f, 0.0f));
        }
        Invoke("finished", 1.5f);
    }
    public void slamming()
    {
        anim.SetBool("Slamming", true);
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, -500.0f));
        Invoke("finished", 1.5f);
    }
    public void finished()
    {
        anim.SetBool("Finished", true);
        anim.SetBool("Slamming", false);
        anim.SetBool("Kicking", false);
        anim.SetBool("JumpAttacking", false);
        Invoke("finisher", 0.1f);
    }
    public void finisher()
    {
        anim.SetBool("Finished", false);
    }
    public void flipSrite()
    {
        this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }
    public void flipBack()
    {
        this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }
}
