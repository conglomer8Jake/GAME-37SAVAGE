using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

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
        }
        if (directionGenerated == "up")
        {
            roomMap[playerPosY, playerPosX, 1] = "p";
            roomMap[playerPosY - 1, playerPosX, 0] = (string)roomCode;
            roomMap[playerPosY - 1, playerPosX, 1] = "P";
        }
        if (directionGenerated == "left")
        {
            roomMap[playerPosY, playerPosX, 1] = "p";
            roomMap[playerPosY, playerPosX - 1, 0] = (string)roomCode;
            roomMap[playerPosY, playerPosX - 1, 1] = "P";
        }
        if (directionGenerated == "down")
        {
            roomMap[playerPosY, playerPosX, 1] = "p";
            roomMap[playerPosY + 1, playerPosX, 0] = (string)roomCode;
            roomMap[playerPosY + 1, playerPosX, 1] = "P";
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
}