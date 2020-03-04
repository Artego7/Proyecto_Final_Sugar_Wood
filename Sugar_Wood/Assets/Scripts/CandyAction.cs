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
    Transform triggerSeeing;
    BoxCollider triggerSeeingCol;

    Transform triggerDetecting;
    SphereCollider triggerDetectingCol;

    [Header("Sweet ScriptableObject")]
    [SerializeField]
    Sweets candySO;

    // Start is called before the first frame update
    void Start()
    {
        sweet = GetComponent<Transform>();
        triggerSeeing = transform.GetChild(0).GetComponent<Transform>();
        triggerSeeingCol = transform.GetChild(0).GetComponent<BoxCollider>();
        triggerDetecting = transform.GetChild(1).GetComponent<Transform>();
        triggerDetectingCol = transform.GetChild(1).GetComponent<SphereCollider>();

        //---------------//

        candySO.isSeeingPlayer = false;
        candySO.isDetectingPlayer = false;
        triggerSeeingCol.center = candySO.positionVision;
        triggerSeeingCol.size = candySO.sizeVision;
        triggerSeeingCol.isTrigger = candySO.isTriggerVision;
        triggerDetectingCol.center = candySO.positionDetection;
        triggerDetectingCol.radius = candySO.radiusDetection;
        triggerDetectingCol.isTrigger = candySO.isTriggerDetection;

        //---------------//
    }

    // Update is called once per frame
    void Update()
    {
        DetectingPlayer();
        FollowPlayer();
    }


    void DetectingPlayer()
    {
        if (candySO.isDetectingPlayer)
        {
            sweet.rotation = Quaternion.Slerp(triggerSeeing.rotation, Quaternion.LookRotation(player.position - triggerSeeing.position), candySO.rotationSpeed * Time.deltaTime);
            RaycastHit whatIsInFront;
            Debug.DrawRay(triggerSeeing.position, triggerSeeing.forward * candySO.distanceVision, Color.blue, 0.1f);
            if (Physics.Raycast(triggerSeeing.position, triggerSeeing.forward, out whatIsInFront, candySO.distanceVision))
            {
                if(whatIsInFront.collider.tag == "Player")
                {
                    candySO.isSeeingPlayer = true;
                }
                else
                {
                    candySO.isSeeingPlayer = false;
                }
            }
        }
    }

    void FollowPlayer()
    {
        if(candySO.isDetectingPlayer && candySO.isSeeingPlayer)
        {
            sweet.position += sweet.forward * candySO.distanceVision * Time.deltaTime;
        }
    }
}
