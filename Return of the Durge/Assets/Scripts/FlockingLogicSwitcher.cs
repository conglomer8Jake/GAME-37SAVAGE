using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlockingLogicSwitcher : MonoBehaviour {

    public FlockerScript.FlockingMode CurrentFlockingMode = FlockerScript.FlockingMode.ChaseTarget;
    public bool FlockersAvoidHazards = true;

    public Text StatusText;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetFlockingMode(FlockerScript.FlockingMode.ChaseTarget);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetFlockingMode(FlockerScript.FlockingMode.FleeTarget);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetFlockingMode(FlockerScript.FlockingMode.MaintainDistance);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetFlockingMode(FlockerScript.FlockingMode.DoNothing);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ToggleHazardAvoidance();
        }

        if(StatusText != null)
        {
            StatusText.text = "Flocking Mode: " + CurrentFlockingMode.ToString() + "\n" +
                "Avoid Hazards: " + FlockersAvoidHazards.ToString() + "\n" +
                "Keyboard 1-4 to Change Mode, 5 to Toggle Hazard Avoidance";
        }
    }

    void SetFlockingMode(FlockerScript.FlockingMode desiredFlockingMode)
    {
        CurrentFlockingMode = desiredFlockingMode;
        FlockerScript[] AllFlockers = FindObjectsOfType<FlockerScript>();
        for (int i = 0; i < AllFlockers.Length; ++i)
        {
            AllFlockers[i].CurrentFlockingMode = CurrentFlockingMode;
        }
    }

    void ToggleHazardAvoidance()
    {
        /*FlockersAvoidHazards = !FlockersAvoidHazards;
        FlockerScript[] AllFlockers = FindObjectsOfType<FlockerScript>();
        for (int i = 0; i < AllFlockers.Length; ++i)
        {
            AllFlockers[i].AvoidHazards = FlockersAvoidHazards;
        }
        */
    }
}
