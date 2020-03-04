using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCharacter : MonoBehaviour
{
    //MOVIMIENTO
    public float walkspeed;
    public float runspeed;

    public GameObject anim;

    //ESCALAR
    public bool climb=false;

    //SALTO
    
    public Vector3 jump;
    public float jumpforce;

    bool canjump;
    Rigidbody rb;

    //PESO

    public float weight;
    public GameObject candy01;
    public GameObject candy02;
    public GameObject candy03;
    public GameObject muffin;
    public GameObject cake;

    //PUERTA
    //public GameObject door;
    //public bool canopendoor;

    public GameObject weightOK;
    public GameObject weightNO;

    //PARA PERDER PESO
    public float counter;
    public float timecounter;

    //ARENAS MOVEDIZAS
    bool sand=false;

    //CANVAS
    public GameObject weighttext; //HUD
    public GameObject pauseCanvas; //menu de pausa
    public GameObject weightNotEnough; //menu de báscula
    public float countdown;
    public GameObject countdownText;
    public int countdownFinal;
    public GameObject mensajeInicio;
    public GameObject mensajeCuidado;
    float contadorCanvas;
    float duracionCanvas=5f;
    bool mensajeTutorial;
    

    //SIN TIEMPO
    public GameObject timeOut;
    public bool necesitousarlo;

    //BOTÓN
    public GameObject hiddencandy;
    bool showcandy = false;
    public float candytime;
    

    // Start is called before the first frame update
    void Start()
    {

    rb=GetComponent<Rigidbody>();  
    
    jump = new Vector3(0.0f, 2.0f, 0.0f);  
    
    weight = 50f;

    }

    // Update is called once per frame
    void Update()
    {

    //CANVAS

    weighttext.GetComponent<Text>().text = weight.ToString() + " / 80 kg"; //peso en pantalla

    countdown -= Time.deltaTime; //cuenta atrás en pantalla
    countdownFinal = Mathf.RoundToInt(countdown); //convertimos el float en int para que no aparezcan decimales en pantalla
    countdownText.GetComponent<Text>().text = countdownFinal.ToString(); //se convierte en texto

    if(mensajeTutorial){
    contadorCanvas+=Time.deltaTime;
        if(contadorCanvas>=duracionCanvas){
            mensajeInicio.SetActive(false);
            mensajeCuidado.SetActive(false);
            mensajeTutorial=false;
            contadorCanvas=0f;
        }
    }

    //SIN TIEMPO
    if(necesitousarlo){
        if(countdown <= 0){
            timeOut.SetActive(true);
            Time.timeScale=0f;
            countdownText.SetActive(false);
            countdown=120;
        }
    }

    //MENU PAUSA

    if(Input.GetKeyDown(KeyCode.Escape)){
        pauseCanvas.SetActive(true);
        Time.timeScale=0f;
        
    }


    //PARA QUE LOS DULCES DEL BOTÓN DESAPAREZCAN CON EL TIEMPO
    
    if(showcandy){
    counter += Time.deltaTime; 
       if (counter >= candytime){
            hiddencandy.SetActive(false);
            counter = 0.0f;
       }   
    }
    
    }

    void FixedUpdate(){

    //MOVIMIENTO

    if (!climb){
        
        anim.GetComponent<Animator>().SetBool("Climb",false);

        if(Input.GetKey(KeyCode.W)){
            //rb.velocity=transform.forward*walkspeed;   //multiplica la velocidad por el eje del personaje (no el del mundo)
            rb.velocity=new Vector3(transform.forward.x*walkspeed,rb.velocity.y,transform.forward.z*walkspeed); //descomponemos el vector forward para poder modificar el valor de y para que caiga el personaje
            anim.GetComponent<Animator>().SetBool("Walk",true);

            if(Input.GetKey(KeyCode.LeftShift)){
                rb.velocity=new Vector3(transform.forward.x*runspeed,rb.velocity.y,transform.forward.z*runspeed); 
                anim.GetComponent<Animator>().SetBool("Run",true);
                counter += Time.deltaTime; 
                if (counter >= timecounter){
                    weight -= 2f;
                    counter = 0.0f;  
                }   
            }else{
                anim.GetComponent<Animator>().SetBool("Run",false);
                counter = 0f;
            }
        }else{
            anim.GetComponent<Animator>().SetBool("Walk",false);
            rb.velocity=new Vector3(transform.forward.x*0,rb.velocity.y,transform.forward.z*0); // le indicamos que la velocidad es 0 para evitar que el personaje "patine"
        }

        if(Input.GetKey(KeyCode.S)){
            rb.velocity=new Vector3(transform.forward.x*-walkspeed,rb.velocity.y,transform.forward.z*-walkspeed); 
            anim.GetComponent<Animator>().SetBool("Back",true);   
            walkspeed = 1f;
        }else{
            anim.GetComponent<Animator>().SetBool("Back",false);
            walkspeed = 3f;
        }

        if (Input.GetKey(KeyCode.D)){
            transform.Rotate(0,2f,0);   
        }else{
            if(Input.GetKey(KeyCode.A)){
                transform.Rotate(0,-2f,0);
            }
        }

    }else{ //ESCALAR
           
            if(Input.GetKey(KeyCode.W)){
                rb.velocity=new Vector3(rb.velocity.x,transform.up.y*walkspeed,rb.velocity.z);
                anim.GetComponent<Animator>().SetBool("Climb",true);
                walkspeed = 1.5f;
                counter += Time.deltaTime; 
                if (counter >= timecounter){
                    weight -= 5f;
                    counter = 0.0f; 
                }    
            }else{
                anim.GetComponent<Animator>().SetBool("Climb",false);
                walkspeed = 3f;
            }
        
    }
    
    //SALTO
    if (canjump){   
        if (Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(jump * jumpforce, ForceMode.Impulse);
            //anim.GetComponent<Animator>().SetBool("Jump",true);
            canjump = false;
        }
    }else{
        //anim.GetComponent<Animator>().SetBool("Jump",false);
        //canjump = true;
    }

    //ARENAS MOVEDIZAS
    if (sand){
        walkspeed=1f;
        runspeed=3f;
        counter += Time.deltaTime;
        if (counter >= timecounter){
            weight -= 3f;
            counter = 0.0f; 
        }
    }else{
        walkspeed=3f;
        runspeed=6f;
    }   


    }

void OnTriggerEnter (Collider col){
        
        if(col.tag=="cake"){
            col.gameObject.SetActive(false);
            weight = weight + 10;
        }

        if(col.tag=="muffin"){
            col.gameObject.SetActive(false);
            weight = weight + 5;
        }

        if(col.tag=="candy"){
            col.gameObject.SetActive(false);
            weight = weight + 1;
        }

        if(col.tag=="bascula"){ //si llegamos al objetivo del nivel nos deja pasar al siguiente
            if(weight>=80){
                //door.transform.Rotate(0,80,0);
                weightNO.SetActive(false);
                weightOK.SetActive(true);
                SceneManager.LoadScene(2);
            }else{
                weightNotEnough.SetActive(true);
                Time.timeScale=0f;
            }
        }

        if(col.tag=="bascula_01"){ //si llegamos al objetivo del nivel nos deja pasar al siguiente
            if(weight>=80){
                //door.transform.Rotate(0,80,0);
                weightNO.SetActive(false);
                weightOK.SetActive(true);
                SceneManager.LoadScene(0);
            }else{
                weightNotEnough.SetActive(true);
                Time.timeScale=0f;
            }
        }

        if(col.tag=="wall"){ //escalar
            climb=true;   
        }

        if (col.tag=="sand"){ //entrar en las arenas movedizas
            sand=true;
        }

        if (col.tag=="button"){
            hiddencandy.SetActive(true);
            showcandy = true;
            counter=0.0f;
        }

        if (col.tag=="inicio"){
            mensajeTutorial=true;
            mensajeInicio.SetActive(true);
        }

        if (col.tag=="cuidado"){
            mensajeTutorial=true;
            mensajeCuidado.SetActive(true);

        }

        
    }



void OnTriggerExit (Collider tri){ //dejar de escalar
    if (tri.tag=="wall"){
        climb=false;
    }

    if (tri.tag=="sand"){ //salir de las arenas movedizas
        sand=false;
    }

}


void OnCollisionEnter (Collision col){
  if (col.collider.tag=="floor" || col.collider.tag=="platform" || col.collider.tag=="button"){
        canjump=true; //evitar el salto múltiple
        //anim.GetComponent<Animator>().SetBool("IsInAir?",false);
  }

  }  

void OnCollisionExit (Collision col){
    if (col.collider.tag=="floor"){
    //anim.GetComponent<Animator>().SetBool("IsInAir?",true);
    }
}
}

