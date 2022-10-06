using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaController : MonoBehaviour
{
    private GameManagerControllerNF gameManager;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemigo")
        {
            Destroy(this.gameObject); //Se destruye la bala
            Destroy(other.gameObject);
            gameManager.GanaPuntos(1);
        }
    }
}
