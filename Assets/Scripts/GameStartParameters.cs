using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStartParameters : MonoBehaviour
{
    
    public static float life = 3;
    public TextMeshProUGUI lifeUI;
    private string _emote;

    void Start()
    {
        Score.objetive = 0;
        Score.score = 0;
        ContadorTiempo.tiempoTranscurrido = 0;
        Timer.elapsedTime = 0;
        life = 3;
        
        
    }

    // Update is called once per frame
    void Update()
    {

        switch (life){
            case 3: _emote = " UwU";
            break;
            case 2: _emote = " -_-";
            break;
            case 1: _emote = " D;"; 
            break;
        }

        lifeUI.text = life.ToString() + _emote;

        if(life <= 0){
            Score.score = 0;
            ContadorTiempo.tiempoTranscurrido = 0;
            life = 0;
            EndGame.Contador = false;

            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Muere y se reinicia el nivel (Agregar posteriormente al final de alguna animación o efecto de muerte)
        }
        
    }
}
