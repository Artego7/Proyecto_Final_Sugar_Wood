using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Sweets", order = 1)]
public class Sweets : ScriptableObject
{
    [Header("Move & Jump")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    [Space(10)]

    //-----------------//

    [Header("Set Direction")]
    public float rotationSpeed;
    [Space(10)]

    //-----------------//

    [Header("Set Vision Area")]
    public float distanceVision;
    public Vector3 positionVision;
    public Vector3 sizeVision;
    public bool isTriggerVision;
    [Space(10)]

    [Header("Set Detection Area")]
    public Vector3 positionDetection;
    public float radiusDetection;
    public bool isTriggerDetection;
    [Space(10)]

    //-----------------//

    [Header("Booleans")]
    public bool isSeeingPlayer;
    public bool isDetectingPlayer;
}
