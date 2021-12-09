using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grantScript : MonoBehaviour
{
    public GameObject topR, topL, botR;
    public GameObject rFist, lFist;

    public Animator anim;

    public int health = 75;
    public int fistsActive;

    public float spawnFistTimer = 15.0f;

    public string selfState;
    void Start()
    {
        selfState = "calm";
        anim = this.gameObject.GetComponent<Animator>();
        topR = GameObject.FindGameObjectWithTag("anchorTop");
        topL = GameObject.FindGameObjectWithTag("anchorLeft");
        botR = GameObject.FindGameObjectWithTag("anchorBot");
    }

    // Update is called once per frame
    void Update()
    {
        spawnFistTimer -= 1 * Time.deltaTime;
        if (health <= (36))
        {
            anim.SetBool("isLowHealth", true);
            selfState = "panic";
        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        if (spawnFistTimer <= 0)
        {
            if (fistsActive < 2)
            {
                spawnFist();
            } else
            {
                resetFistTimer();
            }
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
    public void resetFistTimer()
    {
        if (selfState == "calm")
        {
            spawnFistTimer = 15.0f;
        } else if (selfState == "panic")
        {
            spawnFistTimer = 9.0f;
        }
    }
    public void resetDamageBool()
    {
        anim.SetBool("isDamaged", false);
    }
    public void spawnFist()
    {
        float randX = Random.Range(topL.transform.position.x, topR.transform.position.x);
        float randY = Random.Range(botR.transform.position.y, topR.transform.position.y);
        int randFist = Random.Range(0, 10);
        if (randFist <= 5)
        {
            Instantiate(lFist, new Vector2(randX, randY), Quaternion.identity);
            fistsActive++;
            resetFistTimer();
        }
        else
        {
            Instantiate(rFist, new Vector2(randX, randY), Quaternion.identity);
            fistsActive++;
            resetFistTimer();
        }
    }
}