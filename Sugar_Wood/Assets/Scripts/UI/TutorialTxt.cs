using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class TutorialTxt : MonoBehaviour
{
    //-------------------//
    [SerializeField]
    Text textShowed;
    //-------------------//
    [SerializeField]
    string txtFileName;

    string[] line;
    string path = "Assets/Resources/TextToShow/TutorialText/";
    //-------------------//


    void Start()
    {
        ReadString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [MenuItem("Tools/Write file")]
    void WriteString()
    {
        //string path = "Assets/Resources/TextToShow/TutorialText/TutorialText.txt";

        ////Write some text to the test.txt file
        //StreamWriter writer = new StreamWriter(path, true);
        //writer.WriteLine("TutorialText");
        //writer.Close();

        ////Re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset(path);
        //TextAsset asset = Resources.Load("TutorialText");

        ////Print the text from the file
        //Debug.Log(asset.text);
    }

    [MenuItem("Tools/Read file")]
    void ReadString()
    {
        StreamReader reader = new StreamReader(path + txtFileName);
        string contenido = reader.ReadToEnd();
        line = contenido.Split('\n');

        for (int i = 0; i < line.Length; i++)
        {
            print(line[i]);
        }
        reader.Close();
    }
    public void ShowSelectedText(int i)
    {
        textShowed.text = line[i];
        textShowed.gameObject.SetActive(true);
}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "button")
        {
            ShowSelectedText(0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "button")
        {
            textShowed.gameObject.SetActive(false);
        }
    }

}
