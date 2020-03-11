using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasGameManager : MonoBehaviour
{
    [SerializeField]
    Text playerWeight;
    [SerializeField]
    Player player;

    int tempWeight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempWeight = (int)player.weight;
        playerWeight.text = tempWeight.ToString();
    }
}
