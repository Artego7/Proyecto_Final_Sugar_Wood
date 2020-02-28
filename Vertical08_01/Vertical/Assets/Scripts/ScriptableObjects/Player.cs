using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Player", order = 1)]
public class Player : ScriptableObject
{

    public float maxSpeed;
    public float walkSpeed;
    public float slowSpeed;
    //-----------------//
    public float maxJumpForce;
    public float jumpForce;
    public float slowJumpForce;
    public float climbForce;
    //-----------------//
    public Vector3 goUp;
    public Vector3 goDown;
    //-----------------//
    public float weight;
    public float weightDecrees;
    //-----------------//
    public float dragForce;
    //-----------------//
    public bool isTouchingGround;
    public bool isJumping;
    public bool isClimbing;
    public bool isOnSand;


    public void moveTo(Vector3 Direction)
    {

    }
}
