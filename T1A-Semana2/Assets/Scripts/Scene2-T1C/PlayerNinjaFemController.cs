using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNinjaFemController : MonoBehaviour
{
    public float velocity = 5, jumpForce = 3;
    public GameObject bullet;

    Rigidbody2D gravedad;
    SpriteRenderer renderi;
    Animator animador;

    bool saltos;

    public GameManagerControllerNF gameManager;

    const int ANIMATION_QUIETO = 0;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            var bulletPosition = new Vector3(0, 0, 0);
            bulletPosition = transform.position + new Vector3(3, 0, 0);
            var gb = Instantiate(bullet, bulletPosition, Quaternion.identity);
            var controller = gb.GetComponent<BulletControllerNF>();
            controller.SetRightDirection();
        }
        gravedad.velocity = new Vector2(0, gravedad.velocity.y);
        ChangeAnimation(ANIMATION_QUIETO);

        gravedad.velocity = new Vector2(velocity, gravedad.velocity.y);
        renderi.flipX = false;
        ChangeAnimation(ANIMATION_CORRER);

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
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Enemigo")
        {
            gameManager.PerderVida();
            if (gameManager.lives == 0)
            {                
                Debug.Log("Estas muerto");
                ChangeAnimation(ANIMATION_MORIR);
                Destroy(this.gameObject);
            }
        }
    }

    private void ChangeAnimation(int animation)
    {
        //Estado en 1 = pasa de iddle a correr
        //Estado en 0 = De correr a iddle
        animador.SetInteger("Estado", animation);
    }
}
