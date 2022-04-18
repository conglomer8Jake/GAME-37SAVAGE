using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerScript : MonoBehaviour
{
    public GameObject ChrisChan, DummyThicc, E, Ryan, Grant;
    public GameObject topRight, topLeft, botRight;
    public GameObject soundObj;

    public listOfEnemies lOE;
    public Global vH;
    public gameManager gM;
    public int gridPosY;
    public int gridPosX;
    public float bossSpawnChance;
    public bool bossInstaSpawn = false;
    public GameObject[] numDoorsW, numDoorsE, numDoorsN, numDoorsS;
    public AudioClip chrisJakeTheme, elijahRyanTheme, finalTheme;
    void Start()
    {
        soundObj = GameObject.FindGameObjectWithTag("soundObj");
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
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gridPositionCheck();
        }
    }
    public void bossSpawnCheck()
    {
        if (vH.numRooms >= 4)
        {
            if (gM.currentScene == "LVL_Three")
            {
                Instantiate(Grant, new Vector3(gM.roomCenter.transform.position.x, gM.roomCenter.transform.position.y, 0), Quaternion.identity);
                soundObj.GetComponent<AudioSource>().clip = finalTheme;
                soundObj.GetComponent<AudioSource>().Play();
            }
            int Rand = Random.Range(0, 10);
            if (gM.currentScene == "SampleScene")
            {
                Instantiate(E, new Vector3(gM.roomCenter.transform.position.x, gM.roomCenter.transform.position.y, 0), Quaternion.identity);
                Instantiate(Ryan, new Vector3(gM.roomCenter.transform.position.x, gM.roomCenter.transform.position.y, 0), Quaternion.identity);
                soundObj.GetComponent<AudioSource>().clip = elijahRyanTheme;
                soundObj.GetComponent<AudioSource>().Play();
                vH.bossNumGen = 0;
            }
            if (gM.currentScene == "LVL_Forest")
            {
                Instantiate(ChrisChan, new Vector3(gM.roomCenter.transform.position.x, gM.roomCenter.transform.position.y, 0), Quaternion.identity);
                Instantiate(DummyThicc, new Vector3(gM.roomCenter.transform.position.x, gM.roomCenter.transform.position.y, 0), Quaternion.identity);
                soundObj.GetComponent<AudioSource>().clip = chrisJakeTheme;
                soundObj.GetComponent<AudioSource>().Play();
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
            soundObj.GetComponent<AudioSource>().clip = finalTheme;
            soundObj.GetComponent<AudioSource>().Play();
        }
        int Rand = Random.Range(0, 10);
        //if (Rand >= 5 && vH.bossNumGen != 0 && gM.currentScene != "LVL_Three")
        if (gM.currentScene == "SampleScene")
        {
            Instantiate(E, new Vector3(gM.roomCenter.transform.position.x, gM.roomCenter.transform.position.y, 0), Quaternion.identity);
            Instantiate(Ryan, new Vector3(gM.roomCenter.transform.position.x, gM.roomCenter.transform.position.y, 0), Quaternion.identity);
            soundObj.GetComponent<AudioSource>().clip = elijahRyanTheme;
            soundObj.GetComponent<AudioSource>().Play();
            vH.bossNumGen = 0;
        }
        //if (Rand < 5 && vH.bossNumGen != 1 && gM.currentScene != "LVL_Three")
        if (gM.currentScene == "LVL_Forest")
        {
            Instantiate(ChrisChan, new Vector3(gM.roomCenter.transform.position.x, gM.roomCenter.transform.position.y, 0), Quaternion.identity);
            Instantiate(DummyThicc, new Vector3(gM.roomCenter.transform.position.x, gM.roomCenter.transform.position.y, 0), Quaternion.identity);
            soundObj.GetComponent<AudioSource>().clip = chrisJakeTheme;
            soundObj.GetComponent<AudioSource>().Play();
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
