using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayer : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (!transform.parent.GetComponent<CircuitMode>().isMiniGame)
        {
            if (other.tag == "Player")
            {
                transform.parent.GetComponent<CircuitMode>().startMiniGame = true;
                StartCoroutine(transform.parent.GetComponent<CircuitMode>().startGame());
            }
        }
    }
}
