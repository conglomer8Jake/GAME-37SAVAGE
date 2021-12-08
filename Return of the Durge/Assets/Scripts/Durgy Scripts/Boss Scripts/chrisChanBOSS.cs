using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chrisChanBOSS : MonoBehaviour
{
    public GameObject[] jakeBoss;
    public void Update()
    {
        jakeBoss = GameObject.FindGameObjectsWithTag("DummyThicc");
        if (jakeBoss.Length <= 0)
        {
            this.gameObject.GetComponent<bossScript>().partnerAlive = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("healing"))
        {
            this.gameObject.GetComponent<bossScript>().health++;
        } else
        {

        }
    }
}
