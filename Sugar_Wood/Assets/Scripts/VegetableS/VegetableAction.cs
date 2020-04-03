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

    void Start()
    {
        vegetable = GetComponent<Transform>();
        triggerDetecting = transform.GetChild(0).GetComponent<Transform>();
        triggerDetectingCol = transform.GetChild(0).GetComponent<SphereCollider>();
        vegetableSO.startPosition = transform.position;
        vegetableSO.isDetectingPlayer = false;
        triggerDetectingCol.center = vegetableSO.positionDetection;
        triggerDetectingCol.radius = vegetableSO.radiusDetection;
        triggerDetectingCol.isTrigger = vegetableSO.isTriggerDetection;

    }

    void Update()
    {
        DetectingPlayer();
        //FollowPlayer();

    }

    void DetectingPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < vegetableSO.maxDistanceVision
            && Vector3.Distance(transform.position, player.position) < vegetableSO.minDistanceVision)
        {
            vegetable.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(player.position - transform.position), vegetableSO.rotationSpeed * Time.deltaTime);
            RaycastHit whatIsInFront;
            Debug.DrawRay(transform.position, transform.forward * vegetableSO.distanceVision, Color.red, 0.1f);
            if (Physics.Raycast(transform.position, transform.forward, out whatIsInFront, vegetableSO.distanceVision))
            {
                if (whatIsInFront.collider.tag == "Player")
                {
                    GoForward();
                }
            }
        }
        else if (!vegetableSO.isDetectingPlayer)
        {
            StartCoroutine(GoToStartPoint());
        }
    }

    IEnumerator GoToStartPoint()
    {
        yield return new WaitForSeconds(2);
        if (Vector3.Distance(transform.position, vegetableSO.startPosition) > 1f)
        {
            vegetable.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(vegetableSO.startPosition - transform.position), vegetableSO.rotationSpeed * Time.deltaTime);
            GoForward();
        }
    }

    void GoForward()
    {
        vegetable.position += vegetable.forward * vegetableSO.walkSpeed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Destroy(this);
        }
    }
}
