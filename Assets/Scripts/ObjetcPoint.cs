using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetcPoint : MonoBehaviour
{
    [SerializeField] private AudioClip destructionSound;

    // Start is called before the first frame update
    void Start()
    {
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
        if (gameObject.CompareTag("Objetive"))
{
    Score.objetive++;
    AudioSource.PlayClipAtPoint(destructionSound, transform.position, 0.1f); // Volumen a 0.3
}
else if (gameObject.CompareTag("Score") && EndGame.Contador == true)
{
    Score.score += 100;
    AudioSource.PlayClipAtPoint(destructionSound, transform.position, 0.1f); // Volumen a 0.3
}
else if (gameObject.CompareTag("Bonus"))
{
    Timer.elapsedTime -= 10f;
    ContadorTiempo.tiempoTranscurrido -= 10f;
    AudioSource.PlayClipAtPoint(destructionSound, transform.position, 0.1f); // Volumen a 0.3
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
