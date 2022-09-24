using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanController : MonoBehaviour
{
    public float velocity = 5, jumpForce = 3, tiempoCarga, segundos;
    public GameObject bulletT1, bulletT2, bulletT3, balaTemporal; 
    bool saltos;

    Rigidbody2D gravedad;
    SpriteRenderer renderi;
    Animator animador;
    Collider2D colider;

    const int ANIMATION_IDDLE = 0;
    const int ANIMATION_CORRER = 1;
    const int ANIMATION_SALTAR = 2;
    const int ANIMATION_DISPARAR = 3;

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
        //Iddle
        gravedad.velocity = new Vector2(0, gravedad.velocity.y);
        ChangeAnimation(ANIMATION_IDDLE);
        //Movimiento
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

        //Disparar
        if (Input.GetKey(KeyCode.X))
        {
            tiempoCarga += Time.deltaTime;
            segundos = Mathf.Floor(tiempoCarga % 66);
            if (segundos >= 0 && segundos < 3) renderi.color = new Color(0, 1, 1, 1); //CIAN
            if (segundos >= 3 && segundos < 5) renderi.color = new Color(1, 0, 1, 1); //MORADO
            if (segundos >= 5) renderi.color = new Color(1, 1, 0, 1); //AMARILLO
            Debug.Log("Tiempo: " + segundos);
        }       

        //Disparos en tipos:
        if (Input.GetKeyUp(KeyCode.X))
        {           
            //Balas T1:
            if (segundos >=0 && segundos < 3)
            {
                renderi.color = new Color(1, 1, 1, 1);
                if (renderi.flipX == false)
                {
                    Disparos(1,true,bulletT1);
                    ChangeAnimation(ANIMATION_DISPARAR);
                }
                else
                {
                    Disparos(-1, false, bulletT1);
                    ChangeAnimation(ANIMATION_DISPARAR);
                }
                tiempoCarga = 0;
                segundos = 0;
            }
            if (segundos >= 3 && segundos < 5)
            {
                renderi.color = new Color(1, 1, 1, 1);
                if (renderi.flipX == false)
                {
                    Disparos(1, true, bulletT2);
                    ChangeAnimation(ANIMATION_DISPARAR);
                }
                else
                {
                    Disparos(-1, false, bulletT2);
                    ChangeAnimation(ANIMATION_DISPARAR);
                }
                tiempoCarga = 0;
                segundos = 0;
            }
            if (segundos >= 5)
            {
                renderi.color = new Color(1, 1, 1, 1);
                if (renderi.flipX == false)
                {
                    Disparos(1, true, bulletT3);
                    ChangeAnimation(ANIMATION_DISPARAR);
                }
                else
                {
                    Disparos(-1, false, bulletT3);
                    ChangeAnimation(ANIMATION_DISPARAR);
                }
                tiempoCarga = 0;
                segundos = 0;
            }
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

    public void Disparos(int posicion, bool vrf, GameObject bala)
    {
        var bulletPosition = new Vector3(0, 0, 0);
        bulletPosition = transform.position + new Vector3(posicion, 0, 0);
        var gb = Instantiate(bala, bulletPosition, Quaternion.identity);
        var controller = gb.GetComponent<BulletControllerMM>();
        controller.SetDirection(vrf);
    }
    
}
