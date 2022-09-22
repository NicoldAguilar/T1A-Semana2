using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieComtroller : MonoBehaviour
{
    public int velocity = -2;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
}
