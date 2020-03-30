using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableDetectingPlayer : MonoBehaviour
{
    [SerializeField]
    Vegetables vegetablesSO;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            vegetablesSO.isDetectingPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            vegetablesSO.isDetectingPlayer = false;
        }
    }
}
