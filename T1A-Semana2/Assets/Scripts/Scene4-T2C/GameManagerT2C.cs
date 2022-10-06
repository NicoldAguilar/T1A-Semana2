using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerT2C : MonoBehaviour
{
    public const int SCENE_T1C = 2;


    public Text scoreText;

    public int balas;
    public int lives;
    public int score;

    public float tempx = 0;
    public float tempy = 0;

    // Start is called before the first frame update
    void Start()
    {
        balas = 10;
        lives = 1;
        score = 0;

        PrintDefeatedZombiesInScreen();
    }
   
    public void BalasRestantes(int bullet)
    {
        balas -= bullet;
    }
    public void GanaPuntos(int puntaje)
    {
        score += puntaje;
        PrintDefeatedZombiesInScreen();
    }
    private void PrintDefeatedZombiesInScreen()
    {
        scoreText.text = "  Zombies Derrotados: " + score;
    }
}
