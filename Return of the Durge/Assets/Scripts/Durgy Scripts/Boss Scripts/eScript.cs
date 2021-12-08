using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eScript : MonoBehaviour
{
    public GameObject[] ryan;
    public Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    
    void Update()
    {
        ryan = GameObject.FindGameObjectsWithTag("Ryan");
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
    }
}
