using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixEmbers : MonoBehaviour
{
    public float pixcontador;
    public float pixduracion;
    public GameObject pixpantallanegra;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        pixcontador += Time.deltaTime;
        if(pixcontador>=pixduracion){
            pixpantallanegra.SetActive(false);
        }
        
    }
}
