using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerScript : MonoBehaviour
{
    public GameObject ChrisChan, DummyThicc, E, Ryan;
    public GameObject topRight, topLeft, botRight;

    public listOfEnemies lOE;
    public Global vH;
    public gameManager gM;
    public int gridPosY;
    public int gridPosX;
    public float bossSpawnChance;
    void Start()
    {
        bossSpawnChance = 0.00000001f*(10.0f*Mathf.Exp(vH.numRooms));
        topRight = GameObject.FindGameObjectWithTag("anchorTop");
        topLeft = GameObject.FindGameObjectWithTag("anchorLeft");
        botRight = GameObject.FindGameObjectWithTag("anchorBot");
        gM = GameObject.FindObjectOfType<gameManager>();
        gM.roomCenter = this.gameObject;
        gM.neighborRoomCheck();
        bossSpawnCheck();
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
    public void bossSpawnCheck()
    {
        if (bossSpawnChance >= 1.0f)
        {
            int rand = Random.Range(0, 1);
            if (rand == 0 && vH.bossNumGen != 0)
            {
                float randY = Random.Range(botRight.transform.position.y, topRight.transform.position.y);
                float randX = Random.Range(topLeft.transform.position.x, topRight.transform.position.x);
                Instantiate(E, new Vector3(randX, randY, 0), Quaternion.identity);
                float randY2 = Random.Range(botRight.transform.position.y, topRight.transform.position.y);
                float randX2 = Random.Range(topLeft.transform.position.x, topRight.transform.position.x);
                Instantiate(Ryan, new Vector3(randX2, randY2, 0), Quaternion.identity);
                vH.bossNumGen = 0;
            }
            if (rand == 1 && vH.bossNumGen != 1)
            {
                float randY = Random.Range(botRight.transform.position.y, topRight.transform.position.y);
                float randX = Random.Range(topLeft.transform.position.x, topRight.transform.position.x);
                Instantiate(ChrisChan, new Vector3(randX, randY, 0), Quaternion.identity);
                float randY2 = Random.Range(botRight.transform.position.y, topRight.transform.position.y);
                float randX2 = Random.Range(topLeft.transform.position.x, topRight.transform.position.x);
                Instantiate(DummyThicc, new Vector3(randX2, randY2, 0), Quaternion.identity);
            }
            vH.bossNumGen = 1;
        }
        else
        {
            Invoke("spawnEnemies", 10.0f);
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
