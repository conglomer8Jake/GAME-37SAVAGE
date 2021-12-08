using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grantScript : MonoBehaviour
{
    public Animator anim;
    public int health = 75;
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= (36))
        {
            anim.SetBool("isLowHealth", true);
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("pBullet"))
        {
            health--;
            anim.SetBool("isDamaged", true);
            Invoke("resetDamageBool", 1.0f);
        }
    }
    public void resetDamageBool()
    {
        anim.SetBool("isDamaged", false);
    }
}
