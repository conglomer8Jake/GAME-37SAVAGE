using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public playerMovementHandler pMH;
    public Vector2 clickedMousePos;
    public float speed;
    void Start()
    {
        pMH = GameObject.FindObjectOfType<playerMovementHandler>();
        clickedMousePos = new Vector2(pMH.mousePosX, pMH.mousePosY);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, clickedMousePos, distance);
        Invoke("selfDestroy", 5.0f);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            selfDestroy();
        }
    }
    public void selfDestroy()
    {
        Destroy(this.gameObject);
    }
}
