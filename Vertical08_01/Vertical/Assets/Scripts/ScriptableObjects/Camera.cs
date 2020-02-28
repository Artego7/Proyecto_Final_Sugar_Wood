using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Camera", order = 1)]
public class Camera : ScriptableObject
{
    public float delay;
    //-------------------//
    public Vector3 positionCamera;
    public Vector3 positionDummy;
    public Vector3 rotationDummy;

}
