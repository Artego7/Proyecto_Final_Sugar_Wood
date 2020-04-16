using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCircuit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.parent.GetComponent<CircuitMode>().ChangeStateText(new Vector2(1629.8f, 424.58f), new Vector2(960f, -540.5f), 300);
            transform.parent.GetComponent<CircuitMode>().isMiniGame = false;
            transform.parent.GetComponent<CircuitMode>().textTimer.SetActive(false);
            transform.parent.GetComponent<CircuitMode>().spawnSweetsPrize();
            Destroy(transform.parent.gameObject, 0.2f);
        }
    }
}
