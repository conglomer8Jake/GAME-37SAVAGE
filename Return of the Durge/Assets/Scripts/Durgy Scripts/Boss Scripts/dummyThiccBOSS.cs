using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyThiccBOSS : MonoBehaviour
{
    public GameObject[] ChrisChan;
    public GameObject healingCode;
    public void Update()
    {
        ChrisChan = GameObject.FindGameObjectsWithTag("ChrisChan");
        if (ChrisChan.Length <= 0)
        {
            this.gameObject.GetComponent<FlockerScript>().reTargetPlayer();
            this.gameObject.GetComponent<bossScript>().partnerAlive = false;
        }
    }
    public void healChris()
    {
        Instantiate(healingCode, this.gameObject.transform.position, Quaternion.identity);
        this.gameObject.GetComponent<FlockerScript>().reTargetPlayer();
    }
}
