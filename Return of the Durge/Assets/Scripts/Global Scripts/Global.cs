using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Global : ScriptableObject
{
    public gameManager gM;


    public int numRooms;
    public int level = 1;

    public void restart()
    {
        numRooms = 1;
    }
    public void gameStart()
    {
        //Build the grid on game start
        for (int row = 0; row < 7; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                gM.roomMap[row, col, 1] = "F";
            }
            Debug.Log("\n");
        }
        gM.roomMap[4, 4, 1] = "P";
        gM.roomMap[4, 4, 0] = "R4";
    }
    public void buildGrid()
    {
        //Builds a grid on call
        for (int row = 0; row < 7; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                gM.roomMap[row, col, 1] = "F";
            }
            Debug.Log("\n");
        }
        gM.roomMap[gM.playerPosX, gM.playerPosY, 1] = "P";
    }
}