using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtPlayer : MonoBehaviour
{
	public Transform targetToFace;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Vector3 playerPos = targetToFace.position;
		//playerPos = Camera.main.ScreenToWorldPoint(playerPos);
		Vector2 direction = new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y);

		transform.right = direction;
	}

}
