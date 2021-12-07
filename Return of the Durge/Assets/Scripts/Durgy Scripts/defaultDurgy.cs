using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defaultDurgy : MonoBehaviour
{
    public int health = 5;

    public void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
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
