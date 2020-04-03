using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDetectingPlayer : MonoBehaviour
{
    [SerializeField]
    Sweets sweetSO;

    public Transform posHidePoint;
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
        if (other.tag == "Player")
        {
            sweetSO.isDetectingPlayer = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HidePoint")
        {
            print("hidePoint");
            if (sweetSO.isSeeingPlayer && !sweetSO.isGoingToHidePoint)
            {
                print("hidePoint see");
                sweetSO.isGoingToHidePoint = true;
                posHidePoint = other.gameObject.transform;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            sweetSO.isDetectingPlayer = false;
        }
    }
}
