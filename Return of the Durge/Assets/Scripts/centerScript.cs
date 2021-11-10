using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerScript : MonoBehaviour
{
    public gameManager gM;
    void Start()
    {
        gM = GameObject.FindObjectOfType<gameManager>();
        gM.roomCenter = this.gameObject;
        gM.neighborRoomCheck();
    }
}
