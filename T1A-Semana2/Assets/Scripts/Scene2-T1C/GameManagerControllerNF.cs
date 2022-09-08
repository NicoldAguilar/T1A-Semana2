using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerControllerNF : MonoBehaviour
{
    public Text livesText;
    public Text balasText;

    public int balas;
    public int lives;

    // Start is called before the first frame update
    void Start()
    {
        balas = 5;
        lives = 1;
        PrintInScreenBullet();
        PrintLivesInScreen();
    }

    public void BalasRestantes(int puntos)
    {
        balas -= 1;
        PrintInScreenBullet();
    }

    public void PerderVida()
    {
        lives -= 1;
        PrintLivesInScreen();
    }

    public void PrintLivesInScreen()
    {
        livesText.text = "Vida: " + lives;
    }

    private void PrintInScreenBullet()
    {
        balasText.text = "Balas Restantes: " + balas;
    }


}

