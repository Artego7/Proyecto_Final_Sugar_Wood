using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform targettofollow;
    public Transform targettolook;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    transform.position=Vector3.Lerp(transform.position,targettofollow.position,speed*Time.deltaTime); //la funcion lerp sirve para hacer una transición fluida entre dos puntos
    transform.LookAt(targettolook);
    
    }
}
