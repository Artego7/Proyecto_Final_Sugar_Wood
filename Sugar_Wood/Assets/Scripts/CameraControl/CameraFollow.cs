using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    Transform camRot;
    [SerializeField]
    Transform camDummy;
    //---------------//
    [SerializeField]
    Camera cameraSO;

    //---------------//

    void Start()
    {

    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, camRot.position, cameraSO.delay * Time.deltaTime);
        transform.LookAt(camDummy);
<<<<<<< HEAD
=======

>>>>>>> 8946b0cb6be74cbda7e99576b32dc271a4455a96
    }
}
