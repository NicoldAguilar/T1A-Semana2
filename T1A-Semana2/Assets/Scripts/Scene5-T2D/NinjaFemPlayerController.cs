using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class NinjaFemPlayerController : MonoBehaviour
{
    public float velocity = 5, jumpForce = 3, tiempoCarga, segundos;
    public GameObject bullet;
    public GameObject katana;

    Rigidbody2D gravedad;
    SpriteRenderer renderi;
    Animator animador;
    Collider2D colider;
    AudioSource audioSource;

    Vector3 lastCheckpointPosition, posicionTemp;

    bool saltos, ninja = true, cambio = false, checkpointverif = true, guardado = false;

    public GameManagerT2D gameManager;
    public AudioClip jumpSound;
    public AudioClip bulletSound;
    public AudioClip coinSound;


    const int ANIMATION_CORRER = 1;
    const int ANIMATION_SALTAR = 2;
    const int ANIMATION_MORIR = 3;
    const int ANIMATION_ATAQUE = 4;
    const int ANIMATION_QUIETO = 5;

    public TMP_Text texto;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ha iniciado el juego");
        gravedad = GetComponent<Rigidbody2D>();
        renderi = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
        colider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        gameManager.LoadGameT2();
        texto.text = "Katana";
    }

    // Update is called once per frame
    void Update()
    {
        //Guardar Checkpoint
        if (guardado == false && gameManager.tempx != 0 && gameManager.tempy != 0)
        {
            transform.position = new Vector2((float)gameManager.tempx, (float)gameManager.tempy);
            guardado = true;
        }

        if (ninja == true)
        {
            gravedad.velocity = new Vector2(velocity, gravedad.velocity.y);
        }
        if (gameManager.lives == 0)
        {
            ChangeAnimation(ANIMATION_MORIR);
            Debug.Log("Estas muerto");
            ninja = false;
            colider.enabled = false;
            Destroy(gravedad);
        }

        //gravedad.velocity = new Vector2(1, gravedad.velocity.y);
        //KeyboardMovements();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        saltos = true;
        if (other.gameObject.tag == "DarkHole")
        {
            if (lastCheckpointPosition != null)
            {
                transform.position = lastCheckpointPosition;
            }
        }
        if (other.gameObject.tag == "Enemigo")
        {
            gameManager.PerderVida();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Monedas
        if (other.gameObject.tag == "GoldCoin")
        {
            gameManager.GanaMonedasT1(1);
            gameManager.GanaPuntos(5);
            audioSource.PlayOneShot(coinSound);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "SilverCoin")
        {
            gameManager.GanaMonedasT2(1);
            gameManager.GanaPuntos(10);
            audioSource.PlayOneShot(coinSound);
            Destroy(other.gameObject);
        }
        //Checkpoints
        if (other.gameObject.name == "Checkpoint1" && checkpointverif == true)
        {
            Debug.Log("Checkpoint 1 funciona");
            lastCheckpointPosition = transform.position;
        }
        else if (other.gameObject.name == "Checkpoint2")
        {
            Debug.Log("Checkpoint 2 funciona");
            lastCheckpointPosition = transform.position;//guarda la ultima posición del trasform
            checkpointverif = false;
            var x = transform.position.x;
            var y = transform.position.y;
            gameManager.GuardarPosicionPartida(x, y); //que aparezca en el ultimo checkpont guardado
            gameManager.SaveGameT2();

        }
        //Cambiar escena
        /*if (other.gameObject.name == "Caja")
        {
            SceneManager.LoadScene(GameManagerControllerNF.SCENE_T1C);
        }
        //Resetear todo a 0
        if (other.gameObject.name == "Cactus")
        {
            gameManager.empezarCeroT2();
            gameManager.LoadGameT2();
        }*/
    }

    public void AccionAtacar()
    {
        if (cambio == true)
        {
            if (gameManager.balas > 0)
                DispararKunais();
            else Debug.Log("No hay más balas disponibles");
        }
        else
        {
            Katana();
        }
    }

    public void CambioArma()
    {
        cambio = !cambio; //invierte
        if(texto.text == "Katana")
        {
            texto.text = "Kunai";
        }
        else
        {
            texto.text = "Katana";
        }
    }

    public void DispararKunais() //getKeyUp
    {
        //Kunai:
        gameManager.BalasRestantes(1);
        renderi.color = new Color(1, 1, 1, 1);
        if (renderi.flipX == false)
        {
            CrearBala(1, true, bullet);
        }
        else
        {
            CrearBala(-1, false, bullet);
        }
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
        if (saltos == true)
        {
            gravedad.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            ChangeAnimation(ANIMATION_SALTAR);
            saltos = false;
        }
    }

    public void KeyboardMovements()
    {
        //Iddle
        gravedad.velocity = new Vector2(0, gravedad.velocity.y);
        ChangeAnimation(ANIMATION_QUIETO);

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
    public void CrearBala(int posicion, bool vrf, GameObject bala)
    {
        var bulletPosition = new Vector3(0, 0, 0);
        bulletPosition = transform.position + new Vector3(posicion, 0, 0);
        var gb = Instantiate(bala, bulletPosition, Quaternion.identity);
        var controller = gb.GetComponent<BulletControllerMM>();
        controller.SetDirection(vrf);
    }

    public void Katana()
    {
        ChangeAnimation(ANIMATION_ATAQUE);
        var katanaPosition = new Vector3(transform.position.x+1, transform.position.y, 0);
        var gb = Instantiate(katana, katanaPosition, Quaternion.identity);       
    }
}
