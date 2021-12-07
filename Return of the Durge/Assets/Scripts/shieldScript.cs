using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldScript : MonoBehaviour
{
    public bool damagedRecent;
    public bool fullCap = true;
    public int shieldStrength = 10;
    public float combatTimer = 15.0f;
    void Update()
    {
        changeOpacity();
            if (combatTimer <= 0 && !fullCap)
            {
            damagedRecent = false;
            }
            if (!damagedRecent && !fullCap)
            {
                recharge();
            }
            if (damagedRecent)
            {
            combatTimer = 15.0f;
                cooldown();
            }
            if (shieldStrength < 10)
            {
                fullCap = false;
            }
    }
    public void cooldown()
    {
        combatTimer -= 1 * Time.deltaTime;
    }
    public void recharge()
    {
        shieldStrength += 1 * (int)Time.deltaTime;
        if (shieldStrength == 10)
        {
            fullCap = true;
        }
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("particle"))
        {
            shieldStrength--;
            damagedRecent = true;
        }
        if (other.gameObject.CompareTag("pBullet"))
        {
            shieldStrength--;
            damagedRecent = true;
        }
    }
    public void changeOpacity()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 236, 217, 255*(shieldStrength/10));
    }
}
