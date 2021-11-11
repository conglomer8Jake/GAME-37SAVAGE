using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerScript : MonoBehaviour
{
    public gameManager gM;
    public int gridPosY;
    public int gridPosX;
    void Start()
    {
        gM = GameObject.FindObjectOfType<gameManager>();
        gM.roomCenter = this.gameObject;
        gM.neighborRoomCheck();
        gridPosX = gM.playerPosX;
        gridPosY = gM.playerPosY;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gridPositionCheck();
        }
    }
    public void gridPositionCheck()
    {
        gM.playerPosX = gridPosX;
        gM.playerPosY = gridPosY;
    }
}
