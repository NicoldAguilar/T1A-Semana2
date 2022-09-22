using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanController : MonoBehaviour
{
    public float velocity = 5, jumpForce = 3;
    public GameObject bulletT1, bulletT2, bulletT3; 
    bool saltos;
    public float maximoCarga, tiempoCarga;

    Rigidbody2D gravedad;
    SpriteRenderer renderi;
    Animator animador;
    Collider2D colider;

    const int ANIMATION_IDDLE = 0;
    const int ANIMATION_CORRER = 1;
    const int ANIMATION_SALTAR = 2;
    const int ANIMATION_DISPARAR = 3;
    const int ANIMATION_CARGANDO_DISPARO = 4;

    // Start is called before the first frame update
    void Start()
    {
        gravedad = GetComponent<Rigidbody2D>();
        renderi = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
        colider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            renderi.color = new Color(1, 0, 0, 1);
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            renderi.color = new Color(1, 0, 1, 1);
            var bulletPosition = new Vector3(0, 0, 0);
            if (renderi.flipX == false)
            {
                bulletPosition = transform.position + new Vector3(3, 0, 0); //para que la bala aparezca más lejos de donde estoy (+ new Vector3)
            }
            else
            {
                bulletPosition = transform.position + new Vector3(-3, 0, 0);
            }
            var gb = Instantiate(bulletT1, bulletPosition, Quaternion.identity);
            var controller = gb.GetComponent<BulletControllerMM>();
            ChangeAnimation(ANIMATION_DISPARAR);            
            if (renderi.flipX == false)
            {
                controller.SetRightDirection();                
            }
            else
            {
                controller.SetLeftDirection();
            }
        }
        //Balas
            /*if (Input.GetKey(KeyCode.X))
            {
                if (tiempoCarga < maximoCarga)
                {
                    tiempoCarga += Time.deltaTime;
                    ChangeAnimation(ANIMATION_CARGANDO_DISPARO);
                }
            }
            if (Input.GetKeyUp(KeyCode.X))
            {
                Disparar((int)tiempoCarga);
                tiempoCarga = 0;
            }*/
        else Debug.Log("No hay más balas disponibles");
        //Movimiento
        gravedad.velocity = new Vector2(0, gravedad.velocity.y);
        ChangeAnimation(ANIMATION_IDDLE);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            gravedad.velocity = new Vector2(velocity, gravedad.velocity.y);
            renderi.flipX = false;
            ChangeAnimation(ANIMATION_CORRER);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gravedad.velocity = new Vector2(-velocity, gravedad.velocity.y);
            renderi.flipX = true;
            ChangeAnimation(ANIMATION_CORRER);
        }
        //Saltar
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

    /*private void Disparar(int tiempoCarga)
    {        
        
    }*/
}
