using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity = 5, jumpForce = 3, aceleracion =2;

    Rigidbody2D gravedad;
    SpriteRenderer renderi;
    Animator animador;
    Vector3 lastCheckpointPosition;

    bool saltos = true;
    bool saltos2 = true;
    bool checkpointverif = true;

    const int ANIMATION_QUIETO = 0;
    const int ANIMATION_CAMINAR = 1;
    const int ANIMATION_CORRER = 2;
    const int ANIMATION_SALTAR = 3;
    const int ANIMATION_ATACAR = 4;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ha iniciado el juego");
        gravedad = GetComponent<Rigidbody2D>();
        renderi = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        gravedad.velocity = new Vector2(0, gravedad.velocity.y);
        ChangeAnimation(ANIMATION_QUIETO);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            gravedad.velocity = new Vector2(velocity, gravedad.velocity.y);
            renderi.flipX = false;
            ChangeAnimation(ANIMATION_CAMINAR);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gravedad.velocity = new Vector2(-velocity, gravedad.velocity.y);
            renderi.flipX = true;
            ChangeAnimation(ANIMATION_CAMINAR);
        }
        if(Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.X))
        {
            gravedad.velocity = new Vector2((velocity+aceleracion), gravedad.velocity.y);
            renderi.flipX = false;
            ChangeAnimation(ANIMATION_CORRER);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.X))
        {
            gravedad.velocity = new Vector2((-velocity-aceleracion), gravedad.velocity.y);
            renderi.flipX = true;
            ChangeAnimation(ANIMATION_CORRER);
        }

        //Añadir fuerza para el salto
        if (Input.GetKeyUp(KeyCode.Space) && saltos == true)
        {
            if (saltos2 == false) //segunda vez
            {
                ChangeAnimation(ANIMATION_SALTAR);
                gravedad.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); 
                saltos = false; //salta una vez
            }
            else
            {
                ChangeAnimation(ANIMATION_SALTAR);
                gravedad.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);                
            }
            saltos2 = false;
        }

        //Atacar
        if (Input.GetKey(KeyCode.Z))
        {
            ChangeAnimation(ANIMATION_ATACAR);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        saltos = true;
        saltos2 = true;
        if (other.gameObject.tag == "DarkHole")
        {
            if (lastCheckpointPosition != null)
            {
                transform.position = lastCheckpointPosition;
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Checkpoint1" && checkpointverif == true)
        {
            Debug.Log("Checkpoint 1 funciona");
            lastCheckpointPosition = transform.position;
        }
        else if (other.gameObject.name == "Checkpoint2")
        {
            Debug.Log("Checkpoint 2 funciona");
            lastCheckpointPosition = transform.position;//guarda la ultima posición del trasform
            checkpointverif = false;
        }     
    }

    private void ChangeAnimation(int animation)
    {
        animador.SetInteger("Estado", animation);
    }
}
