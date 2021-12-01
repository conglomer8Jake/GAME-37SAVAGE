using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerDurgy : MonoBehaviour
{
    public playerMovementHandler pMH;
    public FlockerScript fS;

    public int health = 5;
    void Start()
    {
        pMH = GameObject.FindObjectOfType<playerMovementHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("Player"))
        {
            pMH.speed = 2;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pMH.speed *= 1.5f;
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
