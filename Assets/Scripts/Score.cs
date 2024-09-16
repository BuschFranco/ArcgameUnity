using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Score : MonoBehaviour
{
    public static int score;
    public TextMeshPro RecordUI;
    [SerializeField] private TextMeshProUGUI scoreText; 
    [SerializeField] private TextMeshProUGUI ObjetiveAlert; 
    private float recordTime;

    void Start(){
        recordTime = PlayerPrefs.GetFloat("RecordTime", Mathf.Infinity);
        RecordUI.text = "Record(s): " + recordTime.ToString("F2");
    }

   void Update(){
    scoreText.text = "Objetivos: " + score + "/6";


    if(score == 6 && EndGame.Contador ==! false){ //Utilizo el Contador del EndGame porque necesito un bool para esta función y este me sirve
        ObjetiveAlert.text = "Regresa a la casita";
    }
    
   }


}
