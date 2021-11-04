using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class listOfRooms : ScriptableObject
{
    public List<GameObject> rooms;
    public int numDoorsActive;
}
