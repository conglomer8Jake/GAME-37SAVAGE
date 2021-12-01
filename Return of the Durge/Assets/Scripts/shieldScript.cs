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
            if (!damagedRecent && !fullCap)
            {
                recharge();
            }
            if (damagedRecent)
            {
                cooldown();
            }
            if (shieldStrength <= 10)
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
        if (other.gameObject.CompareTag("particlee"))
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
