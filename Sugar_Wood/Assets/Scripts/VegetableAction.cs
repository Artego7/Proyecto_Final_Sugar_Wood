using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableAction : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    Transform player;
    [Space(3)]

    Transform vegetable;

    Transform triggerDetecting;
    SphereCollider triggerDetectingCol;

    [Header("Vegetable ScriptableObject")]
    [SerializeField]
    Vegetables vegetableSO;

    //-------TEMP-------//
    float maxDistance = 50f;
    float minDistance = 25f;
    Vector3 startPosition;
    //-------TEMP-------//

    void Start()
    {
        vegetable = GetComponent<Transform>();
        triggerDetecting = transform.GetChild(0).GetComponent<Transform>();
        triggerDetectingCol = transform.GetChild(0).GetComponent<SphereCollider>();
        startPosition = transform.position;

        //---------------//

        vegetableSO.isSeeingPlayer = false;
        vegetableSO.isDetectingPlayer = false;
        triggerDetectingCol.center = vegetableSO.positionDetection;
        triggerDetectingCol.radius = vegetableSO.radiusDetection;
        triggerDetectingCol.isTrigger = vegetableSO.isTriggerDetection;
        //vegetable.startPosition = startPosition;
        //---------------//
    }

    void Update()
    {
        DetectingPlayer();
        //FollowPlayer();
        //-----------TEMP-----------//
        triggerDetectingCol.center = vegetableSO.positionDetection;
        triggerDetectingCol.radius = vegetableSO.radiusDetection;
        //-----------TEMP-----------//
    }

    void DetectingPlayer()
    {
        //if (vegetableSO.isDetectingPlayer)
        if (Vector3.Distance(transform.position, player.position) < maxDistance
            && Vector3.Distance(transform.position, player.position) < minDistance)
        {
            vegetable.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(player.position - transform.position), vegetableSO.rotationSpeed * Time.deltaTime);
            RaycastHit whatIsInFront;
            Debug.DrawRay(transform.position, transform.forward * vegetableSO.distanceVision, Color.blue, 0.1f);
            if (Physics.Raycast(transform.position, transform.forward, out whatIsInFront, vegetableSO.distanceVision))
            {
                vegetable.position += vegetable.forward * vegetableSO.walkSpeed * Time.deltaTime;
                if (whatIsInFront.collider.tag == "Player")
                {
                    vegetableSO.isSeeingPlayer = true;
                }
                else
                {
                    vegetableSO.isSeeingPlayer = false;
                }
            }
            else
            {
                vegetable.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(startPosition - transform.position), vegetableSO.rotationSpeed * Time.deltaTime);
            }
        }
    }

    void FollowPlayer()
    {
        if (vegetableSO.isSeeingPlayer)
        {
            vegetable.position += vegetable.forward * vegetableSO.walkSpeed * Time.deltaTime;
        }
    }
}
