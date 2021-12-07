using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyThiccBOSS : MonoBehaviour
{
    public GameObject healingCode;
    public void healChris()
    {
        Instantiate(healingCode, this.gameObject.transform.position, Quaternion.identity);
        this.gameObject.GetComponent<FlockerScript>().reTargetPlayer();
    }
}
