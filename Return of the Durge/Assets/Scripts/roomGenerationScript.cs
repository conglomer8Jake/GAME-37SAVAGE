using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomGenerationScript : MonoBehaviour
{
    public Global vH;
    public listOfRooms lOR;
    public GameObject roomCenter;
    public int[] numDoorsRemaining;
    public int randNum;
    public float displacementY;
    public float displacementX;
    public string direction;
    void Start()
    {
        roomCenter = GameObject.FindGameObjectWithTag("center");
        displacementY = this.gameObject.transform.position.y - roomCenter.transform.position.y;
        displacementX = this.gameObject.transform.position.x - roomCenter.transform.position.x;
        if (displacementX > 0)
        {
            direction = "right";
        }
        if (displacementX < 0)
        {
            direction = "left";
        }
        if (displacementY > 0)
        {
            direction = "up";
        }
        if (displacementY < 0)
        {
            direction = "down";
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            randNum = Random.Range(0, 3);
            Instantiate(lOR.rooms[randNum], this.gameObject.transform.position, Quaternion.identity);
            vH.numRooms++;
        }
    }
}
