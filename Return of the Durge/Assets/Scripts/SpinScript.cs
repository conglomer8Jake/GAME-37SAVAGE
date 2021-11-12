using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour {

    float CurrentRotation;

    public float RotationSpeed = 180.0f;

	// Use this for initialization
	void Start () {
        CurrentRotation = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        CurrentRotation += RotationSpeed * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(
            0.0f,
            0.0f,
            CurrentRotation
            );
	}
}
