using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerNF : MonoBehaviour
{
    Rigidbody2D rb;

    public float velocity = 20;
    float realVelocity;

    private GameManagerControllerNF gameManager;

    public void SetRightDirection()
    {
        realVelocity = velocity;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManagerControllerNF>();
        rb = GetComponent<Rigidbody2D>();
        //Destroy(this.gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(realVelocity, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {       
        //Destroy(this.gameObject); //Se destruye la bala
        if (other.gameObject.tag == "Enemigo")
        {
            Destroy(other.gameObject);
            gameManager.BalasRestantes(1);
        }
    }
}

