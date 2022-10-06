using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewNinjaController : MonoBehaviour
{
    public float velocity = 5, jumpForce = 3, tiempoCarga, segundos;
    public GameObject bullet;

    Rigidbody2D gravedad;
    SpriteRenderer renderi;
    Animator animador;

    Vector3 lastCheckpointPosition, posicionTemp;

    bool saltos, ninja = true, cambio = false;

    public GameManagerT2C gameManager;
    public AudioClip jumpSound;
    public AudioClip bulletSound;
    public AudioClip coinSound;

    const int ANIMATION_CORRER = 1;
    const int ANIMATION_SALTAR = 2;
    const int ANIMATION_MORIR = 3;
    const int ANIMATION_ATAQUE = 4;
    const int ANIMATION_QUIETO = 5;

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
        gravedad.velocity = new Vector2(velocity, gravedad.velocity.y);
    }

    public void Disparos() //getKey
    {
        cambio = true;
    }

    public void DispararTipos() //getKeyUp
    {
        //Kunai:
            renderi.color = new Color(1, 1, 1, 1);
            if (renderi.flipX == false)
            {
                Disparos(1, true, bullet);
            }
            else
            {
                Disparos(-1, false, bullet);
            }
        cambio = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        saltos = true;
    }
    private void ChangeAnimation(int animation)
    {
        animador.SetInteger("Estado", animation);
    }

    public void MoveRight()
    {
        velocity = 5;
        renderi.flipX = false;
        ChangeAnimation(ANIMATION_CORRER);
    }
    public void MoveLeft()
    {
        velocity = -5;
        renderi.flipX = true;
        ChangeAnimation(ANIMATION_CORRER);
    }
    public void NotMoving()
    {
        velocity = 0;
        ChangeAnimation(ANIMATION_QUIETO);
    }

    public void Jumping()
    {
        gravedad.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        saltos = false;
    }

    public void KeyboardMovements()
    {
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

    public void AtaqueKatana(int posicion, bool vrf, GameObject Katana)
    {
        var katanaPosicion = new Vector3(0, 0, 0);
        katanaPosicion = transform.position + new Vector3(1, 0, 0);
        var gb = Instantiate(Katana, katanaPosicion, Quaternion.identity);
        ChangeAnimation(ANIMATION_ATAQUE);
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