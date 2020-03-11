using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Player", order = 1)]
public class Player : ScriptableObject
{
    [Header("Set Movement")]
    public float maxSpeed;
    public float walkSpeed;
    public float slowSpeed;
    [Space(10)]
    //-----------------//
    [Header("Set Jump/Climb Force")]
    public float maxJumpForce;
    public float jumpForce;
    public float slowJumpForce;
    public float climbForce;
    public Vector3 goUp;
    public Vector3 goDown;
    [Space(10)]
    //-----------------//
    [Header("Set Weight Parameters")]
    public float weight;
    public float weightDecreesIdle;
    public float weightDecreesWalking;
    public float weightDecreesRuning;
    public float weightDecreesJumping;
    public float weightDecreesClimbing;
    public float weightDecreesOnSand;
    [Space(10)]
    //-----------------//
    [Header("Set Drag Force")]
    public float dragForce;
    [Space(10)]
    //-----------------//
    [Header("Set Booleans")]
    public bool isTouchingGround;
    public bool isJumping;
    public bool isClimbing;
    public bool isOnSand;
}
