using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerScript : MonoBehaviour
{
    public GameObject topRight, topLeft, botRight, botLeft;

    public listOfEnemies lOE;
    public Global vH;
    public gameManager gM;
    public int gridPosY;
    public int gridPosX;
    void Start()
    {
        gM = GameObject.FindObjectOfType<gameManager>();
        gM.roomCenter = this.gameObject;
        gM.neighborRoomCheck();
        spawnEnemies();
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
    public void spawnEnemies()
    {
        int randNum = Random.Range(1, 6) * vH.level;
        for (int i = 0; i < randNum; i++)
        {
            int randEnemy = Random.Range(0, lOE.enemies.Count);
            float randY = Random.Range(botRight.transform.position.y, topRight.transform.position.y);
            float randX = Random.Range(topLeft.transform.position.x, topRight.transform.position.x);
            Instantiate(lOE.enemies[randEnemy], new Vector3(randX, randY,0), Quaternion.identity);
            gM.enemiesAlive++;
        }
    }
}
