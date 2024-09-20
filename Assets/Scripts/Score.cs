using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Score : MonoBehaviour
{
    public static int objetive;
    public static int score;
    public TextMeshPro timeRecordUI;
    public TextMeshPro scoreRecordUI;
    [SerializeField] private TextMeshProUGUI objetiveText; 
    [SerializeField] private TextMeshProUGUI scoreText; 
    [SerializeField] private TextMeshProUGUI ObjetiveAlert; 
    private float recordTime;
    private float recordScore;

    void Start(){
        

    }

   void Update(){
    objetiveText.text = objetive + "/6";
    scoreText.text = score.ToString();


    if(objetive == 6 && EndGame.Contador ==! false){ //Utilizo el Contador del EndGame porque necesito un bool para esta función y este me sirve
        ObjetiveAlert.text = "Regresa a la casita";
    }


        //Actualiza los records en pantalla
        recordTime = PlayerPrefs.GetFloat("RecordTime", Mathf.Infinity);
        timeRecordUI.text = "Record(s): " + recordTime.ToString("F2");

        recordScore = PlayerPrefs.GetInt("RecordScore", 0);
        scoreRecordUI.text = "Record(Score): " + recordScore.ToString();
   }


}
