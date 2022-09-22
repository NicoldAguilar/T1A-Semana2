using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerNinjaFemController : MonoBehaviour
{
    public float velocity = 5, jumpForce = 3;
    public GameObject bullet;

    Rigidbody2D gravedad;
    SpriteRenderer renderi;
    Animator animador;
    Collider2D colider;
    AudioSource audioSource;

    Vector3 lastCheckpointPosition, posicionTemp;

    bool saltos, ninja=true, checkpointverif = true, guardado = false;

    public GameManagerControllerNF gameManager;
    public AudioClip jumpSound;
    public AudioClip bulletSound;
    public AudioClip coinSound;

    const int ANIMATION_CORRER = 1;
    const int ANIMATION_SALTAR = 2;
    const int ANIMATION_MORIR = 3;    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ha iniciado el juego");
        gravedad = GetComponent<Rigidbody2D>();
        renderi = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
        colider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
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
            if (Input.GetKeyUp(KeyCode.X))
            {
                if (gameManager.balas > 0)
                {
                    var bulletPosition = new Vector3(0, 0, 0);
                    if (renderi.flipX == false)
                    {
                        bulletPosition = transform.position + new Vector3(3, 0, 0); //para que la bala aparezca más lejos de donde estoy (+ new Vector3)
                    }
                    else
                    {
                        bulletPosition = transform.position + new Vector3(-3, 0, 0);
                    }
                    var gb = Instantiate(bullet, bulletPosition, Quaternion.identity);
                    var controller = gb.GetComponent<BulletControllerNF>();
                    gameManager.BalasRestantes(1);
                    if (renderi.flipX == false)
                    {
                        controller.SetRightDirection();
                    }
                    else
                    {
                        controller.SetLeftDirection();
                    }
                    audioSource.PlayOneShot(bulletSound);
                }
                else Debug.Log("No hay más balas disponibles");
            }

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
                audioSource.PlayOneShot(jumpSound);
            }

        }

        if (gameManager.lives == 0)
        {
            ChangeAnimation(ANIMATION_MORIR);
            Debug.Log("Estas muerto");
            ninja = false;
            colider.enabled = false;
            Destroy(gravedad);          
        }
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
            gameManager.SaveGame();

        }
        //Cambiar escena
        if(other.gameObject.name == "Caja")
        {
            SceneManager.LoadScene(GameManagerControllerNF.SCENE_T1C);
        }
        //Resetear todo a 0
        if(other.gameObject.name == "Cactus")
        {
            gameManager.empezarCero();
            gameManager.LoadGame();
        }
    }

    private void ChangeAnimation(int animation)
    {
        //Estado en 1 = pasa de iddle a correr
        //Estado en 0 = De correr a iddle
        animador.SetInteger("Estado", animation);
    }
}
