using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementHandler : MonoBehaviour
{
    public GameObject levelChanger;
    public gameManager GM;
    public string recentColl = "";
    public string spriteDirectoin = "";
    public Transform throwPosLeft, throwPosRight, throwPosUp, throwPosDown;
    public Transform throwPosActual;
    public GameObject throwableSomething;
    public Animator Nolanator;
    public Animator faded;

    public AudioSource nolanShoot;

    public float velX, velY;
    public float speed = 2.0f;
    public float dashSpeed = 3.0f;
    public float dashCooldown = 0;
    public float mousePosX;
    public float mousePosY;
    public float collCooldown = 3.0f;
    public float fireCooldown = 0.75f;
    public float shotgunCooldown = 3.0f;

    public int health = 5;

    public bool shotgunRecent = false;
    public bool fireRecent = false;
    public bool invokeCalled;
    public bool dashCooldownOff = true;
    public bool isDashing = false;
    public bool isMoving = false;
    public bool wDown, aDown, sDown, dDown;
    void Start()
    {
        GM = GameObject.FindObjectOfType<gameManager>();
        levelChanger = GameObject.FindGameObjectWithTag("levelChanger");
        faded = levelChanger.GetComponent<Animator>();
        faded.SetBool("fadeOut",false);
    }
    void Update()
    {
        if (health <= 0)
        {
            GM.endGameLevel();
        }
        if(GM.worldState != -1){
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
                        isMoving = false;
                    }
                }
                //UP
                {
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
                }
                //Left
                {
                    if (aDown && !dDown && !wDown)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * speed * dashSpeed, 0.0f);
                    }
                    if (aDown && sDown)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * speed * dashSpeed, -1 * speed * dashSpeed);
                    }
                }
                //Down
                {
                    if (sDown && !dDown && !aDown)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -1 * speed * dashSpeed);
                    }
                    if (sDown && dDown)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * dashSpeed, -1 * speed * dashSpeed);
                    }
                }
                //Right
                {
                    if (dDown && !wDown && !sDown)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * dashSpeed, 0.0f);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownOff == true)
            {
                dashSpeed = 4.5f;
                isDashing = true;
                Invoke("resetSpeed", 0.35f);
            }
            velX = GetComponent<Rigidbody2D>().velocity.x;
            velY = GetComponent<Rigidbody2D>().velocity.y;
            //Throwing something
            {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !fireRecent)
                {
                    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePosX = mousePos.x;
                    mousePosY = mousePos.y;
                    throwPosCheck();
                    Instantiate(throwableSomething, throwPosActual.position, Quaternion.identity);
                    nolanShoot.Play();
                    fireRecent = true;
                }
                if (fireRecent)
                {
                    fireCooldown -= 1 * Time.deltaTime;
                }
                if (fireCooldown <= 0)
                {
                    fireRecent = false;
                    fireCooldown = 0.75f;
                }
            }
            {
                if (Input.GetKeyDown(KeyCode.Mouse1) && !shotgunRecent)
                {
                    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePosX = mousePos.x;
                    mousePosY = mousePos.y;
                    throwPosCheck();
                    for (int i = 0; i < 5; i++)
                    {
                        Instantiate(throwableSomething, throwPosActual.position, Quaternion.identity);
                        nolanShoot.Play();
                        shotgunRecent = true;
                    }
                }
                if (shotgunRecent)
                {
                    shotgunCooldown -= 1 * Time.deltaTime;
                }
                if (shotgunCooldown <= 0)
                {
                    shotgunRecent = false;
                    shotgunCooldown = 3.0f;
                }
            }  
        }
        if (GM.worldState == -1)
        {
            wDown = false;
            aDown = false;
            sDown = false;
            dDown = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }
        //Flip sprite when mouse is to left or right
        flipSprite();

        if (isMoving == true)
        {
            Nolanator.SetBool("IsWalking", true);
            Nolanator.Play("Nolan_Walk");
        }
        if (isMoving != true)
        {
            Nolanator.SetBool("IsWalking", false);
            Nolanator.Play("Nolan_Idle");
        }
    }
    public void resetSpeed()
    {
        dashSpeed = 3;
        invokeCalled = true;
        dashCooldownOff = false;
    }
    public void flipSprite()
    {
        //Change the sprite of the player depending on mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        if (direction.x <= 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            spriteDirectoin = "left";
        }
        else if (direction.x >= 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            spriteDirectoin = "right";
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("stair"))
        {
            faded.SetBool("fadeOut", true);
            Invoke("callNewLevel", 1.0f);
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("particle"))
        {
            health--;
            Debug.Log("Smoocked");
            //Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("chrisFistL") || other.gameObject.CompareTag("chrisFistR"))
        {
            health--;
        }
    }
    void OnParticleCollision(GameObject other)
    {
        health--;
        //Destroy(other);
    }

    public void throwPosCheck()
    {
        if (mousePosX - transform.position.x < 0 && Mathf.Abs(mousePosX) > Mathf.Abs(mousePosY))
        {
            throwPosActual = throwPosLeft;
        }
        if (mousePosX - transform.position.x > 0 && Mathf.Abs(mousePosX) > Mathf.Abs(mousePosY))
        {
            throwPosActual = throwPosRight;
        }
        if (mousePosY - transform.position.y < 0 && Mathf.Abs(mousePosY) > Mathf.Abs(mousePosX))
        {
            throwPosActual = throwPosDown;
        }
        if (mousePosY - transform.position.y > 0 && Mathf.Abs(mousePosY) > Mathf.Abs(mousePosX))
        {
            throwPosActual = throwPosUp;
        }

        /*
            if (spriteDirectoin == "left")
        {
            throwPosActual = throwPosLeft;
        }
        if (spriteDirectoin == "right")
        {
            throwPosActual = throwPosRight;
        } */
    }
    public void callNewLevel()
    {
        GM.newLevel();
    }
}