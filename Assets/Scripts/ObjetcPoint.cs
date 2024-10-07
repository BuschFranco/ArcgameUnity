using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjetcPoint : MonoBehaviour
{
    [SerializeField] private AudioClip destructionSound;

    //Elementos Relacionados con eventos en HUD
    public GameObject eventHUD;
    public TextMeshProUGUI eventHUDtext;

    // Start is called before the first frame update
    void Start()
    {
        eventHUD = GameObject.FindWithTag("EventsHUD");

         if (eventHUD != null)
        {
            // Acceder al componente NavMeshAgent del objeto enemigo
            eventHUDtext = eventHUD.GetComponent<TextMeshProUGUI>();
        }

        int recordScore = PlayerPrefs.GetInt("RecordScore", 0);
        Debug.Log("Record Score al iniciar: " + recordScore);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
         if (gameObject.CompareTag("Objetive"))
        {
            eventHUDtext.text = "Objetivo Recogido (+1)\n" + eventHUDtext.text;
            Score.objetive++;
            AudioSource.PlayClipAtPoint(destructionSound, transform.position, 0.1f); // Volumen a 0.3
        }
        else if (gameObject.CompareTag("Score") && EndGame.Contador == true)
        {
            eventHUDtext.text = "+100P\n" + eventHUDtext.text;
            Score.score += 100;
            AudioSource.PlayClipAtPoint(destructionSound, transform.position, 0.1f); // Volumen a 0.3
        }
        else if (gameObject.CompareTag("Bonus"))
        {
            eventHUDtext.text = "Bonus de Tiempo (+25s)\n" + eventHUDtext.text;
            Timer.elapsedTime -= 25f;
            ContadorTiempo.tiempoTranscurrido -= 25f;
            AudioSource.PlayClipAtPoint(destructionSound, transform.position, 0.1f); // Volumen a 0.3
        }
        else if(gameObject.CompareTag("Enemy"))
        {
            eventHUDtext.text = "Mataste a un Fantasma (+50P)\n" + eventHUDtext.text;
            Score.score += 50;
        }
        else if(gameObject.CompareTag("EnemyKing"))
        {
            eventHUDtext.text = "Mataste al Rey de los Fantasmas (+200P)\n" + eventHUDtext.text;
            Score.score += 200;
        }
    }

    public static void CheckRecordScore()
    {
        // Obtenemos el récord actual almacenado en PlayerPrefs
        int recordScore = PlayerPrefs.GetInt("RecordScore", 0);

        // Si el puntaje actual es mayor que el récord, lo actualizamos
        if (Score.score > recordScore)
        {
            PlayerPrefs.SetInt("RecordScore", Score.score);
            PlayerPrefs.Save();
            Debug.Log("¡Nuevo récord establecido!: " + Score.score);
        }
        else
        {
            Debug.Log("Puntaje actual: " + Score.score + ". Récord actual: " + recordScore);
        }
    }
}
