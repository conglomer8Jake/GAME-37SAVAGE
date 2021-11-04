using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyInertSpawner : MonoBehaviour
{
    public gameManager gM;
    public GameObject spawnerToBeDestroyed;
    void Start()
    {
        gM = GameObject.FindObjectOfType<gameManager>();
        if (gM.directionGenerated == "up")
        {
            spawnerToBeDestroyed = GameObject.FindGameObjectWithTag("south");
            Destroy(spawnerToBeDestroyed);
        }
        if (gM.directionGenerated == "down")
        {
            spawnerToBeDestroyed = GameObject.FindGameObjectWithTag("north");
            Destroy(spawnerToBeDestroyed);
        }
        if (gM.directionGenerated == "left")
        {
            spawnerToBeDestroyed = GameObject.FindGameObjectWithTag("east");
            Destroy(spawnerToBeDestroyed);
        }
        if (gM.directionGenerated == "right")
        {
            spawnerToBeDestroyed = GameObject.FindGameObjectWithTag("west");
            Destroy(spawnerToBeDestroyed);
        }
 
    }
}
