using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStartParameters : MonoBehaviour
{
    
    public static float life = 3;
    public TextMeshProUGUI lifeUI;

    void Start()
    {
        Score.objetive = 0;
        Score.score = 0;
        ContadorTiempo.tiempoTranscurrido = 0;
        Timer.elapsedTime = 0;
        life = 3;
        EndGame.Contador = true;
    }

    // Update is called once per frame
    void Update()
    {

        lifeUI.text = "Vidas: " + life;

        if(life <= 0){
            life = 0;

            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Muere y se reinicia el nivel (Agregar posteriormente al final de alguna animación o efecto de muerte)
        }
        
    }
}
