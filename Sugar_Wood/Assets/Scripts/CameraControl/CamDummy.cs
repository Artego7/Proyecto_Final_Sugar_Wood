using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDummy : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    Camera cameraSO;


    //-----TEMP-----//

    float speedH = 2.0f;
    float speedV = 2.0f;

    float yaw;
    float pitch;


    //-----TEMP-----//

    void Start()
    {
        yaw = 0f;
        pitch = 0f;
        cameraSO.rotationDummy = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + cameraSO.positionDummy;
        yaw = speedH * Input.GetAxis("Mouse X");
        pitch = speedV * -Input.GetAxis("Mouse Y");

        transform.eulerAngles += new Vector3(pitch, yaw, 0f);
        cameraSO.rotationDummy = transform.eulerAngles;

        //transform.Rotate(pitch, yaw, 0f);
        //transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime * (cameraSO.delay * 30f));
        //transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime * (cameraSO.delay * 30f));
    }
}
