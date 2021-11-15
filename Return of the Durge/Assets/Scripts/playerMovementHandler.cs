using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementHandler : MonoBehaviour
{
    public Vector3 mousePosition;

    public string recentColl = "";
    public Transform throwPosRight;
    public GameObject throwableSomething;

    public float velX, velY;
    public float speed = 5.0f;
    public float dashSpeed = 3.0f;
    public float dashCooldown = 0;
    public float mousePosX;
    public float mousePosY;
    public float collCooldown = 3.0f;

    public int health = 5;

    public bool invokeCalled;
    public bool dashCooldownOff = true;
    public bool isDashing = false;
    public bool isMoving = false;
    public bool wDown, aDown, sDown, dDown;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Dash Cooldown
        if (dashCooldownOff == false && isDashing)
        {
            dashCooldown -= 1 * Time.deltaTime;
        }
        if (dashCooldown > 0.0f && isDashing)
        {
            dashCooldownOff = false;
        }
        else
        {
            dashCooldownOff = true;
            isDashing = false;
            dashCooldown = 6.0f;
        }
        //Player Movement using WASD
        {
            //Up
            if (Input.GetKeyDown(KeyCode.W))
            {
                isMoving = true;
                wDown = true;
            }
            //Left
            if (Input.GetKeyDown(KeyCode.A))
            {
                isMoving = true;
                aDown = true;
            }
            //Down
            if (Input.GetKeyDown(KeyCode.S))
            {
                isMoving = true;
                sDown = true;
            }
            //Right
            if (Input.GetKeyDown(KeyCode.D))
            {
                isMoving = true;
                dDown = true;
            }
            //GetKeyUp
            {
                if (Input.GetKeyUp(KeyCode.W))
                {
                    wDown = false;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -1 * speed * dashSpeed);
                }
                if (Input.GetKeyUp(KeyCode.A))
                {
                    aDown = false;
                    GetComponent<Rigidbody2D>().velocity = (new Vector2(speed * dashSpeed, 0.0f));
                }
                if (Input.GetKeyUp(KeyCode.S))
                {
                    sDown = false;
                    GetComponent<Rigidbody2D>().velocity = (new Vector2(0.0f, speed * dashSpeed));
                }
                if (Input.GetKeyUp(KeyCode.D))
                {
                    dDown = false;
                    GetComponent<Rigidbody2D>().velocity = (new Vector2(-1 * speed * dashSpeed, 0.0f));
                }
                if (wDown == false && aDown == false && sDown == false && dDown == false)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                }
            }

            //UP
            if (wDown && !dDown && !aDown)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, speed * dashSpeed);
            }
            if (wDown && aDown)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * speed * dashSpeed, speed * dashSpeed);
            }
            if (wDown && dDown)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed * dashSpeed, speed * dashSpeed);
            }
            //Left
            if (aDown && !dDown && !wDown)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * speed * dashSpeed, 0.0f);
            }
            if (aDown && sDown)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * speed * dashSpeed, -1 * speed * dashSpeed);
            }
            //Down
            if (sDown && !dDown && !aDown)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -1 * speed * dashSpeed);
            }
            if (sDown && dDown)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed * dashSpeed, -1 * speed * dashSpeed);
            }
            //Right
            if (dDown && !wDown && !sDown)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed * dashSpeed, 0.0f);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownOff == true)
        {
            dashSpeed = 4.0f;
            //GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x * dashSpeed, GetComponent<Rigidbody2D>().velocity.y * dashSpeed);
            isDashing = true;
            Invoke("resetSpeed", 0.25f);
        }
        velX = GetComponent<Rigidbody2D>().velocity.x;
        velY = GetComponent<Rigidbody2D>().velocity.y;

        //Throwing something
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(throwableSomething, throwPosRight.position, Quaternion.identity);

        }

        //Flip sprite when mouse is to left or right
        flipSprite();
    }
    public void resetSpeed()
    {
        dashSpeed = 5;
        invokeCalled = true;
        dashCooldownOff = false;
    }
    public void flipSprite()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        if (direction.x <= 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (direction.x >= 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            health--;
            Destroy(other.gameObject);
        }
    }
}
