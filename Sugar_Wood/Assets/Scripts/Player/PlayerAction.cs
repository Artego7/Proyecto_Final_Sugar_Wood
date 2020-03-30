using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    Player playerSO;
    [SerializeField]
    Sweets[] sweet;
    [SerializeField]
    Vegetables[] vegetables;

    Rigidbody rb;

    //------------------//
    Vector3 tempPosition;

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
        DecreesWeight();
        IncreesWeight();
        //print(rb.velocity);
    }

    void FixedUpdate()
    {
        rotate();
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
            if (Input.GetKey(KeyCode.W) && rb.drag == 0f)
            {
                rb.AddForce(transform.forward * playerSpeed);
                //anim.GetComponent<Animator>().SetBool("Walk", true);
            }
        }
        //Back
        if (!playerSO.isClimbing || playerSO.isTouchingGround)
        {
            if (Input.GetKey(KeyCode.S) && rb.drag == 0f)
            {
                rb.AddForce(transform.forward * -playerSpeed);
                //anim.GetComponent<Animator>().SetBool("Walk", true);
            }
        }
        ////Right
        if (Input.GetKey(KeyCode.D) && rb.drag == 0f)
        {
            rb.AddForce(transform.right * playerSpeed);
            //anim.GetComponent<Animator>().SetBool("Walk", true);
        }
        ////Left
        else if (Input.GetKey(KeyCode.A) && rb.drag == 0f)
        {
            rb.AddForce(transform.right * -playerSpeed);
            //anim.GetComponent<Animator>().SetBool("Walk", true);
        }
        if (!Input.anyKey)
        {
            rb.drag = playerSO.dragForce;
        }
        if (rb.velocity == new Vector3(0, 0, 0) || Input.anyKey)
        {
            rb.drag = 0f;
        }
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, playerSO.walkSpeed);

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

            if (Input.GetKey(KeyCode.W) && rb.drag == 0f)
            {
                print("gola");
                //rb.position += new Vector3(0f,playerSO.climbForce,0f);
                rb.velocity = new Vector3(rb.velocity.x, transform.up.y * playerSO.climbForce, rb.velocity.z);
                //rb.AddForce(rb.velocity.x, transform.up.y * playerSO.climbForce, rb.velocity.z);
                //anim.GetComponent<Animator>().SetBool("Climb",true);
                tempPosition = rb.position;
            }
            else if (Input.GetKey(KeyCode.S) && rb.drag == 0f && !playerSO.isTouchingGround)
            {
                rb.velocity = new Vector3(rb.velocity.x, transform.up.y * -playerSO.climbForce, rb.velocity.z);
                //anim.GetComponent<Animator>().SetBool("Climb",true);
                tempPosition = rb.position;
            }
            else
            {
                rb.position = new Vector3(rb.position.x, tempPosition.y, rb.position.z);
                //anim.GetComponent<Animator>().SetBool("Climb",false);
            }
        }
    }

    void rotate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.Rotate -= playerSO.rotation;
            transform.Rotate(new Vector3(0f, -playerSO.rotation, 0f));
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            //transform.Rotate -= playerSO.rotation;
            transform.Rotate(new Vector3(0f, playerSO.rotation, 0f));
        }
    }

    void DecreesWeight()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            if (playerSO.isTouchingGround && !playerSO.isClimbing
                && !playerSO.isJumping && !playerSO.isOnSand)
            {
                //Runing
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    playerSO.weight -= playerSO.weightDecreesRuning * Time.deltaTime;
                }
                //Walking
                else
                {
                    playerSO.weight -= playerSO.weightDecreesWalking * Time.deltaTime;
                }
            }
            else if(playerSO.isJumping)
            {
                //Jumping & Runing
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    playerSO.weight -= (playerSO.weightDecreesJumping + playerSO.weightDecreesRuning) * Time.deltaTime;
                }
                //Jumping
                else
                {
                    playerSO.weight -= playerSO.weightDecreesJumping * Time.deltaTime;
                }
            }
            else if (playerSO.isClimbing)
            {
                //Climbing & Runing
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    playerSO.weight -= (playerSO.weightDecreesClimbing + playerSO.weightDecreesRuning) * Time.deltaTime;
                }
                //Climbing On Sand
                else if (playerSO.isOnSand)
                {
                    playerSO.weight -= (playerSO.weightDecreesClimbing + playerSO.weightDecreesOnSand) * Time.deltaTime;
                }
                //Climbing
                else
                {
                    playerSO.weight -= playerSO.weightDecreesClimbing * Time.deltaTime;
                }
            }
            else if (playerSO.isOnSand)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    //Runing & Jump On Sand
                    if(playerSO.isJumping)
                    {
                        playerSO.weight -= (playerSO.weightDecreesOnSand + playerSO.weightDecreesRuning + playerSO.weightDecreesJumping) * Time.deltaTime;
                    }
                    //Runing On Sand
                    else
                    {
                        playerSO.weight -= (playerSO.weightDecreesOnSand + playerSO.weightDecreesRuning) * Time.deltaTime;
                    }
                }
                //Jumping On Sand
                else if (playerSO.isJumping)
                {
                    playerSO.weight -= (playerSO.weightDecreesOnSand + playerSO.weightDecreesJumping) * Time.deltaTime;
                }
                //On Sand
                else
                {
                    playerSO.weight -= playerSO.weightDecreesOnSand * Time.deltaTime;
                }
            }
        }
        //Idle
        else
        {
            playerSO.weight -= playerSO.weightDecreesIdle * Time.deltaTime;
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "floor" || collision.collider.tag == "platform" || collision.collider.tag == "button")
        {
            playerSO.isJumping = false;
            playerSO.isTouchingGround = true;
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
}
