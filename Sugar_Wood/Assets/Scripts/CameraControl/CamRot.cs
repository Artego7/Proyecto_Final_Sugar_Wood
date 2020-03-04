using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRot : MonoBehaviour
{
    [SerializeField]
    Camera cameraSO;

    void Start()
    {
        transform.position = cameraSO.positionCamera;
    }

    void Update()
    {
        
    }
}
