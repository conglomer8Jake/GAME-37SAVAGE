using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public playerMovementHandler pMH;
    public Vector2 clickedMousePos;
    public float speed;
    public float directionPosX, directionPosY;
    void Start()
    {
        pMH = GameObject.FindObjectOfType<playerMovementHandler>();
        directionPosX = pMH.mousePosX - pMH.transform.position.x;
        directionPosY = pMH.mousePosY - pMH.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(directionPosX*100.0f, directionPosY*100.0f), distance);
        Invoke("selfDestroy", 5.0f);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            selfDestroy();
        }
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("pBullet"))
        {
            selfDestroy();
        }
    }
    public void selfDestroy()
    {
        Destroy(this.gameObject);
    }
}
