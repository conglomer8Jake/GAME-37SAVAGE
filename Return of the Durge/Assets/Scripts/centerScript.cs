using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerScript : MonoBehaviour
{
    public GameObject stair;
    public GameObject topRight, topLeft, botRight;

    public listOfEnemies lOE;
    public Global vH;
    public gameManager gM;
    public int gridPosY;
    public int gridPosX;
    public float stairSpawnChance;
    void Start()
    {
        stairSpawnChance = 0.00000001f*(10.0f*Mathf.Exp(vH.numRooms));
        topRight = GameObject.FindGameObjectWithTag("anchorTop");
        topLeft = GameObject.FindGameObjectWithTag("anchorLeft");
        botRight = GameObject.FindGameObjectWithTag("anchorBot");
        gM = GameObject.FindObjectOfType<gameManager>();
        gM.roomCenter = this.gameObject;
        gM.neighborRoomCheck();
        stairSpawnCheck();
        Invoke("spawnEnemies", 10.0f);
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
    public void stairSpawnCheck()
    {
        if (stairSpawnChance >= 1.0f)
        {
            float randY = Random.Range(botRight.transform.position.y, topRight.transform.position.y);
            float randX = Random.Range(topLeft.transform.position.x, topRight.transform.position.x);
            Instantiate(stair, new Vector3(randX, randY, 0), Quaternion.identity);
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
            if (lOE.enemies[randEnemy] != null)
            {
                Instantiate(lOE.enemies[randEnemy], new Vector3(randX, randY, 0), Quaternion.identity);
                gM.enemiesAlive++;
            }
        }
        Destroy(topRight);
        Destroy(topLeft);
        Destroy(botRight);
    }
}
