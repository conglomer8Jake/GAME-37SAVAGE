using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomGenerationScript : MonoBehaviour
{
    public gameManager gM;
    public Global vH;
    public listOfRooms lOR;
    public GameObject roomCenter;
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
        gM = GameObject.FindObjectOfType<gameManager>();
        roomCenter = GameObject.FindGameObjectWithTag("center");
        displacementY = this.gameObject.transform.position.y - roomCenter.transform.position.y;
        displacementX = this.gameObject.transform.position.x - roomCenter.transform.position.x;
        if (displacementX != 0)
        {
            if (displacementX > 0)
            {
                direction = "right";
                verticalDiff = 0.0f;
                horizontalDiff = 34.0569f;
            }
            if (displacementX < 0)
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
                verticalDiff = -27.27f;
                horizontalDiff = 0.0f;
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            randNum = Random.Range(10, 10);
            Instantiate(lOR.rooms[randNum], new Vector2(this.gameObject.transform.position.x+horizontalDiff, this.gameObject.transform.position.y+verticalDiff), Quaternion.identity);
            if (direction == "right")
            {
                Debug.Log("right");
                gM.updateMap();
            }
            
            vH.numRooms++;
            Destroy(this.gameObject);
        }
    }
}