using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDummy : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    Camera cameraSO;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + cameraSO.positionDummy;
<<<<<<< HEAD
        transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * (cameraSO.delay * 30f));
        transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime * (cameraSO.delay * 30f));
=======
        transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime * (cameraSO.delay * 30f));

>>>>>>> 8946b0cb6be74cbda7e99576b32dc271a4455a96
    }
}
