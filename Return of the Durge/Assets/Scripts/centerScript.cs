using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerScript : MonoBehaviour
{
    public GameObject ChrisChan, DummyThicc, E, Ryan, Grant;
    public GameObject topRight, topLeft, botRight;

    public listOfEnemies lOE;
    public Global vH;
    public gameManager gM;
    public int gridPosY;
    public int gridPosX;
    public float bossSpawnChance;
    public bool bossInstaSpawn = false;
    public GameObject[] numDoorsW, numDoorsE, numDoorsN, numDoorsS;
    void Start()
    {
        gM = GameObject.FindObjectOfType<gameManager>();
        bossSpawnChance = 0.000001f*(10.0f*Mathf.Exp(vH.numRooms)*vH.level);
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
        if (Input.GetKeyDown(KeyCode.Y))
        {
            bossSpawnInsta();
        }
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
        if (bossSpawnChance >= 1.0f || gM.numDoorsActive == 0 || bossInstaSpawn)
        {
            Debug.Log("BOSS INCOMING");
            if (gM.currentScene == "LVL_Three")
            {
                float randY = Random.Range(botRight.transform.position.y, topRight.transform.position.y);
                float randX = Random.Range(topLeft.transform.position.x, topRight.transform.position.x);
                Instantiate(Grant, new Vector3(randX, randY, 0), Quaternion.identity);
            }
            if (vH.bossNumGen != 0 && gM.currentScene != "LVL_Three")
            {
                float randY = Random.Range(botRight.transform.position.y, topRight.transform.position.y);
                float randX = Random.Range(topLeft.transform.position.x, topRight.transform.position.x);
                Instantiate(E, new Vector3(randX, randY, 0), Quaternion.identity);
                float randY2 = Random.Range(botRight.transform.position.y, topRight.transform.position.y);
                float randX2 = Random.Range(topLeft.transform.position.x, topRight.transform.position.x);
                Instantiate(Ryan, new Vector3(randX2, randY2, 0), Quaternion.identity);
                vH.bossNumGen = 0;
            }
            if (vH.bossNumGen != 1 && gM.currentScene != "LVL_Three")
            {
                float randY = Random.Range(botRight.transform.position.y, topRight.transform.position.y);
                float randX = Random.Range(topLeft.transform.position.x, topRight.transform.position.x);
                Instantiate(ChrisChan, new Vector3(randX, randY, 0), Quaternion.identity);
                float randY2 = Random.Range(botRight.transform.position.y, topRight.transform.position.y);
                float randX2 = Random.Range(topLeft.transform.position.x, topRight.transform.position.x);
                Instantiate(DummyThicc, new Vector3(randX2, randY2, 0), Quaternion.identity);
                vH.bossNumGen = 1;
            }
        }
        else
        {
            Invoke("spawnEnemies", 5.0f);
        }
    }
    public void bossSpawnInsta()
    {
        if (gM.currentScene == "LVL_Three")
        {
            Instantiate(Grant, new Vector3(gM.roomCenter.transform.position.x, gM.roomCenter.transform.position.y, 0), Quaternion.identity);
        }
        int Rand = Random.Range(0, 10);
        if (Rand >= 5 && vH.bossNumGen != 0 && gM.currentScene != "LVL_Three")
        {
            Instantiate(E, new Vector3(gM.roomCenter.transform.position.x, gM.roomCenter.transform.position.y, 0), Quaternion.identity);
            Instantiate(Ryan, new Vector3(gM.roomCenter.transform.position.x, gM.roomCenter.transform.position.y, 0), Quaternion.identity);
            vH.bossNumGen = 0;
        }
        if (Rand < 5 && vH.bossNumGen != 1 && gM.currentScene != "LVL_Three")
        {
            Instantiate(ChrisChan, new Vector3(gM.roomCenter.transform.position.x, gM.roomCenter.transform.position.y, 0), Quaternion.identity);
            Instantiate(DummyThicc, new Vector3(gM.roomCenter.transform.position.x, gM.roomCenter.transform.position.y, 0), Quaternion.identity);
            vH.bossNumGen = 1;
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
