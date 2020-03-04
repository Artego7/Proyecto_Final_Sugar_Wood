using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    Player playerSO;

    Rigidbody rb;

    //------------------//

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerSO.isOnSand = false;
        playerSO.isTouchingGround = true;
        playerSO.isClimbing = false;
        playerSO.isJumping = false;
        
    }

    void Update()
    {

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
        }else if (playerSO.isOnSand)
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
        if (!playerSO.isClimbing || playerSO.isTouchingGround)
        {
            //Back
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
            //StartCoroutine(AddDrag());
        }
        if (rb.velocity == new Vector3(0,0,0) || Input.anyKey)
        {
            rb.drag = 0f;
        }
        
        //print(rb.velocity);
        //IEnumerator AddDrag()
        //{
        //    while (rb.velocity != new Vector3(0, 0, 0))
        //    {
        //        rb.drag = 1.5f;

        //        yield return null;
        //    }

        //    rb.velocity = Vector3.zero;
        //    rb.angularVelocity = Vector3.zero;
        //    rb.drag = 0;
        //}
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
                rb.AddForce(rb.velocity.x, transform.up.y * playerSO.climbForce, rb.velocity.z);
                //anim.GetComponent<Animator>().SetBool("Climb",true);
            }
            else if(Input.GetKey(KeyCode.S) && rb.drag == 0f)
            {
                rb.AddForce(playerSO.goDown * -playerSO.climbForce);
                //anim.GetComponent<Animator>().SetBool("Climb",true);
            }
            else
            {
                //anim.GetComponent<Animator>().SetBool("Climb",false);
            }

        }
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
        if (other.tag == "wall" && !Input.GetKey(KeyCode.W))
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
