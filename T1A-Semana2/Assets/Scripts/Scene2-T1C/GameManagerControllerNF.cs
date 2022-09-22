using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManagerControllerNF : MonoBehaviour
{
    public const int SCENE_T1C = 2;


    public Text livesText;
    public Text balasText;
    public Text coinsText;
    public Text coins2Text;
    public Text puntosText;

    public int balas;
    public int lives;
    public int coins;
    public int coins2;
    public int score;

    public float tempx = 0;
    public float tempy = 0;

    // Start is called before the first frame update
    void Start()
    {
        balas = 10;
        lives = 1;
        coins = 0;
        coins2 = 0;
        score = 0;

        PrintInScreenBullet();
        PrintLivesInScreen();
        PrintCoinsT1InScreen();
        PrintPuntosInScreen();
        LoadGame();
    }
    public void SaveGame()
    {
        var filePath = Application.persistentDataPath + "/saveT2.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenWrite(filePath);
        else file = File.Create(filePath);

        GameData data = new GameData();
        data.Score = score;
        data.CoinsT1 = coins;
        data.CoinsT2 = coins2;
        data.Balas = balas;
        data.ax = tempx;
        data.ay = tempy;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }
    public void LoadGame()
    {
        var filePath = Application.persistentDataPath + "/saveT2.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenRead(filePath);
        else
        {
            Debug.LogError("No se encontro el archivo");
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        //Para llamar a los datos guardados
        score = data.Score;
        PrintPuntosInScreen();
        coins = data.CoinsT1;
        PrintCoinsT1InScreen();
        coins2 = data.CoinsT2;
        PrintCoinsT2InScreen();

        tempx = data.ax;
        tempy = data.ay;

        Debug.Log("Carga");
        
    }

    public void empezarCero()
    {
        var filePath = Application.persistentDataPath + "/saveT2.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenWrite(filePath);
        else file = File.Create(filePath);

        GameData data = new GameData();
        data.Score = 0;
        data.CoinsT1 = 0;
        data.CoinsT2 = 0;
        data.Balas = 10;
        data.ax = 0;
        data.ay = 0;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }
    public void GuardarPosicionPartida(float x, float y)
    {
        tempx = x;
        tempy = y;
    }
    public void BalasRestantes(int bullet)
    {
        balas -= bullet;
        PrintInScreenBullet();
    }

    public void PerderVida()
    {
        lives -= 1;
        PrintLivesInScreen();
    }

    public void GanaMonedasT1(int moneditas)
    {
        coins += moneditas;
        PrintCoinsT1InScreen();
    }

    public void GanaMonedasT2(int moneditas2)
    {
        coins2 += moneditas2;
        PrintCoinsT2InScreen();
    }

    public void GanaPuntos(int puntaje)
    {
        score += puntaje;
        PrintPuntosInScreen();
    }

    private void PrintInScreenBullet()
    {
        balasText.text = "Balas Restantes: " + balas + "/10";
    }

    public void PrintLivesInScreen()
    {
        livesText.text = "Vida: " + lives;
    }

    private void PrintCoinsT1InScreen()
    {
        coinsText.text = "Monedas T1: " + coins;
    }

    private void PrintCoinsT2InScreen()
    {
        coins2Text.text = "Monedas T2: " + coins2;
    }

    private void PrintPuntosInScreen()
    {
        puntosText.text = "Puntos: " + score;
    }
}

