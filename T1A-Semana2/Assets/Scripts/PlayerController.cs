using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity = 5, jumpForce = 3;

    Rigidbody2D gravedad;
    SpriteRenderer renderi;
    Animator animador;

    bool saltos;

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
        //Atacar 
        if (Input.GetKey(KeyCode.Z))
        {
            ChangeAnimation(ANIMATION_ATACAR);
        }

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
            gravedad.velocity = new Vector2((velocity+2), gravedad.velocity.y);
            renderi.flipX = false;
            ChangeAnimation(ANIMATION_CORRER);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.X))
        {
            gravedad.velocity = new Vector2((-velocity-2), gravedad.velocity.y);
            renderi.flipX = true;
            ChangeAnimation(ANIMATION_CORRER);
        }

        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.Z))
        {
            ChangeAnimation(ANIMATION_ATACAR);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.Z))
        {
            ChangeAnimation(ANIMATION_ATACAR);
        }
        //Añadir fuerza para el salto
        if (Input.GetKeyUp(KeyCode.Space) && saltos)
        {
            gravedad.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); //método recomendado
            ChangeAnimation(ANIMATION_SALTAR);
            saltos = false; //salta una vez
            
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        saltos = true;
    }

    private void ChangeAnimation(int animation)
    {
        animador.SetInteger("Estado", animation);
    }
}
