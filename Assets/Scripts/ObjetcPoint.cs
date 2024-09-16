using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetcPoint : MonoBehaviour
{
    [SerializeField] private AudioClip destructionSound;

    // Start is called before the first frame update
    void Start()
    {
        // Cargar el récord guardado al inicio del juego
        int recordScore = PlayerPrefs.GetInt("RecordScore", 0);
        Debug.Log("Record Score al iniciar: " + recordScore);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        // Verificamos si el objeto tiene la etiqueta "Objective"
        if (gameObject.CompareTag("Objetive"))
        {
            Score.objetive++;
            // Reproducimos el sonido en el momento de la destrucción
            AudioSource.PlayClipAtPoint(destructionSound, transform.position);
        }
        else if (gameObject.CompareTag("Score"))
        {
            // Añadimos puntos al score actual
            Score.score += 100;
            AudioSource.PlayClipAtPoint(destructionSound, transform.position);

            // Verificar si el puntaje actual es un nuevo récord
            CheckRecordScore();
        }
    }

    // Método para verificar y guardar si es un nuevo récord
    void CheckRecordScore()
    {
        // Obtenemos el récord actual almacenado en PlayerPrefs
        int recordScore = PlayerPrefs.GetInt("RecordScore", 0);

        // Si el puntaje actual es mayor que el récord, lo actualizamos
        if (Score.score > recordScore)
        {
            PlayerPrefs.SetInt("RecordScore", Score.score);
            PlayerPrefs.Save(); // Guardamos permanentemente
            Debug.Log("¡Nuevo récord establecido!: " + Score.score);
        }
        else
        {
            Debug.Log("Puntaje actual: " + Score.score + ". Récord actual: " + recordScore);
        }
    }
}
