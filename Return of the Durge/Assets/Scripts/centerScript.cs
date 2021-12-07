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
    public GameObject[] numDoorsW, numDoorsE, numDoorsN, numDoorsS;
    void Start()
    {
        gM = GameObject.FindObjectOfType<gameManager>();
        bossSpawnChance = 0.00000001f*(10.0f*Mathf.Exp(vH.numRooms));
        /*
        topRight = GameObject.FindGameObjectWithTag("anchorTop");
        topLeft = GameObject.FindGameObjectWithTag("anchorLeft");
        botRight = GameObject.FindGameObjectWithTag("anchorBot");
        */
        gM.roomCenter = this.gameObject;
        gM.neighborRoomCheck();
        bossSpawnCheck();
        gridPosX = gM.playerPosX;
        gridPosY = gM.playerPosY;
    }
    public void Update()
    {
        doorCheck();
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
        if (bossSpawnChance >= 1.0f || gM.numDoorsActive == 0)
        {
            Debug.Log("BOSS INCOMING");
            int rand = Random.Range(0, 10);
            if (rand >= 5 && vH.bossNumGen != 0)
            {
                float randY = Random.Range(botRight.transform.position.y, topRight.transform.position.y);
                float randX = Random.Range(topLeft.transform.position.x, topRight.transform.position.x);
                Instantiate(E, new Vector3(randX, randY, 0), Quaternion.identity);
                float randY2 = Random.Range(botRight.transform.position.y, topRight.transform.position.y);
                float randX2 = Random.Range(topLeft.transform.position.x, topRight.transform.position.x);
                Instantiate(Ryan, new Vector3(randX2, randY2, 0), Quaternion.identity);
                vH.bossNumGen = 0;
            }
            if (rand < 5 && vH.bossNumGen != 1)
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
        gM.roomCenter = this.gameObject;
        gM.DebugOutRoomArray();
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
        /*
        Destroy(topRight);
        Destroy(topLeft);
        Destroy(botRight);
        */
    }
    public void doorCheck()
    {
        numDoorsE = GameObject.FindGameObjectsWithTag("east");
        numDoorsW = GameObject.FindGameObjectsWithTag("west");
        numDoorsN = GameObject.FindGameObjectsWithTag("north");
        numDoorsS = GameObject.FindGameObjectsWithTag("south");
        gM.numDoorsActive = numDoorsE.Length + numDoorsN.Length + numDoorsS.Length + numDoorsW.Length;
    }
}
