using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Sweets", order = 1)]
public class Sweets : ScriptableObject
{
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;//¿¿¿¿SALTAN????


    public bool isSeeingPlayer;

}
