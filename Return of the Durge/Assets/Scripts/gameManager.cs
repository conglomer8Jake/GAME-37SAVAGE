using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    int vertical, horizontal;

    string[,] roomMap = new string[7, 7];
    public int rowsGenerated;
    public int colsGenerated;
    void Start()
    {
        roomMap[4, 4] = "P:" + "S";
        vertical = (int)Camera.main.orthographicSize;
        horizontal = vertical * (Screen.width / Screen.height);
        for (int row = 0; row < 7; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                Debug.Log("F");
                roomMap[row, col] = "F";
            }
            Debug.Log("\n");
        }
    }
    void Update()
    {

    }
}