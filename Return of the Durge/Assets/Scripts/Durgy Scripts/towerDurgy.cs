using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerDurgy : MonoBehaviour
{
    public playerMovementHandler pMH;
    public FlockerScript fS;
    void Start()
    {
        pMH = GameObject.FindObjectOfType<playerMovementHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerStay2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("Player"))
        {
            pMH.speed = 2;
        }
        if (!other.gameObject.CompareTag("Player"))
        {
            fS.SpeedPerSecond *= 2;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pMH.speed *= 1.5f;
        }
        if (!other.gameObject.CompareTag("Player"))
        {
            fS.SpeedPerSecond /= 2;
        }
    }
}
