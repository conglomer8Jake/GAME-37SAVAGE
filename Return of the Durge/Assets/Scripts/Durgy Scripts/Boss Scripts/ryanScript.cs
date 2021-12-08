using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ryanScript : MonoBehaviour
{
    public GameObject[] eBoss;
    public Animator Anim;
    public string attack;
    private void Start()
    {
        Anim = this.gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        eBoss = GameObject.FindGameObjectsWithTag("E");
        if (eBoss.Length <= 0)
        {
            this.gameObject.GetComponent<bossScript>().partnerAlive = false;
        }
    }
    public void prepAttack()
    {
        Anim.SetBool("Finished", false);
        Anim.SetBool("IsAttacking", true);
        Invoke("findAttack", 0.1f);
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
        Anim.SetBool("SlashStorm", true);
        Invoke("finish", 3.5f);
    }
    public void dashAttack()
    {
        Anim.SetBool("DashAttacking", true);
        Invoke("finish", 1.0f);
    }
    public void finish()
    {
        this.gameObject.GetComponent<FlockerScript>().SpeedPerSecond = 4.0f;
        Anim.SetBool("Finished", true);
        Anim.SetBool("SlashStorm", false);
        Anim.SetBool("DashAttacking", false);
        Anim.SetBool("IsAttacking", false);
    }
}
