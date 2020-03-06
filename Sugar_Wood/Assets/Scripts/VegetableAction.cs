using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableAction: MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    Transform player;
    [Space(3)]

    Transform vegetable;
    Transform triggerSeeing;
    BoxCollider triggerSeeingCol;

    Transform triggerDetecting;
    SphereCollider triggerDetectingCol;

    [Header("Vegetable ScriptableObject")]
    [SerializeField]
    Vegetables vegetableSO;

    void Start()
    {
        vegetable = GetComponent<Transform>();
        triggerSeeing = transform.GetChild(0).GetComponent<Transform>();
        triggerSeeingCol = transform.GetChild(0).GetComponent<BoxCollider>();
        triggerDetecting = transform.GetChild(1).GetComponent<Transform>();
        triggerDetectingCol = transform.GetChild(1).GetComponent<SphereCollider>();

        //---------------//

        vegetableSO.isSeeingPlayer = false;
        vegetableSO.isDetectingPlayer = false;
        triggerSeeingCol.center = vegetableSO.positionVision;
        triggerSeeingCol.size = vegetableSO.sizeVision;
        triggerSeeingCol.isTrigger = vegetableSO.isTriggerVision;
        triggerDetectingCol.center = vegetableSO.positionDetection;
        triggerDetectingCol.radius = vegetableSO.radiusDetection;
        triggerDetectingCol.isTrigger = vegetableSO.isTriggerDetection;

        //---------------//
    }

    void Update()
    {
        DetectingPlayer();
        FollowPlayer();
    }

    void DetectingPlayer()
    {
        if (vegetableSO.isDetectingPlayer)
        {
            vegetable.rotation = Quaternion.Slerp(triggerSeeing.rotation, 
                Quaternion.LookRotation(player.position - triggerSeeing.position), vegetableSO.rotationSpeed * Time.deltaTime);
            RaycastHit whatIsInFront;
            Debug.DrawRay(triggerSeeing.position, triggerSeeing.forward * vegetableSO.distanceVision, Color.blue, 0.1f);
            if (Physics.Raycast(triggerSeeing.position, triggerSeeing.forward, out whatIsInFront, vegetableSO.distanceVision))
            {
                if(whatIsInFront.collider.tag == "Player")
                {
                    vegetableSO.isSeeingPlayer = true;
                }
                else
                {
                    vegetableSO.isSeeingPlayer = false;
                }
            }
        }
    }

    void FollowPlayer()
    {
        if(vegetableSO.isDetectingPlayer && vegetableSO.isSeeingPlayer)
        {
            vegetable.position += vegetable.forward * vegetableSO.distanceVision * Time.deltaTime;
        }
    }
}
