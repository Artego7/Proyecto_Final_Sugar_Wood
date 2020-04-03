using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class TextToShow : MonoBehaviour
{
    //-------------------//
    [SerializeField]
    Text textShowed;
    [SerializeField]
    int textWanted;
    //-------------------//
    [SerializeField]
    string txtFileName;

    string[] line;
    string path = "TextToShow/TutorialText/";
    //-------------------//


    void Start()
    {
        ReadString();
    }

    void ReadString()
    {
        string contenido = Resources.Load(path + txtFileName).ToString();
        line = contenido.Split('\n');

        //for (int i = 0; i < line.Length; i++)
        //{
        //    print(line[i]);
        //}

    }
    public void ShowSelectedText()
    {
        textShowed.text = line[textWanted];
        textShowed.gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ShowSelectedText();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            textShowed.gameObject.SetActive(false);
        }
    }

}