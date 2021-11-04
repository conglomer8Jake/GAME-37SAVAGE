using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    public GameObject roomCenter;

    public string[,,] roomMap = new string[7, 7, 2];
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
    void Start()
    {
        for (int row = 0; row < 7; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                roomMap[row, col, 1] = "F";
            }
            Debug.Log("\n");
        }
        roomMap[4, 4, 1] = "P";
        roomMap[4, 4, 0] = "R4";
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
            roomMap[playerPosX + 1, playerPosY, 0] = (string)roomCode;
            roomMap[playerPosX + 1, playerPosY, 1] = "P";
        }
        if (directionGenerated == "up")
        {
            roomMap[playerPosX, playerPosY + 1, 0] = (string)roomCode;
            roomMap[playerPosX, playerPosY + 1, 1] = "P";
        }
        if (directionGenerated == "left")
        {
            roomMap[playerPosX - 1, playerPosY, 0] = (string)roomCode;
            roomMap[playerPosX - 1, playerPosY, 1] = "P";
        }
        if (directionGenerated == "down")
        {
            roomMap[playerPosX, playerPosY - 1, 0] = (string)roomCode;
            roomMap[playerPosX, playerPosY - 1, 1] = "P";
        }
        for (int row = 0; row < 7; row++)
        {
            for (int col = 0; col < 7; col++)
            { 
                Debug.Log(roomMap[row, col,0]);
            }
            Debug.Log("\n");
        }
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