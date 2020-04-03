using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CandyAction : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    Transform player;
    [Space(3)]

    [Header("Sweet ScriptableObject")]
    [SerializeField]
    Sweets sweetSO;
    [Space(3)]

    Transform sweet;
    Transform triggerDetecting;
    SphereCollider triggerDetectingCol;

    //-------AI-------//
    NavMeshAgent agent;

    float delay;

    void Start()
    {
        sweet = GetComponent<Transform>();
        triggerDetecting = transform.GetChild(0).GetComponent<Transform>();
        triggerDetectingCol = transform.GetChild(0).GetComponent<SphereCollider>();

        //---------------//
        agent = GetComponent<NavMeshAgent>();
        sweetSO.isSeeingPlayer = false;
        sweetSO.isDetectingPlayer = false;
        sweetSO.isGoingToHidePoint = false;
        sweetSO.startPosition = transform.position;
        triggerDetectingCol.center = sweetSO.positionDetection;
        triggerDetectingCol.radius = sweetSO.radiusDetection;
        triggerDetectingCol.isTrigger = sweetSO.isTriggerDetection;

        //---------------//
    }

    void Update()
    {
        DetectingPlayer();
        //RunAwayFromPlayer();
        HideingFromPlayer();

        //-----------TEMP-----------//
        triggerDetectingCol.center = sweetSO.positionDetection;
        triggerDetectingCol.radius = sweetSO.radiusDetection;
        //-----------TEMP-----------//
    }

    void DetectingPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < sweetSO.maxDistanceVision
            && Vector3.Distance(transform.position, player.position) < sweetSO.minDistanceVision
            && !sweetSO.isGoingToHidePoint)
        {
            sweet.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(player.position - transform.position), sweetSO.rotationSpeed * Time.deltaTime);
            RaycastHit whatIsInFront;
            Debug.DrawRay(transform.position, transform.forward * sweetSO.distanceVision, Color.red, 0.1f);
            if (Physics.Raycast(transform.position, transform.forward, out whatIsInFront, sweetSO.distanceVision))
            {
                if (whatIsInFront.collider.tag == "Player")
                {
                    RunAwayFromPlayer();
                    sweetSO.isSeeingPlayer = true;
                }
            }
        }
        else if (Vector3.Distance(transform.position, sweetSO.startPosition) > 1f && !sweetSO.isDetectingPlayer && !sweetSO.isGoingToHidePoint && Time.time >= delay)
        {
            print("go start");

            agent.SetDestination(sweetSO.startPosition);

            sweetSO.isSeeingPlayer = false;
        }
        else
        {
            sweetSO.isSeeingPlayer = false;
        }
    }

    IEnumerator goToPosition(int delay, Vector3 pos)
    {
        print("wait 3");
        yield return new WaitForSeconds(delay);
        print("go 3");
        while (Vector3.Distance(transform.position, sweetSO.startPosition) > 1f)
        {
            agent.SetDestination(pos);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        StopCoroutine(goToPosition(3, sweetSO.startPosition));
    }

    void HideingFromPlayer()
    {
        print("go hide");
        if (sweetSO.isGoingToHidePoint)
        {
            Transform posToHide = transform.GetChild(0).GetComponent<CandyDetectingPlayer>().posHidePoint;
            if (Vector3.Distance(transform.position, posToHide.position) > 1f)
            {
                print(posToHide.position);
                agent.SetDestination(posToHide.position);

            }
            else if (Vector3.Distance(transform.position, posToHide.position) <= 0.1f)
            {
                sweetSO.isGoingToHidePoint = false;
            }
        }
    }

    void GoForward()
    {
        sweet.position += sweet.forward * sweetSO.walkSpeed * Time.deltaTime;
    }

    void RunAwayFromPlayer()
    {
        sweet.position -= sweet.forward * sweetSO.walkSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Destroy(gameObject);
        }
        if (collision.collider.tag == "HidePoint" 
            && collision.collider.gameObject.GetComponent<Transform>().position == transform.GetChild(0).GetComponent<CandyDetectingPlayer>().posHidePoint.position)
        {
            Destroy(collision.collider.gameObject);
            sweetSO.isGoingToHidePoint = false;
            delay = Time.time + 4;
        }
    }
}
