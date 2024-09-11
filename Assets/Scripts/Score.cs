using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Score : MonoBehaviour
{
    public static int score;
    [SerializeField] private TextMeshProUGUI scoreText; 
    [SerializeField] private TextMeshProUGUI ObjetiveAlert; 

   void Update(){
    scoreText.text = "Objetivos: " + score + "/6";


    if(score == 6 && EndGame.Contador ==! false){ //Utilizo el Contador del EndGame porque necesito un bool para esta función y este me sirve
        ObjetiveAlert.text = "Regresa a la casita";
    }
    
   }


}
