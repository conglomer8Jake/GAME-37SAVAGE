using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fistScript : MonoBehaviour
{
    public Vector2 target;
    public Animator anim;
    public GameObject Player;
    public grantScript GS;
    public float distanceToPlayer;
    public float speed = 3.0f;
    public int health = 10;
    void Start()
    {
        GS = GameObject.FindObjectOfType<grantScript>();
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = this.gameObject.GetComponent<Animator>();
        target = Player.transform.position;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);
        distanceToPlayer = Player.transform.position.x - transform.position.x;
        if (distanceToPlayer <= 5.0f)
        {
            punch();
            anim.SetBool("inRange", true);
            Invoke("punchCooldown", 1.0f);
        } else
        {
            //chill
        }
        //flip sprite based on relation to player position
        {
            if (this.gameObject.CompareTag("rFist"))
            {
                if (this.gameObject.transform.position.x < Player.transform.position.x)
                {
                    flipSprite();
                }
                else
                {
                    flipSpriteBack();
                }
            }
            if (this.gameObject.CompareTag("lFist"))
            {
                if (this.gameObject.transform.position.x > Player.transform.position.x)
                {
                    flipSprite();
                }
                else
                {
                    flipSpriteBack();
                }
            }
        }
        if (health <= 0)
        {
            GS.fistsActive--;
            Destroy(this.gameObject);
        }
    }
    public void flipSprite()
    {
        this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }
    public void flipSpriteBack()
    {
        this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }
    //TESTING ONLY
    public void reTargetPlayer()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void punchCooldown()
    {
        anim.SetBool("onCooldown", true);
        reTargetPlayer();
    }
    public void punch()
    {
        target = transform.position;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("pBullet"))
        {
            health--;
        }
    }
}
