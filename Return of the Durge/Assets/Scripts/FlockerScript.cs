using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockerScript : MonoBehaviour {
    public gameManager gM;
    public enum FlockingMode
    {
        ChaseTarget,
        FleeTarget,
        MaintainDistance,
        DoNothing,
    }

    public float SpeedPerSecond = 4.0f;
    public GameObject FlockingTarget;
    public FlockingMode CurrentFlockingMode = FlockingMode.ChaseTarget;
    public float DesiredDistanceFromTarget_Min = 3.5f;
    public float DesiredDistanceFromTarget_Max = 4.5f;
    public MaintainDistance FlockingDistance = MaintainDistance.MidRange;
    public enum MaintainDistance
    {
        ShortRange,
        MidRange,
        LongRange,
    }

    public bool AvoidWalls = true;
    // Use this for initialization
    void Start () 
    {
        //reTargetPlayer();
        Invoke("reTargetPlayer", 0.1f);
        gM = GameObject.FindObjectOfType<gameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gM.worldState == -1)
        {
            gamePaused();
            for (int i = 0; i == 1; i++)
            {
                gamePaused();
                Debug.Log("paused");
            }
        } else if (gM.worldState == 1)
        {
            reTargetPlayer();
            for (int i = 0; i == 1; i++)
            {
                reTargetPlayer();
            }
        }
        Vector3 desiredDirection = new Vector3();
 
        Vector3 vectorToTarget = FlockingTarget.transform.position - transform.position;
        float distanceToTarget = vectorToTarget.magnitude;
        
        switch (CurrentFlockingMode)
        {
            case FlockingMode.ChaseTarget:
                desiredDirection = vectorToTarget;          //Move towards target
                break;
            case FlockingMode.FleeTarget:
                desiredDirection = -vectorToTarget;
                break;
            case FlockingMode.MaintainDistance:
                {
                    switch (FlockingDistance)
                    {
                        case MaintainDistance.ShortRange:
                            DesiredDistanceFromTarget_Min = 1.5f;
                            DesiredDistanceFromTarget_Max = 2.5f;
                            break;
                        case MaintainDistance.MidRange:
                            DesiredDistanceFromTarget_Min = 4.5f;
                            DesiredDistanceFromTarget_Max = 5.5f;
                            break;
                        case MaintainDistance.LongRange:
                            DesiredDistanceFromTarget_Min = 8.5f;
                            DesiredDistanceFromTarget_Max = 9.5f;
                            break;
                    }

                    if (distanceToTarget < DesiredDistanceFromTarget_Min)
                    {
                        desiredDirection = -vectorToTarget;
                    } else if (distanceToTarget > DesiredDistanceFromTarget_Max)
                    {
                        desiredDirection = vectorToTarget;
                    }
                }
                break;
            case FlockingMode.DoNothing:
                desiredDirection = Vector3.zero;
                break;
        }

        //Change to avoid walls instead of hazards
        if(AvoidWalls)
        {
            HazardScript[] hazards = FindObjectsOfType<HazardScript>();

            Vector3 avoidanceVector = Vector3.zero;
            for (int i = 0; i < hazards.Length; ++i)
            {
                Vector3 vectorToHazard = hazards[i].transform.position - transform.position;
                if (vectorToHazard.magnitude < 4.0f)
                {
                    Vector3 vectorAwayFromHazard = -vectorToHazard;
                    avoidanceVector += vectorAwayFromHazard;
                }
            }

            if(avoidanceVector != Vector3.zero)
            {
                desiredDirection.Normalize();
                avoidanceVector.Normalize();
                desiredDirection = desiredDirection * 0.5f + avoidanceVector * 0.5f;
            }
        }

        desiredDirection.Normalize();
        transform.position += desiredDirection * SpeedPerSecond * Time.deltaTime;
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("pBullet"))
        {
            this.gameObject.GetComponent<bossScript>().health--;
        }
    }
    public void reTargetPlayer()
    {
        if (this.gameObject.CompareTag("DummyThicc"))
        {
            FlockingTarget = GameObject.FindGameObjectWithTag("ChrisChan");
        }
        else 
        {
            FlockingTarget = GameObject.FindGameObjectWithTag("Player");
        }
        if (this.gameObject.CompareTag("enemy"))
        {
            FlockingTarget = GameObject.FindGameObjectWithTag("Player");
            if (this.gameObject.GetComponentInChildren<ParticleSystem>() != null)
            {
                this.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            }
        }
    }
    public void gamePaused()
    {
        FlockingTarget = this.gameObject;
        if (this.gameObject.GetComponentInChildren<ParticleSystem>() != null)
        {
            this.gameObject.GetComponentInChildren<ParticleSystem>().Pause();
        }
    }
}
