using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableSeeingPlayer : MonoBehaviour
{
    [SerializeField]
    Vegetables candySO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        //if (other.tag == "Player")
        //{
        //    candySO.isDetectingPlayer = true;
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        //if (other.tag == "Player")
        //{
        //    candySO.isDetectingPlayer = false;
        //}
    }
}
