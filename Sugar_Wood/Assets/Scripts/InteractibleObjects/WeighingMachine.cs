using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeighingMachine : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    GameObject puente;
    [SerializeField]
    GameObject puente2;

    [SerializeField]
    int maxWeight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            if(player.weight >= maxWeight)
                puente.GetComponent<Bridge>().puenteActivo = true;
                puente2.GetComponent<Bridge>().puenteActivo = true;
            print("end game");
        }
    }
}
