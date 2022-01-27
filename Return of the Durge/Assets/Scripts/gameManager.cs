using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class gameManager : MonoBehaviour
{
    public Global vH;
    public listOfRooms lOR;
    public GameObject roomCenter;
    public GameObject player;
    public GameObject[] numEnemies;

    public const int COLUMN_COUNT = 7;
    public const int ROW_COUNT = 7;

    public string[,,] roomMap = new string[ROW_COUNT, COLUMN_COUNT, 2];
    //plane 0 = room information
    //plane 1 = player position

    public string directionGenerated;
    public string roomCode;
    public string playerPos;
    public string currentScene = "";

    public int worldState = 1;
    public int vertical, horizontal;
    public int rowsGenerated;
    public int colsGenerated;
    public int playerPosX = 3;
    public int playerPosY = 3;
    public int enemiesAlive = 0;
    public int numDoorsActive;

    public float roomGenCooldown = 5.0f;

    public bool roomMadeRecent = false;
    public bool topRes;
    public bool leftRes;
    public bool downRes;
    public bool rightRes;

    public bool topNull, rightNull, botNull, leftNull;
    void Start()
    {
        Instantiate(player, new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);
        roomCenter = GameObject.FindGameObjectWithTag("center");
        for (int row = 0; row < ROW_COUNT; row++)
        {
            for (int col = 0; col < COLUMN_COUNT; col++)
            {
                roomMap[row, col, 0] = "F";
                roomMap[row, col, 1] = " ";
            }
        }

        int centerRow = ROW_COUNT / 2;
        int centerColumn = COLUMN_COUNT / 2;
        roomMap[centerRow, centerColumn, 1] = "P";
        roomMap[centerRow, centerColumn, 0] = "R4";
        DebugOutRoomArray();
        vH.restart();
    }
    public void updateMap()
    {
        for (int row = 0; row < ROW_COUNT; row++)
        {
            for (int col = 0; col < COLUMN_COUNT; col++)
            {
                if (roomMap[row,col,1].Contains("P"))
                {
                    playerPosX = col;
                    playerPosY = row;
                }
            }
        }
        if (directionGenerated == "right")
        {
            roomMap[playerPosY, playerPosX, 1] = "p";
            roomMap[playerPosY, playerPosX + 1, 0] = (string)roomCode;
            roomMap[playerPosY, playerPosX + 1, 1] = "P";
            playerPosX += 1;
        }
        if (directionGenerated == "up")
        {
            roomMap[playerPosY, playerPosX, 1] = "p";
            roomMap[playerPosY - 1, playerPosX, 0] = (string)roomCode;
            roomMap[playerPosY - 1, playerPosX, 1] = "P";
            playerPosY -= 1;
        }
        if (directionGenerated == "left")
        {
            roomMap[playerPosY, playerPosX, 1] = "p";
            roomMap[playerPosY, playerPosX - 1, 0] = (string)roomCode;
            roomMap[playerPosY, playerPosX - 1, 1] = "P";
            playerPosX -= 1;
        }
        if (directionGenerated == "down")
        {
            roomMap[playerPosY, playerPosX, 1] = "p";
            roomMap[playerPosY + 1, playerPosX, 0] = (string)roomCode;
            roomMap[playerPosY + 1, playerPosX, 1] = "P";
            playerPosY += 1;
        }
        DebugOutRoomArray();
    }
    public void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;
        /*
        numEnemies = GameObject.FindGameObjectsWithTag("enemy");
        enemiesAlive = numEnemies.Length;
        */
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Pause");
            worldState *= -1;
            worldStateChecker();
        }
        if (roomMadeRecent)
        {
            roomGenCooldown -= 1 * Time.deltaTime;
        }
        if (roomGenCooldown <= 0)
        {
            roomMadeRecent = false;
            roomGenCooldown = 5.0f;
            lOR.potentialRooms.Clear();
        }
    }
    public void DebugOutRoomArray()
    {
        string outputString = "Map\n";
        for (int row = 0; row < ROW_COUNT; row++)
        {
            for (int col = 0; col < COLUMN_COUNT; col++)
            {
                outputString += roomMap[row, col, 0];
                outputString += " ";
            }
            outputString += "\n";
        }
        Debug.Log(outputString);

        outputString = "Player\n";
        for (int row = 0; row < ROW_COUNT; row++)
        {
            for (int col = 0; col < COLUMN_COUNT; col++)
            {
                outputString += roomMap[row, col, 1];
                outputString += " ";
            }
            outputString += "\n";
        }
        Debug.Log(outputString);
    }
    public void neighborRoomCheck()
    {
        lOR.potentialRooms.Clear();
        topNull = false;
        botNull = false;
        rightNull = false;
        leftNull = false;
        //checking room's neighbors 
        if (directionGenerated == "right")
            {
            //check up, right, down
            {
                if (playerPosY == 1)
                {
                    topNull = true;
                }
                else
                {
                    topNull = false;
                    if (roomMap[playerPosY - 1, playerPosX, 0].Contains("F") || roomMap[playerPosY - 1, playerPosX, 0] == "R4" && !topNull)
                    {
                        topRes = false;
                    }
                    else if (roomMap[playerPosY - 1, playerPosX, 0].Contains("R3ULR") || roomMap[playerPosY - 1, playerPosX, 0].Contains("R2UL") || roomMap[playerPosY - 1, playerPosX, 0].Contains("R2UR") || roomMap[playerPosY - 1, playerPosX, 0].Contains("R2LR") || !roomMap[playerPosY - 1, playerPosX, 0].Contains("R1D") && !topNull)
                    {
                        topRes = true;
                    }
                }        
                if (playerPosX == 5)
                {
                    rightNull = true;
                }
                else
                {
                    rightNull = false;
                    if (roomMap[playerPosY, playerPosX + 1, 0].Contains("F") || roomMap[playerPosY, playerPosX + 1, 0] == "R4")
                    {
                        rightRes = false;
                    }
                    else if (roomMap[playerPosY, playerPosX + 1, 0].Contains("R3URD") || roomMap[playerPosY, playerPosX + 1, 0].Contains("R2UD") || roomMap[playerPosY, playerPosX + 1, 0].Contains("R2UR") || roomMap[playerPosY, playerPosX + 1, 0].Contains("R2DR") || !roomMap[playerPosY, playerPosX + 1, 0].Contains("R1L") && !rightNull)
                    {
                        rightRes = true;
                    }
                }
                if (playerPosY == 5)
                {
                    botNull = true;
                }
                else
                {
                    botNull = false;
                    if (roomMap[playerPosY + 1, playerPosX, 0].Contains("F") || roomMap[playerPosY + 1, playerPosX, 0] == "R4")
                    {
                        downRes = false;
                    }
                    else if (roomMap[playerPosY + 1, playerPosX, 0].Contains("R3DLR") || roomMap[playerPosY + 1, playerPosX, 0].Contains("R2DL") || roomMap[playerPosY + 1, playerPosX, 0].Contains("R2DR") || roomMap[playerPosY + 1, playerPosX, 0].Contains("R2LR") || !roomMap[playerPosY + 1, playerPosX, 0].Contains("R1U") && !botNull)
                    {
                        downRes = true;
                    }
                }
            }
            //Add rooms to the restricted room list 
            {
                if (playerPosX == 5)
                {
                    lOR.potentialRooms.Add(lOR.R1L);
                    lOR.potentialRooms.Add(lOR.R2UL);
                    lOR.potentialRooms.Add(lOR.R2DL);
                    lOR.potentialRooms.Add(lOR.R3ULD);
                    return;
                }
                if (topRes && rightRes && downRes)
                {
                    lOR.potentialRooms.Add(lOR.R1L);
                }
                if (!topRes && !rightRes && !downRes)
                {
                    lOR.potentialRooms.Add(lOR.R1L);
                    lOR.potentialRooms.Add(lOR.R2DL);
                    lOR.potentialRooms.Add(lOR.R2LR);
                    lOR.potentialRooms.Add(lOR.R2UL);
                    lOR.potentialRooms.Add(lOR.R3ULR);
                    lOR.potentialRooms.Add(lOR.R3ULD);
                    lOR.potentialRooms.Add(lOR.R3DLR);
                    lOR.potentialRooms.Add(lOR.R4);
                }
                if (topRes && !rightRes && !downRes)
                {
                    lOR.potentialRooms.Add(lOR.R1L);
                    lOR.potentialRooms.Add(lOR.R2LR);
                    lOR.potentialRooms.Add(lOR.R2UL);
                    lOR.potentialRooms.Add(lOR.R3DLR);
                }
                if (topRes && rightRes && !downRes)
                {
                    lOR.potentialRooms.Add(lOR.R1L);
                    lOR.potentialRooms.Add(lOR.R2DL);

                }
                if (topRes && !rightRes && downRes)
                {
                    lOR.potentialRooms.Add(lOR.R1L);
                    lOR.potentialRooms.Add(lOR.R2LR);
                }
                if (!topRes && rightRes && downRes)
                {
                    lOR.potentialRooms.Add(lOR.R1L);
                    lOR.potentialRooms.Add(lOR.R2UL);
                }
                if (!topRes && rightRes && !downRes)
                {
                    lOR.potentialRooms.Add(lOR.R1L);
                    lOR.potentialRooms.Add(lOR.R2UL);
                    lOR.potentialRooms.Add(lOR.R2DL);
                    lOR.potentialRooms.Add(lOR.R3ULD);

                }
                if (!topRes && !rightRes && downRes)
                {
                    lOR.potentialRooms.Add(lOR.R1L);
                    lOR.potentialRooms.Add(lOR.R2UL);
                    lOR.potentialRooms.Add(lOR.R2LR);
                    lOR.potentialRooms.Add(lOR.R3ULR);
                }
            }
        }
        if (directionGenerated == "up")
        {
            //check, up, right, left
            {
                if (playerPosY == 1)
                {
                    topNull = true;
                }
                else
                {
                    topNull = false;
                    if (roomMap[playerPosY - 1, playerPosX, 0].Contains("F") || roomMap[playerPosY - 1, playerPosX, 0] == "R4" && !topNull)
                    {
                        topRes = false;
                    }
                    else if (roomMap[playerPosY - 1, playerPosX, 0].Contains("R3ULR") || roomMap[playerPosY - 1, playerPosX, 0].Contains("R2UL") || roomMap[playerPosY - 1, playerPosX, 0].Contains("R2UR") || roomMap[playerPosY - 1, playerPosX, 0].Contains("R2LR") || !roomMap[playerPosY - 1, playerPosX, 0].Contains("R1D") && !topNull)
                    {
                        topRes = true;
                    }
                }
                if (playerPosX == 5)
                {
                    rightNull = true;
                }
                else
                {
                    rightNull = false;
                    if (roomMap[playerPosY, playerPosX + 1, 0].Contains("F") || roomMap[playerPosY, playerPosX + 1, 0] == "R4")
                    {
                        rightRes = false;
                    }
                    else if (roomMap[playerPosY, playerPosX + 1, 0].Contains("R3URD") || roomMap[playerPosY, playerPosX + 1, 0].Contains("R2UD") || roomMap[playerPosY, playerPosX + 1, 0].Contains("R2UR") || roomMap[playerPosY, playerPosX + 1, 0].Contains("R2DR") || !roomMap[playerPosY, playerPosX + 1, 0].Contains("R1L") && !rightNull)
                    {
                        rightRes = true;
                    }
                }
                if (playerPosX == 1)
                {
                    leftNull = true;
                }
                else
                {
                    leftNull = false;
                    if (roomMap[playerPosY, playerPosX - 1, 0] == "F" || roomMap[playerPosY, playerPosX - 1, 0] == "R4")
                    {
                        leftRes = false;
                    }
                    else if (roomMap[playerPosY, playerPosX - 1, 0].Contains("R3ULD") || roomMap[playerPosY, playerPosX - 1, 0].Contains("R2UD") || roomMap[playerPosY, playerPosX - 1, 0].Contains("R2UL") || roomMap[playerPosY, playerPosX - 1, 0].Contains("R2DL") || !roomMap[playerPosY, playerPosX - 1, 0].Contains("R1R") && !leftNull)
                    {
                        leftRes = true;
                    }
                }
            }
            //Add rooms to the restricted room list
            {
                if (playerPosY == 1)
                {
                    lOR.potentialRooms.Add(lOR.R1D);
                    lOR.potentialRooms.Add(lOR.R2DL);
                    lOR.potentialRooms.Add(lOR.R2DR);
                    lOR.potentialRooms.Add(lOR.R3DLR);
                    return;
                }
                if (topRes && rightRes && leftRes)
                {
                    lOR.potentialRooms.Add(lOR.R1D);
                }
                if (!topRes && !rightRes && !leftRes)
                {
                    lOR.potentialRooms.Add(lOR.R1D);
                    lOR.potentialRooms.Add(lOR.R2DL);
                    lOR.potentialRooms.Add(lOR.R2DR);
                    lOR.potentialRooms.Add(lOR.R2UD);
                    lOR.potentialRooms.Add(lOR.R3DLR);
                    lOR.potentialRooms.Add(lOR.R3ULD);
                    lOR.potentialRooms.Add(lOR.R3URD);
                    lOR.potentialRooms.Add(lOR.R4);
                }
                if (topRes && !rightRes && !leftRes)
                {
                    lOR.potentialRooms.Add(lOR.R1D);
                    lOR.potentialRooms.Add(lOR.R2DL);
                    lOR.potentialRooms.Add(lOR.R2DR);
                    lOR.potentialRooms.Add(lOR.R3DLR);
                }
                if (topRes && rightRes && !leftRes)
                {
                    lOR.potentialRooms.Add(lOR.R1D);
                    lOR.potentialRooms.Add(lOR.R2DL);
                }
                if (topRes && !rightRes && leftRes)
                {
                    lOR.potentialRooms.Add(lOR.R1D);
                    lOR.potentialRooms.Add(lOR.R2DR);
                }
                if (!topRes && rightRes && leftRes)
                {
                    lOR.potentialRooms.Add(lOR.R1D);
                    lOR.potentialRooms.Add(lOR.R2UD);
                }
                if (!topRes && rightRes && !leftRes)
                {
                    lOR.potentialRooms.Add(lOR.R1D);
                    lOR.potentialRooms.Add(lOR.R2DL);
                    lOR.potentialRooms.Add(lOR.R2UD);
                    lOR.potentialRooms.Add(lOR.R3ULD);
                }
                if (!topRes && !rightRes && leftRes)
                {
                    lOR.potentialRooms.Add(lOR.R1D);
                    lOR.potentialRooms.Add(lOR.R2DR);
                    lOR.potentialRooms.Add(lOR.R2UD);
                    lOR.potentialRooms.Add(lOR.R3URD);
                }
            }
        }
        if (directionGenerated == "left")
        {
            //check up, left, down
            {
                if (playerPosY == 1)
                {
                    topNull = true;
                }
                else
                {
                    topNull = false;
                    if (roomMap[playerPosY - 1, playerPosX, 0].Contains("F") || roomMap[playerPosY - 1, playerPosX, 0] == "R4" && !topNull)
                    {
                        topRes = false;
                    }
                    else if (roomMap[playerPosY - 1, playerPosX, 0].Contains("R3ULR") || roomMap[playerPosY - 1, playerPosX, 0].Contains("R2UL") || roomMap[playerPosY - 1, playerPosX, 0].Contains("R2UR") || roomMap[playerPosY - 1, playerPosX, 0].Contains("R2LR") || !roomMap[playerPosY - 1, playerPosX, 0].Contains("R1D") && !topNull)
                    {
                        topRes = true;
                    }
                }
                if (playerPosX == 1)
                {
                    leftNull = true;
                }
                else
                {
                    leftNull = false;
                    if (roomMap[playerPosY, playerPosX - 1, 0] == "F" || roomMap[playerPosY, playerPosX - 1, 0] == "R4")
                    {
                        leftRes = false;
                    }
                    else if (roomMap[playerPosY, playerPosX - 1, 0].Contains("R3ULD") || roomMap[playerPosY, playerPosX - 1, 0].Contains("R2UD") || roomMap[playerPosY, playerPosX - 1, 0].Contains("R2UL") || roomMap[playerPosY, playerPosX - 1, 0].Contains("R2DL") || !roomMap[playerPosY, playerPosX - 1, 0].Contains("R1R") && !leftNull)
                    {
                        leftRes = true;
                    }
                }
                if (playerPosY == 5)
                {
                    botNull = true;
                }
                else
                {
                    botNull = false;
                    if (roomMap[playerPosY + 1, playerPosX, 0].Contains("F") || roomMap[playerPosY + 1, playerPosX, 0] == "R4")
                    {
                        downRes = false;
                    }
                    else if (roomMap[playerPosY + 1, playerPosX, 0].Contains("R3DLR") || roomMap[playerPosY + 1, playerPosX, 0].Contains("R2DL") || roomMap[playerPosY + 1, playerPosX, 0].Contains("R2DR") || roomMap[playerPosY + 1, playerPosX, 0].Contains("R2LR") || !roomMap[playerPosY + 1, playerPosX, 0].Contains("R1U") && !botNull)
                    {
                        downRes = true;
                    }
                }
            }
            //Add rooms to the restricted room list
            {
                if (playerPosX == 1)
                {
                    lOR.potentialRooms.Add(lOR.R1R);
                    lOR.potentialRooms.Add(lOR.R2DR);
                    lOR.potentialRooms.Add(lOR.R2UR);
                    lOR.potentialRooms.Add(lOR.R3URD);
                    return;
                }
                if (topRes && leftRes && downRes)
                {
                    lOR.potentialRooms.Add(lOR.R1R);
                }
                if (!topRes && !leftRes && !downRes)
                {
                    lOR.potentialRooms.Add(lOR.R1R);
                    lOR.potentialRooms.Add(lOR.R2DR);
                    lOR.potentialRooms.Add(lOR.R2LR);
                    lOR.potentialRooms.Add(lOR.R2UR);
                    lOR.potentialRooms.Add(lOR.R3DLR);
                    lOR.potentialRooms.Add(lOR.R3ULR);
                    lOR.potentialRooms.Add(lOR.R3URD);
                    lOR.potentialRooms.Add(lOR.R4);
                }
                if (topRes && !leftRes && !downRes)
                {
                    lOR.potentialRooms.Add(lOR.R1R);
                    lOR.potentialRooms.Add(lOR.R2DR);
                    lOR.potentialRooms.Add(lOR.R2LR);
                    lOR.potentialRooms.Add(lOR.R3DLR);
                }
                if (topRes && leftRes && !downRes)
                {
                    lOR.potentialRooms.Add(lOR.R1R);
                    lOR.potentialRooms.Add(lOR.R2DR);
                }
                if (topRes && !leftRes && downRes)
                {
                    lOR.potentialRooms.Add(lOR.R1R);
                    lOR.potentialRooms.Add(lOR.R2LR);
                }
                if (!topRes && leftRes && downRes)
                {
                    lOR.potentialRooms.Add(lOR.R1R);
                    lOR.potentialRooms.Add(lOR.R2UR);
                }
                if (!topRes && leftRes && !downRes)
                {
                    lOR.potentialRooms.Add(lOR.R1R);
                    lOR.potentialRooms.Add(lOR.R2DR);
                    lOR.potentialRooms.Add(lOR.R2UR);
                    lOR.potentialRooms.Add(lOR.R3URD);

                }
                if (!topRes && !leftRes && downRes)
                {
                    lOR.potentialRooms.Add(lOR.R1R);
                    lOR.potentialRooms.Add(lOR.R2LR);
                    lOR.potentialRooms.Add(lOR.R2UR);
                    lOR.potentialRooms.Add(lOR.R3ULR);
                }
            }
        }
        if (directionGenerated == "down")
        {
            //check left, down, right
            {
                if (playerPosX == 1)
                {
                    leftNull = true;
                }
                else
                {
                    leftNull = false;
                    if (roomMap[playerPosY, playerPosX - 1, 0] == "F" || roomMap[playerPosY, playerPosX - 1, 0] == "R4")
                    {
                        leftRes = false;
                    }
                    else if (roomMap[playerPosY, playerPosX - 1, 0].Contains("R3ULD") || roomMap[playerPosY, playerPosX - 1, 0].Contains("R2UD") || roomMap[playerPosY, playerPosX - 1, 0].Contains("R2UL") || roomMap[playerPosY, playerPosX - 1, 0].Contains("R2DL") || !roomMap[playerPosY, playerPosX - 1, 0].Contains("R1R") && !leftNull)
                    {
                        leftRes = true;
                    }
                }
                if (playerPosY == 5)
                {
                    botNull = true;
                }
                else
                {
                    botNull = false;
                    if (roomMap[playerPosY + 1, playerPosX, 0].Contains("F") || roomMap[playerPosY + 1, playerPosX, 0] == "R4")
                    {
                        downRes = false;
                    }
                    else if (roomMap[playerPosY + 1, playerPosX, 0].Contains("R3DLR") || roomMap[playerPosY + 1, playerPosX, 0].Contains("R2DL") || roomMap[playerPosY + 1, playerPosX, 0].Contains("R2DR") || roomMap[playerPosY + 1, playerPosX, 0].Contains("R2LR") || !roomMap[playerPosY + 1, playerPosX, 0].Contains("R1U") && !botNull)
                    {
                        downRes = true;
                    }
                }
                if (playerPosX == 5)
                {
                    rightNull = true;
                }
                else
                {
                    rightNull = false;
                    if (roomMap[playerPosY, playerPosX + 1, 0].Contains("F") || roomMap[playerPosY, playerPosX + 1, 0] == "R4")
                    {
                        rightRes = false;
                    }
                    else if (roomMap[playerPosY, playerPosX + 1, 0].Contains("R3URD") || roomMap[playerPosY, playerPosX + 1, 0].Contains("R2UD") || roomMap[playerPosY, playerPosX + 1, 0].Contains("R2UR") || roomMap[playerPosY, playerPosX + 1, 0].Contains("R2DR") || !roomMap[playerPosY, playerPosX + 1, 0].Contains("R1L") && !rightNull)
                    {
                        rightRes = true;
                    }
                }
            }
            //Add rooms to the restriced room list
            {
                if (playerPosY == 5)
                {
                    lOR.potentialRooms.Add(lOR.R1U);
                    lOR.potentialRooms.Add(lOR.R2UL);
                    lOR.potentialRooms.Add(lOR.R2UR);
                    lOR.potentialRooms.Add(lOR.R3ULR);
                    return;
                }
                if (leftRes && downRes && rightRes)
                {
                    lOR.potentialRooms.Add(lOR.R1U);
                }
                if (!leftRes && !downRes && !rightRes)
                {
                    lOR.potentialRooms.Add(lOR.R1U);
                    lOR.potentialRooms.Add(lOR.R2UD);
                    lOR.potentialRooms.Add(lOR.R2UL);
                    lOR.potentialRooms.Add(lOR.R2UR);
                    lOR.potentialRooms.Add(lOR.R3ULD);
                    lOR.potentialRooms.Add(lOR.R3ULR);
                    lOR.potentialRooms.Add(lOR.R3URD);
                    lOR.potentialRooms.Add(lOR.R4);
                }
                if (leftRes && !downRes && !rightRes)
                {
                    lOR.potentialRooms.Add(lOR.R1U);
                    lOR.potentialRooms.Add(lOR.R2UD);
                    lOR.potentialRooms.Add(lOR.R2UR);
                    lOR.potentialRooms.Add(lOR.R3URD);
                }
                if (leftRes && downRes && !rightRes)
                {
                    lOR.potentialRooms.Add(lOR.R1U);
                    lOR.potentialRooms.Add(lOR.R2UR);
                }
                if (leftRes && !downRes && rightRes)
                {
                    lOR.potentialRooms.Add(lOR.R1U);
                    lOR.potentialRooms.Add(lOR.R2UD);
                }
                if (!leftRes && downRes && rightRes)
                {
                    lOR.potentialRooms.Add(lOR.R1U);
                    lOR.potentialRooms.Add(lOR.R2UL);
                }
                if (!leftRes && downRes && !rightRes)
                {
                    lOR.potentialRooms.Add(lOR.R1U);
                    lOR.potentialRooms.Add(lOR.R2UL);
                    lOR.potentialRooms.Add(lOR.R2UR);
                    lOR.potentialRooms.Add(lOR.R3ULR);
                }
                if (!leftRes && !downRes && rightRes)
                {
                    lOR.potentialRooms.Add(lOR.R1U);
                    lOR.potentialRooms.Add(lOR.R2UD);
                    lOR.potentialRooms.Add(lOR.R2UL);
                    lOR.potentialRooms.Add(lOR.R3ULD);
                }
            }
        }
    }
    public void newLevel()
    {
        if (currentScene == "SampleScene")
        {
            SceneManager.LoadScene("LVL_Forest");
        }
        if (currentScene == "LVL_Forest")
        {
            SceneManager.LoadScene("LVL_Three");
        }
        if (currentScene == "LVL_Three")
        {
            SceneManager.LoadScene("endScene");
        }
    }
    public void worldStateChecker()
    {
        if (worldState == 1)
        {
            //playerState
        } else if (worldState == -1)
        {
            //Menustate
        }
    }
    public void endGameLevel()
    {
        SceneManager.LoadScene("endScene");
    }
}