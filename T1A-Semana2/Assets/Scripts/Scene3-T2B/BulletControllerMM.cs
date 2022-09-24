using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerMM : MonoBehaviour
{
    Rigidbody2D rb;

    public float velocity = 20, realVelocity;

    public void SetDirection(bool verificar)
    {
        if(verificar == true) realVelocity = velocity;
        if(verificar == false) realVelocity = -velocity;
    }

    // Start is called before the first frame update
    void Start()
    {
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
