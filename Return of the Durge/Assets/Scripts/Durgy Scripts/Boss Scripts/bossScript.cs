using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossScript : MonoBehaviour
{
    public enum boss
    {
        E,
        Ryan,
        ChrisChan,
        DummyThicc
    }
    public boss currentBoss;
    public boss currentPartnerBoss;

    public GameObject partnerBossObject;
    public GameObject player;

    public Vector2 target;

    public string currentMode;
    public string selfState;
    
    public int health;

    public float coolDownTimer = 15.0f;
    void Start()
    {
        {
            player = GameObject.FindGameObjectWithTag("Player");
            selfState = "calmMode";
        }

        if (this.gameObject.CompareTag("E"))
        {
            currentBoss = boss.E;
            partnerBossObject = GameObject.FindGameObjectWithTag("Ryan");
            currentPartnerBoss = boss.Ryan;
        }
        if (this.gameObject.CompareTag("Ryan"))
        {
            currentBoss = boss.Ryan;
            partnerBossObject = GameObject.FindGameObjectWithTag("E");
            currentPartnerBoss = boss.E;
        }
        if (this.gameObject.CompareTag("ChrisChan"))
        {
            currentBoss = boss.ChrisChan;
            partnerBossObject = GameObject.FindGameObjectWithTag("DummyThicc");
            currentPartnerBoss = boss.DummyThicc;
        }
        if (this.gameObject.CompareTag("DummyThicc"))
        {
            currentBoss = boss.DummyThicc;
            partnerBossObject = GameObject.FindGameObjectWithTag("ChrisChan");
            currentPartnerBoss = boss.ChrisChan;
        }
    }
    void Update()
    {
        if (health <= health / 2)
        {
            selfState = "panicMode";
            partnerBossObject.GetComponent<bossScript>().selfState = "panicMode";
        }
        if (currentBoss == boss.E)
        {
            health = 50;
        }
        if (currentBoss == boss.Ryan)
        {
            health = 15;
        }
        if (currentBoss == boss.ChrisChan)
        {
            health = 40;
            coolDownTimer -= 1 * Time.deltaTime;
            if (coolDownTimer <= 0 && selfState == "calmMode")
            {
                calmMode();
                coolDownTimer = 15.0f;
            }
        }
        if (currentBoss == boss.DummyThicc)
        {
            health = 20;
            coolDownTimer -= 1 * Time.deltaTime;
            if (coolDownTimer <= 0 && selfState == "calmMode")
            {
                calmMode();
                coolDownTimer = 15.0f;
            }
        }
    }
    public void calmMode()
    {
        switch (currentBoss)
        {
            case boss.E:
                //ability 1
                break;
            case boss.Ryan:
                //ability 1
                break;
            case boss.ChrisChan:
                this.gameObject.GetComponent<FlockerScript>().FlockingTarget = this.gameObject;
                chrisSlam();
                break;
            case boss.DummyThicc:
                this.gameObject.GetComponent<dummyThiccBOSS>().healChris();
                break;
        }
    }
    public void panicMode()
    {
        switch (currentBoss)
        {
            case boss.E:
                //Elijah's funcitonality
                break;
            case boss.Ryan:
                //Ryan's functionality
                break;
            case boss.ChrisChan:
                //Chris' functionality
                break;
            case boss.DummyThicc:
                //Jake's functionality
                break;
        }
    }
    public void chrisSlam()
    {
        //play the slam animation
        //check if sprite overlaps with the player
        //if so, knock player back X amount away from the boss
        this.gameObject.GetComponent<FlockerScript>().reTargetPlayer();
    }
}
