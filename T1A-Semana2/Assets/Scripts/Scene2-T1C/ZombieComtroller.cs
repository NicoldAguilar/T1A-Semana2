using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieComtroller : MonoBehaviour
{
    public int velocity = -2, vidas = 3;
    Rigidbody2D rb;
    Collider2D c;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        c = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocity, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Limite")
        {
            velocity *= -1;
        }
    }

    public void quitarVidasZombie(int perder)
    {
        vidas -= perder;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Disparo1")
        {
            quitarVidasZombie(1);
        }
        if (other.gameObject.tag == "Disparo2")
        {
            quitarVidasZombie(2);
        }
        if (other.gameObject.tag == "Disparo3")
        {
            quitarVidasZombie(3);
        }
        if(vidas <= 0)
        {
            Destroy(this.gameObject); //Se destruye la bala 
        }
    }

    public int nroVidas()
    {
        return vidas;
    }
}
