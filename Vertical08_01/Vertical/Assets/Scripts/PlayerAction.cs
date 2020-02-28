using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    Player player;

    Rigidbody rb;

    //------------------//
    Vector3 goUp;
    Vector3 goDown;


    //------------------//
    bool isSand;
    bool isTouchingGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        goUp = new Vector3(0f, 2f, 0f);
        goDown = new Vector3(0f, -2f, 0f);

        isSand = false;
        isTouchingGround = true;
        player.isClimbing = false;
        player.isJumping = false;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement(player.maxSpeed);
        }
        else
        {
            movement(player.walkSpeed);
        }
        if (!player.isClimbing)
        {
            jump();
        }

        climb();
    }

    void movement(float playerSpeed)
    {
        //Front
        if (!player.isClimbing)
        {
            if (Input.GetKey(KeyCode.W) && rb.drag == 0f)
            {
                rb.AddForce(transform.forward * playerSpeed);
                //anim.GetComponent<Animator>().SetBool("Walk", true);
            }
        }
        if (!player.isClimbing || isTouchingGround)
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
            rb.drag = 1.5f;
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
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, player.walkSpeed);

    }

    void jump()
    {
        if (!player.isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(goUp * player.jumpForce, ForceMode.Impulse);
                //anim.GetComponent<Animator>().SetBool("Jump",true);
                player.isJumping = true;
            }
        }
    }

    void climb()
    {
        if (player.isClimbing)
        {
            if (Input.GetKey(KeyCode.W) && rb.drag == 0f)
            {
                print("gola");
                rb.AddForce(rb.velocity.x, transform.up.y * player.climbForce, rb.velocity.z);
                //anim.GetComponent<Animator>().SetBool("Climb",true);
            }
            else if(Input.GetKey(KeyCode.S) && rb.drag == 0f)
            {
                rb.AddForce(goDown * -player.climbForce);
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
            player.isClimbing = true;
        }

        if (other.tag == "sand")
        {
            player.walkSpeed -= player.walkSpeed * 0.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "wall" && !Input.GetKey(KeyCode.W))
        {
            player.isClimbing = false;
        }

        if (other.tag == "sand")
        {
            player.walkSpeed -= player.walkSpeed * 2f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "floor" || collision.collider.tag == "platform" || collision.collider.tag == "button")
        {
            player.isJumping = false;
            isTouchingGround = true;
            //anim.GetComponent<Animator>().SetBool("IsInAir?",false);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "floor" || collision.collider.tag == "platform" || collision.collider.tag == "button")
        {
            isTouchingGround = false;
            //anim.GetComponent<Animator>().SetBool("IsInAir?",false);
        }
    }
}
