using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healingCodeScript : MonoBehaviour
{
    public GameObject chrisChan;
    public Vector2 target;
    public float speed = 5.0f;
    private void Start()
    {
       
        chrisChan = GameObject.FindGameObjectWithTag("ChrisChan");
        target = new Vector2(chrisChan.transform.position.x, chrisChan.transform.position.y);
    }
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }
}
