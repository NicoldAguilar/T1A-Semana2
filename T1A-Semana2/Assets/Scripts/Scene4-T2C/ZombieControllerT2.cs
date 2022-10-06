using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControllerT2 : MonoBehaviour
{
    public int velocity = -2, vidas = 2;
    Rigidbody2D rb;
    Collider2D c;

    public GameManagerT2D gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        c = GetComponent<Collider2D>();
        gameManager = FindObjectOfType<GameManagerT2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocity, rb.velocity.y);
        if (vidas <= 0)
        {
            Destroy(this.gameObject);
            gameManager.GanaPuntos(20);
            gameManager.ZombieMuerto(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Limite")
        {
            velocity *= -1;
        }
        if (other.gameObject.tag == "Katana")
        {
            quitarVidasZombie(2);
        }
    }

    public void quitarVidasZombie(int perder)
    {
        vidas -= perder;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Kunai")
        {
            quitarVidasZombie(1);
            Destroy(other.gameObject);
        }
    }

    public int nroVidas()
    {
        return vidas;
    }
}
