using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtPlayer : MonoBehaviour
{
	public GameObject player;
	public Transform targetToFace;

	// Use this for initialization
	void Start()
	{
		Invoke("reTarget", 0.1f);//FOR TESTING ONLY
		//reTarget();
	}

	// Update is called once per frame
	void Update()
	{
		targetToFace = player.transform;
		Vector3 playerPos = targetToFace.position;
		//playerPos = Camera.main.ScreenToWorldPoint(playerPos);
		Vector2 direction = new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y);

		transform.right = direction;
	}
	public void reTarget()
    {
		player = GameObject.FindGameObjectWithTag("Player");
	}
}
