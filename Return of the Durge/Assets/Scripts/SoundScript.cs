using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public AudioSource deathSound;
    public AudioSource shootSound;
    //public GameObject test;
    //private Healthfind health1;
    // Start is called before the first frame update
    void Start()
    {
       // GameObject enemy = GameObject.Find("enemy");
        GameObject.Find("blooter").GetComponent<bloatedDurgy>().health = 5;
      //  bloatedDurgy bloatedDurgy = enemy.GetComponent<bloatedDurgy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<bloatedDurgy>().health == 0)
        {
            deathSound.Play();
        }
        /*if(onattackidk)
        {
            shootSound.Play();
        }*/
    }
}
