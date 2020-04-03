using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public bool puenteActivo;
    float smooth = 1.5f;
    float tiltAngle = 100.0f;

    public Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (puenteActivo == true)
        {

            // Rotate the cube by converting the angles into a quaternion
            Quaternion target = Quaternion.Euler(rotation.x, rotation.y, rotation.z);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        }
    }
}
