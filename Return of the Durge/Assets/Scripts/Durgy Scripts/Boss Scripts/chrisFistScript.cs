using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chrisFistScript : MonoBehaviour
{
    public string currentState = "returning";
    public GameObject Player;
    public Vector2 target;
    public float speed = 4.5f;
    public bool isPunching = false;
    public bool isInvoked = false;
    public float punchCooldown = 5.0f;
    void Start()
    {
        Player = this.gameObject.GetComponentInParent<chrisChanBOSS>().Player;
    }

    void Update()
    {
        Player = this.gameObject.GetComponentInParent<chrisChanBOSS>().Player;
        punchCooldown -= 1 * Time.deltaTime;
        if (this.gameObject.CompareTag("chrisFistL"))
        {
            float step = speed * Time.deltaTime;
            if (isPunching && !isInvoked)
            {
                float randTimeA = Random.Range(0.0f, 0.75f);
                Invoke("randomPunchTime", randTimeA);
            }
            if (!isPunching)
            {
                target = GetComponentInParent<chrisChanBOSS>().lFistHome.transform.position;
                transform.position = Vector2.MoveTowards(transform.position, target, step);
            }
        }
        if (this.gameObject.CompareTag("chrisFistR"))
        {
            float step = speed * Time.deltaTime;
            if (isPunching && !isInvoked)
            {
                float randTimeB = Random.Range(0.0f, 0.75f);
                Invoke("randomPunchTime", randTimeB);
            }
            if (!isPunching)
            {
                target = GetComponentInParent<chrisChanBOSS>().rFistHome.transform.position;
                transform.position = Vector2.MoveTowards(transform.position, target, step);
            }
        }
        if (punchCooldown <= 0)
        {
            isPunching = true;
            Invoke("returnPunch", 4.5f);
        }
        if (isPunching)
        {
            randomPunchTime();
        }
    }
    public void returnPunch()
    {
        isInvoked = false;
        isPunching = false;
        punchCooldown = 5.5f;
    }
    public void randomPunchTime()
    {
        isInvoked = true;
        float step = speed * Time.deltaTime;
        target = Player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("pBullet"))
        {
            speed /= 2;
            Invoke("speedNormalize", 0.5f);
        }
    }
    public void speedNormalize()
    {
        speed = 4.5f;
    }
}
