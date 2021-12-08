using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healingCodeScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject JakeyBoye;
    public GameObject chrisChan;
    public Vector2 target;
    public float targetX, targetY;
    public float speed = 5.0f;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        JakeyBoye = GameObject.FindGameObjectWithTag("DummyThicc");
        chrisChan = GameObject.FindGameObjectWithTag("ChrisChan");
        target = new Vector2(chrisChan.transform.position.x, chrisChan.transform.position.y);
        Invoke("selfDestory", 5.0f);
    }
    void Update()
    {
        if (JakeyBoye.GetComponent<bossScript>().partnerAlive)
        {
            targetX = chrisChan.transform.position.x;
            targetY = chrisChan.transform.position.y;
        } else
        {
            targetX = Player.transform.position.x;
            targetX = Player.transform.position.y;
        }
        target = new Vector2(targetX, targetY);
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("pBullet"))
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("ChrisChan"))
        {
            Destroy(this.gameObject);
        }
    }
    public void selfDestroy()
    {
        Destroy(this.gameObject);
    }
}
