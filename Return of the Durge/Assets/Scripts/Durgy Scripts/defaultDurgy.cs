using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defaultDurgy : MonoBehaviour
{
    public int health = 5;

    public GameObject deathSoundObject;
    public void Update()
    {
        if (health <= 0)
        {
            GameObject deathSound = Instantiate(deathSoundObject, this.gameObject.transform.position, Quaternion.identity);
            Destroy(deathSound, 0.7f);

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
