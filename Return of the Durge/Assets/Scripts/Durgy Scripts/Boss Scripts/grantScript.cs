using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grantScript : MonoBehaviour
{
    public GameObject stairs;
    public GameObject topR, topL, botR;
    public centerScript CS;

    public Animator anim;

    public int health = 75;
    public int fistsActive;

    public float spawnTimer = 10.0f;

    public string selfState;
    void Start()
    {
        selfState = "calm";
        anim = this.gameObject.GetComponent<Animator>();
        topR = GameObject.FindGameObjectWithTag("anchorTop");
        topL = GameObject.FindGameObjectWithTag("anchorLeft");
        botR = GameObject.FindGameObjectWithTag("anchorBot");
        CS = GameObject.FindObjectOfType<centerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= 1 * Time.deltaTime;
        if (health <= (36))
        {
            anim.SetBool("isLowHealth", true);
            selfState = "panic";
        }
        if (health <= 0)
        {
            Instantiate(stairs, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (spawnTimer <= 0)
        {
            CS.spawnEnemies();
            spawnTimer = 45.0f;
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