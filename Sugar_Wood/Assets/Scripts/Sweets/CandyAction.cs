using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyAction : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    Transform player;
    [Space(3)]

    Transform sweet;

    Transform triggerDetecting;
    SphereCollider triggerDetectingCol;

    [Header("Sweet ScriptableObject")]
    [SerializeField]
    Sweets candySO;

    //-------TEMP-------//
    float maxDistance = 50f;
    float minDistance = 25f;
    Vector3 startPosition;
    //-------TEMP-------//

    void Start()
    {
        sweet = GetComponent<Transform>();
        triggerDetecting = transform.GetChild(0).GetComponent<Transform>();
        triggerDetectingCol = transform.GetChild(0).GetComponent<SphereCollider>();

        //---------------//

        candySO.isSeeingPlayer = false;
        candySO.isDetectingPlayer = false;
        triggerDetectingCol.center = candySO.positionDetection;
        triggerDetectingCol.radius = candySO.radiusDetection;
        triggerDetectingCol.isTrigger = candySO.isTriggerDetection;

        //---------------//
    }

    void Update()
    {
        DetectingPlayer();
        //RunAwayFromPlayer();

        //-----------TEMP-----------//
        triggerDetectingCol.center = candySO.positionDetection;
        triggerDetectingCol.radius = candySO.radiusDetection;
        //-----------TEMP-----------//
    }

    void DetectingPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < maxDistance
            && Vector3.Distance(transform.position, player.position) < minDistance)
        {
            sweet.rotation = Quaternion.Slerp(transform.rotation, 
                Quaternion.LookRotation(player.position - transform.position), candySO.rotationSpeed * Time.deltaTime);
            RaycastHit whatIsInFront;
            Debug.DrawRay(transform.position, transform.forward * candySO.distanceVision, Color.blue, 0.1f);
            if (Physics.Raycast(transform.position, transform.forward, out whatIsInFront, candySO.distanceVision))
            {
                if(whatIsInFront.collider.tag == "Player")
                {
                    RunAwayFromPlayer();
                    candySO.isSeeingPlayer = true;
                }
                else
                {
                    candySO.isSeeingPlayer = false;
                }
            }
            else if (candySO.isDetectingPlayer)
            {
                sweet.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(startPosition - transform.position), candySO.rotationSpeed * Time.deltaTime);
                RunAwayFromPlayer();
            }
        }
    }
    void HidingFromPlayer()
    {

    }

    void RunAwayFromPlayer()
    {
        if(candySO.isSeeingPlayer)
        {
            sweet.position -= sweet.forward * candySO.distanceVision * Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Destroy(this);
        }
    }
}
