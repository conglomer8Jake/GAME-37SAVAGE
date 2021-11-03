using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        FlockerScript flocker = collision.gameObject.GetComponent<FlockerScript>();
        if(flocker != null)
        {
            Destroy(collision.gameObject);
        }
    }
}
