using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    Transform camDummy;
    //---------------//
    [SerializeField]
    Camera cameraSO;

    //---------------//

    void Start()
    {

        transform.position = cameraSO.positionCamera;
    }

    void Update()
    {

        //transform.position = Vector3.Lerp(transform.position, cameraSO.positionRot, cameraSO.delay * Time.deltaTime);
        //transform.LookAt(camDummy);
    }
}
