using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimAtMouse : MonoBehaviour
{
    public bool isFlipped;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        faceMouse();
    }
    void faceMouse()
    {
        //Debug.Log("FACE MOUSE");
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        //Vector2 direction = new Vector2(mousePosition.x, mousePosition.y);

        transform.right = direction;

        if (direction.x <= 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }
        else if (direction.x >= 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipY = false;
        }
    }
}