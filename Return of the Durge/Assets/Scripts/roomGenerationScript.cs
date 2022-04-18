using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomGenerationScript : MonoBehaviour
{
    public playerMovementHandler pMH;
    public gameManager gM;
    public Global vH;
    public listOfRooms lOR;
    
    public GameObject roomGenerated;
    public int[] numDoorsRemaining;
    public int randNum;
    public float displacementY;
    public float displacementX;
    public float verticalDiff;
    public float horizontalDiff;
    public string direction;
    void Start()
    {
        pMH = GameObject.FindObjectOfType<playerMovementHandler>();
        gM = GameObject.FindObjectOfType<gameManager>();
        displacementY = this.gameObject.transform.position.y - gM.roomCenter.transform.position.y;
        displacementX = this.gameObject.transform.position.x - gM.roomCenter.transform.position.x;
        if (displacementX != 0)
        {
            if (displacementX > 0 && displacementX > displacementY)
            {
                direction = "right";
                verticalDiff = 0.0f;
                horizontalDiff = 34.0569f;
            }
            if (displacementX < 0 && displacementX < displacementY)
            {
                direction = "left";
                verticalDiff = 0.0f;
                horizontalDiff = -32.944f;
            }
        }
        if (displacementY != 0)
        {
            if (displacementY > 0)
            {
                direction = "up";
                verticalDiff = 28.27f;
                horizontalDiff = 0.0f;
            }
            if (displacementY < 0)
            {
                direction = "down";
                verticalDiff = -30.73f;
                horizontalDiff = 0.0f;
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !gM.roomMadeRecent && gM.enemiesAlive == 0)
        {
            Debug.Log("collided");
            if (direction == "right")
            {
                gM.directionGenerated = "right";
                //gM.updateMap();
            }
            if (direction == "left")
            {
                gM.directionGenerated = "left";
                //gM.updateMap();
            }
            if (direction == "up")
            {
                gM.directionGenerated = "up";
                //gM.updateMap();
            }
            if (direction == "down")
            {
                gM.directionGenerated = "down";
            }
            gM.neighborRoomCheck();
            randNum = Random.Range(0, lOR.potentialRooms.Count);
            Instantiate(lOR.potentialRooms[randNum], new Vector2(this.gameObject.transform.position.x + horizontalDiff, this.gameObject.transform.position.y + verticalDiff), Quaternion.identity);
            roomGenerated = lOR.potentialRooms[randNum];
            gM.roomCode = lOR.potentialRooms[randNum].tag;
            vH.numRooms++;
            gM.updateMap();
            gM.roomMadeRecent = true;
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Player") && gM.roomMadeRecent)
        {
            pMH.recentColl = direction;
            gM.numDoorsActive--;
            Destroy(this.gameObject);
        }
    }
}