using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircuitMode : MonoBehaviour
{
    [SerializeField]
    Transform StartPoint;
    [SerializeField]
    Transform EndPoint;

    [SerializeField]
    GameObject player;

    //-------UI-------//
    [SerializeField]
    public GameObject textTimer;
    [SerializeField]
    GameObject alertMessage;
    [SerializeField]
    GameObject timerAlert;
    //----------------//

    float timerToStart;
    [SerializeField]
    float timeForGame;
    float timeAlert;

    [SerializeField]
    GameObject[] sweets;

    public bool startMiniGame;
    public bool isMiniGame;
    bool isGoingOut;


    void Start()
    {
        textTimer.SetActive(false);
        alertMessage.SetActive(false);
        timerAlert.SetActive(false);
        ChangeStateText(new Vector2(1629.8f, 424.58f), new Vector2(960f, -540.5f), 300);
        startMiniGame = false;
        isGoingOut = false;
        timerToStart = 3f;
        timeAlert = 5f;
        for (int i = 0; i < sweets.Length; i++)
        {
            sweets[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startMiniGame && !isMiniGame)
        {
            showStartTimer();
        }
        else if (!startMiniGame)
        {
            timerToStart = 3f;
        }
        if (isMiniGame)
        {
            ChangeStateText(new Vector2(482.9f, 125.8f), new Vector2(305f, -108f), 100);
            Timer();
        }
        else if (!isMiniGame)
        {
            ChangeStateText(new Vector2(1629.8f, 424.58f), new Vector2(960f, -540.5f), 300);
            timeForGame = 120f;
        }
        goingOutTheGame();
    }

    public IEnumerator startGame()
    {
        textTimer.SetActive(true);
        player.GetComponent<PlayerAction>().enabled = false;
        yield return new WaitForSeconds(3);
        player.GetComponent<PlayerAction>().enabled = true;
        startMiniGame = false;
        isMiniGame = true;
    }

    void showStartTimer()
    {
        textTimer.GetComponent<Text>().text = timerToStart.ToString("F0");
        timerToStart -= Time.deltaTime;
    }

    void Timer()
    {
        textTimer.GetComponent<Text>().text = timeForGame.ToString("F0");
        timeForGame -= Time.deltaTime;
        //print(timeForGame);
        if (timeForGame <= 0)
        {
            isMiniGame = false;
            textTimer.SetActive(false);
        }
    }

    void goingOutTheGame()
    {
        if (isGoingOut)
        {
            timeAlert -= Time.deltaTime;
            timerAlert.GetComponent<Text>().text = timeAlert.ToString("F0");
            if (timeAlert <= 0)
            {
                isMiniGame = false;
                textTimer.SetActive(false);
                timerAlert.SetActive(false);
                alertMessage.SetActive(false);
                isGoingOut = false;
                timeAlert = 5f;
                Destroy(transform.gameObject, 0.2f);
            }
        }
    }

    public void ChangeStateText(Vector2 size, Vector2 position, int fontSize)
    {
        textTimer.GetComponent<Text>().rectTransform.sizeDelta = size;
        textTimer.GetComponent<Text>().rectTransform.anchoredPosition = position;
        textTimer.GetComponent<Text>().fontSize = fontSize;
    }

    public void spawnSweetsPrize()
    {
        for (int i = 0; i < sweets.Length; i++)
        {
            sweets[i].SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isMiniGame)
        {
            if (other.tag == "Player")
            {
                timerAlert.SetActive(false);
                alertMessage.SetActive(false);
                isGoingOut = false;
                timeAlert = 5f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isMiniGame)
        {
            if (other.tag == "Player")
            {
                timerAlert.SetActive(true);
                alertMessage.SetActive(true);
                isGoingOut = true;
            }
        }
    }
}
