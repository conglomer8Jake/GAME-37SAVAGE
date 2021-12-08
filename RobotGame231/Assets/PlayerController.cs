using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float jumpDelayTime = 0.2f;
    float respawnTime = 2.0f;
    public float jumpSpeed = 1.0f;
    float gravity = 20.0f;
    public float jumpAnimSpeed = 1.0f;
    public float walkSpeed = 2.0f;
    public float walkAnimSpeed = 1.0f;
    public float runSpeed = 6.0f;
    public float runAnimSpeed = 1.25f;

    private float rotateSpeed = 150.0f;
    bool grounded = false;
    Vector3 moveDirection = Vector3.zero;
    bool isWalking = true;
    string moveStatus = "idle";
    float xSpeed = 250.0f;
    float ySpeed = 120.0f;
    float yMin = -40.0f;
    float yMax = 80.0f;
    float x = 0.0f;
    float y = 0.0f;
    bool jumpMove;
    bool canJump;
    bool dead;
    Vector3 startPos;
    Quaternion startRot;

    public AnimationClip idleAnim;
    public AnimationClip walkAnim;
    public AnimationClip runAnim;
    public AnimationClip jumpAnim;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animation>()[jumpAnim.name].layer = 10;
        canJump = true;
        startPos = this.transform.position;
        startRot = this.transform.rotation;
    }

    void Update()
    {
        if (dead == false)
        {
            GetComponent<Animation>()[walkAnim.name].speed = walkAnimSpeed;
            GetComponent<Animation>()[runAnim.name].speed = runAnimSpeed;
            //Only allow movement and jumps while grounded
            if (grounded)
            {
                moveDirection = new Vector3((Input.GetMouseButton(1) ? Input.GetAxis("Horizontal") : 0), 0, Input.GetAxis("Vertical"));
                
                moveDirection *= isWalking ? walkSpeed : runSpeed;
                
                moveStatus = "idle";
                if(moveDirection != Vector3.zero)
                {
                    moveStatus = !isWalking ? "walking" : "running";
                    if (isWalking)
                    {
                        //invoke walk animation
                        GetComponent<Animation>().Play(walkAnim.name);
                    }
                    else
                    {
                        //call Run animation
                        GetComponent<Animation>().CrossFade(runAnim.name);
                    }
                }
                else
                {
                    //call idle animation
                    GetComponent<Animation>().CrossFade(idleAnim.name);
                }
                
                //Jump
                if(Input.GetKeyDown(KeyCode.Space) && canJump == true)
                {
                    canJump = false;
                    AnimationState jmp = GetComponent<Animation>().CrossFadeQueued(jumpAnim.name, 0.1f, QueueMode.PlayNow);
                    jmp.speed = jumpAnimSpeed;
                    StartCoroutine(JumpDelay());
                }
                if(jumpMove == true)
                {
                    moveDirection.y = 2 * jumpSpeed;
                    moveDirection += transform.forward * jumpSpeed;
                    jumpMove = false;
                    canJump = true;
                }
            } //End Grounded
            // Allow turning at anytime
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);

            if(Input.GetKeyDown("left shift") || Input.GetKeyDown("right shift"))
            {//toggle walk/run
                isWalking = !isWalking;
            }
            moveDirection.y -= 10 * gravity * Time.deltaTime;
            //Move Controller
            CharacterController controller = GetComponent<CharacterController>();
            var flags = controller.Move(moveDirection * Time.deltaTime);
            grounded = (flags & CollisionFlags.Below) != 0;


        }
    }

    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(jumpDelayTime);
        jumpMove = true;
    }

}
