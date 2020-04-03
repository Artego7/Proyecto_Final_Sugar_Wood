using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    Player playerSO;
    [SerializeField]
    Camera cameraSO;
    [SerializeField]
    Sweets[] sweet;
    [SerializeField]
    Vegetables[] vegetables;

    Rigidbody rb;

    //------------------//
    Vector3 tempPosition;

    enum moveDecreesEnum
    {
        walking,
        jumping,
        climbing,
        onSand
    }
    moveDecreesEnum moveDecrees;
    bool moving;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerSO.isOnSand = false;
        playerSO.isTouchingGround = true;
        playerSO.isClimbing = false;
        playerSO.isJumping = false;
        playerSO.weight = 60f;
    }

    void Update()
    {
        IncreesWeight();
        //print(transform.eulerAngles);
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement(playerSO.maxSpeed);
        }
        else if (playerSO.isOnSand)
        {
            movement(playerSO.slowSpeed);
        }
        else
        {
            movement(playerSO.walkSpeed);
        }
        if (!playerSO.isClimbing)
        {
            jump(playerSO.jumpForce);
        }
        else if (playerSO.isOnSand)
        {
            jump(playerSO.slowJumpForce);
        }

        climb();
    }

    void movement(float playerSpeed)
    {
        //Front
        if (!playerSO.isClimbing)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
                //anim.GetComponent<Animator>().SetBool("Walk", true);
                moving = true;
            }
        }
        //Back
        if (!playerSO.isClimbing || playerSO.isTouchingGround)
        {
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.forward * -playerSpeed * Time.deltaTime);
                //anim.GetComponent<Animator>().SetBool("Walk", true);
                moving = true;
            }
        }
        ////Right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
            //anim.GetComponent<Animator>().SetBool("Walk", true);
            moving = true;
        }
        ////Left
        if (Input.GetKey(KeyCode.A))
        {

            transform.Translate(Vector3.right * -playerSpeed * Time.deltaTime);
            //anim.GetComponent<Animator>().SetBool("Walk", true);
            moving = true;
        }
        if (!Input.GetKey(KeyCode.W) &&
            !Input.GetKey(KeyCode.A) &&
            !Input.GetKey(KeyCode.S) &&
            !Input.GetKey(KeyCode.D))
        {
            moving = false;
        }
        if (moving && !playerSO.isClimbing)
        {
            transform.eulerAngles = new Vector3(0f, cameraSO.rotationDummy.y, 0f);
        }
    }

    void jump(float jumpForce)
    {
        if (!playerSO.isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(playerSO.goUp * jumpForce, ForceMode.Impulse);
                //anim.GetComponent<Animator>().SetBool("Jump",true);
                playerSO.isJumping = true;
            }
        }
    }

    void climb()
    {
        if (playerSO.isClimbing)
        {
            rb.useGravity = false;
            if (Input.GetKey(KeyCode.W))
            {
                print("gola");
                transform.position += new Vector3(0f, playerSO.climbForce * Time.deltaTime, 0f);
                //anim.GetComponent<Animator>().SetBool("Climb",true);
                tempPosition = transform.position;
            }
            else if (Input.GetKey(KeyCode.S) && !playerSO.isTouchingGround)
            {
                transform.Translate(Vector3.down * playerSO.climbForce * Time.deltaTime);
                //anim.GetComponent<Animator>().SetBool("Climb",true);
                tempPosition = transform.position;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, tempPosition.y, transform.position.z);
                //anim.GetComponent<Animator>().SetBool("Climb",false);
            }
        }
        else
        {
            rb.useGravity = true;
        }
    }

    void DecreesWeight()
    {
        if (playerSO.isTouchingGround && !playerSO.isClimbing
            && !playerSO.isJumping && !playerSO.isOnSand)
        {
            //Runing
            if (Input.GetKey(KeyCode.LeftShift))
            {
                    DecreesWeight(playerSO.weightDecreesRuning);
            }
            //Walking
            else
            {
                    DecreesWeight(playerSO.weightDecreesWalking);
            }
        }
        else if (playerSO.isJumping)
        {
            //Jumping & Runing
            if (Input.GetKey(KeyCode.LeftShift))
            {
                    DecreesWeight((playerSO.weightDecreesJumping + playerSO.weightDecreesRuning));
            }
            //Jumping
            else
            {
                    DecreesWeight(playerSO.weightDecreesJumping);
            }
        }
        else if (playerSO.isClimbing)
        {
            //Climbing & Runing
            if (Input.GetKey(KeyCode.LeftShift))
            {
                    DecreesWeight((playerSO.weightDecreesClimbing + playerSO.weightDecreesRuning));
            }
            //Climbing On Sand
            else if (playerSO.isOnSand)
            {
                    DecreesWeight((playerSO.weightDecreesClimbing + playerSO.weightDecreesOnSand));
            }
            //Climbing
            else
            {
                    DecreesWeight(playerSO.weightDecreesClimbing);
            }
        }
        else if (playerSO.isOnSand)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //Runing & Jump On Sand
                if (playerSO.isJumping)
                {
                    DecreesWeight((playerSO.weightDecreesOnSand + playerSO.weightDecreesRuning + playerSO.weightDecreesJumping));
                }
                //Runing On Sand
                else
                {
                    DecreesWeight((playerSO.weightDecreesOnSand + playerSO.weightDecreesRuning));
                }
            }
            //Jumping On Sand
            else if (playerSO.isJumping)
            {
                DecreesWeight((playerSO.weightDecreesOnSand + playerSO.weightDecreesJumping));
            }
            //On Sand
            else
            {
                DecreesWeight(playerSO.weightDecreesOnSand);
            }
        }
        //Idle
        //playerSO.weight -= playerSO.weightDecreesIdle * Time.deltaTime;

    }
    void DecreesWeight(float weightToDecrees)
    {
        playerSO.weight -= weightToDecrees * Time.deltaTime;
    }

    void IncreesWeight()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wall" && Input.GetKey(KeyCode.W))
        {
            playerSO.isClimbing = true;
        }

        if (other.tag == "sand")
        {
            playerSO.isOnSand = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "wall")
        {
            playerSO.isClimbing = false;
        }

        if (other.tag == "sand")
        {
            playerSO.isOnSand = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "floor" || collision.collider.tag == "platform" || collision.collider.tag == "button")
        {
            playerSO.isTouchingGround = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "floor" || collision.collider.tag == "platform" || collision.collider.tag == "button")
        {
            playerSO.isJumping = false;

            //anim.GetComponent<Animator>().SetBool("IsInAir?",false);
        }
        if (collision.collider.tag == "sweet")
        {
            playerSO.weight += sweet[0].playerIncreesWeight;
        }
        if (collision.collider.tag == "vegetable")
        {
            playerSO.weight -= vegetables[0].playerDecreesWeight;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "floor" || collision.collider.tag == "platform" || collision.collider.tag == "button")
        {
            playerSO.isTouchingGround = false;
            //anim.GetComponent<Animator>().SetBool("IsInAir?",false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Ray(transform.position, transform.forward * 100f));
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Ray(transform.position, transform.right * 100f));
    }

}
