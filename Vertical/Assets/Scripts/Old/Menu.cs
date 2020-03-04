using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject PauseCanvas;
    public GameObject WeightCanvas;

    public GameObject credits;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play (){
        SceneManager.LoadScene(1);
        Time.timeScale=1f;
    }

    public void RestartTutorial(){
        SceneManager.LoadScene(1);
        Time.timeScale=1f;
    }

    public void RestartLevel01(){
        SceneManager.LoadScene(2);
        Time.timeScale=1f;
    }

    public void Resume(){
        PauseCanvas.SetActive(false);
        WeightCanvas.SetActive(false);
        Time.timeScale=1f;
    }

    public void ExitGame(){
        SceneManager.LoadScene(0);
        Time.timeScale=1f;
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void Credits(){
        credits.SetActive(true);
    }

    public void Back(){
        SceneManager.LoadScene(0);
    }

}
