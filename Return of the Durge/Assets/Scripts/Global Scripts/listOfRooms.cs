using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class listOfRooms : ScriptableObject
{
    public List<GameObject> rooms;
    public int numDoorsActive;
    public List<GameObject> potentialRooms;

    public GameObject R4;
    public GameObject R3ULR;
    public GameObject R3ULD;
    public GameObject R3URD;
    public GameObject R3DLR;
    public GameObject R2UD;
    public GameObject R2UL;
    public GameObject R2UR;
    public GameObject R2DL;
    public GameObject R2DR;
    public GameObject R2LR;
    public GameObject R1U;
    public GameObject R1L;
    public GameObject R1D;
    public GameObject R1R;

    public void resetList()
    {
        potentialRooms.Clear();
    }
}
