using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suputamadre : MonoBehaviour
{
    [SerializeField]
    Player playerSO;
    [SerializeField]
    Camera cameraSO;

    bool moving;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement(playerSO.walkSpeed);
    }
    void movement(float playerSpeed)
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
            moving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * -playerSpeed * Time.deltaTime);
            moving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
            moving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {

            transform.Translate(Vector3.right * -playerSpeed * Time.deltaTime);
            moving = true;
        }
        if (!Input.GetKey(KeyCode.W) && 
            !Input.GetKey(KeyCode.A) && 
            !Input.GetKey(KeyCode.S) && 
            !Input.GetKey(KeyCode.D))
        {
            moving = false;
        }
        if (moving)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.eulerAngles += new Vector3(0f, 1f, 0f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.eulerAngles += new Vector3(0f, -1f, 0f);
            }
        }
    }

}
