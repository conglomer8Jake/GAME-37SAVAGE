using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public Global vH;
    public listOfRooms lOR;
    public GameObject roomCenter;

    public const int COLUMN_COUNT = 7;
    public const int ROW_COUNT = 7;

    public string[,,] roomMap = new string[ROW_COUNT, COLUMN_COUNT, 2];
    //plane 0 = room information
    //plane 1 = player position

    public string directionGenerated;
    public string roomCode;
    public string playerPos;

    public int vertical, horizontal;
    public int rowsGenerated;
    public int colsGenerated;
    public int playerPosX;
    public int playerPosY;

    public float roomGenCooldown = 5.0f;

    public bool roomMadeRecent = false;
    public bool topRes;
    public bool leftRes;
    public bool downRes;
    public bool rightRes;
    void Start()
    {
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
        for (int row = 0; row < 7; row++)
        {
            for (int col = 0; col < 7; col++)
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
        if (roomMadeRecent)
        {
            roomGenCooldown -= 1 * Time.deltaTime;
        }
        if (roomGenCooldown <= 0)
        {
            roomMadeRecent = false;
            roomGenCooldown = 5.0f;
        }
    }
    private void DebugOutRoomArray()
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
        Debug.Log("Function Called");
        //checking room's neighbors 
        if (directionGenerated == "right")
            {
            //check up, right, down
            {
                if (roomMap[playerPosY - 1, playerPosX, 0].Contains("F"))
                {
                    topRes = false;
                }
                else if (roomMap[playerPosY - 1, playerPosX, 0].Contains("R3ULR") || roomMap[playerPosY - 1, playerPosX, 0].Contains("R2UL") || roomMap[playerPosY - 1, playerPosX, 0].Contains("R2UR") || roomMap[playerPosY - 1, playerPosX, 0].Contains("R2LR") || !roomMap[playerPosY - 1, playerPosX, 0].Contains("R1D"))
                {
                    topRes = true;
                }
                if (roomMap[playerPosY, playerPosX + 1, 0].Contains("F"))
                {
                    rightRes = false;
                }
                else if (roomMap[playerPosY, playerPosX + 1, 0].Contains("R3URD") || roomMap[playerPosY, playerPosX + 1, 0].Contains("R2UD") || roomMap[playerPosY, playerPosX + 1, 0].Contains("R2UR") || roomMap[playerPosY, playerPosX + 1, 0].Contains("R2DR") || !roomMap[playerPosY, playerPosX + 1, 0].Contains("R1L"))
                {
                    rightRes = true;
                }
                if (roomMap[playerPosY + 1, playerPosX, 0].Contains("F"))
                {
                    downRes = false;
                }
                else if (roomMap[playerPosY + 1, playerPosX, 0].Contains("R3DLR") || roomMap[playerPosY + 1, playerPosX, 0].Contains("R2DL") || roomMap[playerPosY + 1, playerPosX, 0].Contains("R2DR") || roomMap[playerPosY + 1, playerPosX, 0].Contains("R2LR") || !roomMap[playerPosY + 1, playerPosX, 0].Contains("R1U"))
                {
                    downRes = true;
                }
            }
            //Add rooms to the restricted room list 
            {
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
                if (roomMap[playerPosY - 1, playerPosX, 0].Contains("F"))
                {
                    topRes = false;
                }
                else if (roomMap[playerPosY - 1, playerPosX, 0].Contains("R3ULR") || roomMap[playerPosY - 1, playerPosX, 0].Contains("R2UL") || roomMap[playerPosY - 1, playerPosX, 0].Contains("R2UR") || roomMap[playerPosY - 1, playerPosX, 0].Contains("R2LR") || !roomMap[playerPosY - 1, playerPosX, 0].Contains("R1D"))
                {
                    topRes = true;
                }
                if (roomMap[playerPosY, playerPosX + 1, 0].Contains("F"))
                {
                    rightRes = false;
                }
                else if (roomMap[playerPosY, playerPosX + 1, 0].Contains("R3URD") || roomMap[playerPosY, playerPosX + 1, 0].Contains("R2UD") || roomMap[playerPosY, playerPosX + 1, 0].Contains("R2UR") || roomMap[playerPosY, playerPosX + 1, 0].Contains("R2DR") || !roomMap[playerPosY, playerPosX + 1, 0].Contains("R1L"))
                {
                    rightRes = true;
                }
                if (roomMap[playerPosY, playerPosX - 1, 0].Contains("F"))
                {
                    leftRes = false;
                }
                else if (roomMap[playerPosY, playerPosX - 1, 0].Contains("R3ULD") || roomMap[playerPosY, playerPosX - 1, 0].Contains("R2UD") || roomMap[playerPosY, playerPosX - 1, 0].Contains("R2UL") || roomMap[playerPosY, playerPosX - 1, 0].Contains("R2DL") || !roomMap[playerPosY, playerPosX - 1, 0].Contains("R1R"))
                {
                    leftRes = true;
                }
            }
            //Add rooms to the restricted room list
            {
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
                }
                if (topRes && rightRes && !leftRes)
                {
                    lOR.potentialRooms.Add(lOR.R1D);
                }
                if (topRes && !rightRes && leftRes)
                {
                    lOR.potentialRooms.Add(lOR.R1D);
                }
                if (!topRes && rightRes && leftRes)
                {
                    lOR.potentialRooms.Add(lOR.R1D);
                }
                if (!topRes && rightRes && !leftRes)
                {
                    lOR.potentialRooms.Add(lOR.R1D);
                }
                if (!topRes && !rightRes && leftRes)
                {
                    lOR.potentialRooms.Add(lOR.R1D);
                }
            }
        }
        if (directionGenerated == "left")
        {
            //check up, left, down
            {

                if (roomMap[playerPosY - 1, playerPosX, 0] == "F")
                {
                    topRes = false;
                }
                else if (roomMap[playerPosY - 1, playerPosX, 0].Contains("R3ULR") || roomMap[playerPosY - 1, playerPosX, 0].Contains("R2UL") || roomMap[playerPosY - 1, playerPosX, 0].Contains("R2UR") || roomMap[playerPosY - 1, playerPosX, 0].Contains("R2LR") || !roomMap[playerPosY - 1, playerPosX, 0].Contains("R1D"))
                {
                    topRes = true;
                }
                if (roomMap[playerPosY, playerPosX - 1, 0] == "F")
                {

                }
                else if (roomMap[playerPosY, playerPosX - 1, 0].Contains("R3ULD") || roomMap[playerPosY, playerPosX - 1, 0].Contains("R2UD") || roomMap[playerPosY, playerPosX - 1, 0].Contains("R2UL") || roomMap[playerPosY, playerPosX - 1, 0].Contains("R2DL") || !roomMap[playerPosY, playerPosX - 1, 0].Contains("R1R"))
                {
                    leftRes = true;
                }
                if (roomMap[playerPosY + 1, playerPosX, 0].Contains("F"))
                {
                    downRes = false;
                }
                else if (roomMap[playerPosY + 1, playerPosX, 0].Contains("R3DLR") || roomMap[playerPosY + 1, playerPosX, 0].Contains("R2DL") || roomMap[playerPosY + 1, playerPosX, 0].Contains("R2DR") || roomMap[playerPosY + 1, playerPosX, 0].Contains("R2LR") || !roomMap[playerPosY + 1, playerPosX, 0].Contains("R1U"))
                {
                    downRes = true;
                }
            }
            //Add rooms to the restricted room list
            {
                if (topRes && leftRes && downRes)
                {
                    lOR.potentialRooms.Add(lOR.R1R);
                }
            }
        }
        if (directionGenerated == "down")
        {
            //check left, down, right

            //Add rooms to the restriced room list
        }
    }
}