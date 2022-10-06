using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletControllerNF : MonoBehaviour
{
    Rigidbody2D rb;

    public float velocity = 20;
    float realVelocity = 5;
    

    private GameManagerControllerNF gameManager;

    public void SetRightDirection()
    {
        realVelocity = velocity;
    }
    public void SetLeftDirection()
    {
        realVelocity = -velocity;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManagerControllerNF>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(realVelocity, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {       
        
        if (other.gameObject.tag == "Enemigo")
        {
            Destroy(this.gameObject); //Se destruye la bala           
        }        
    }
}

