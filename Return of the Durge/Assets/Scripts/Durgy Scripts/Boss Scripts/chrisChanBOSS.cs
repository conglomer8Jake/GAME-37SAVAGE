using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chrisChanBOSS : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D other)
    {   /*
        if (other.gameObject.CompareTag("placeHolder"))
        {
            this.gameObject.GetComponent<FlockerScript>().FlockingTarget = this.gameObject;
            Destroy(other.gameObject);
            this.gameObject.GetComponent<bossScript>().callReTarget();
        }
        */
        if (other.gameObject.CompareTag("healing"))
        {
            this.gameObject.GetComponent<bossScript>().health++;
        } else
        {

        }
    }
}
