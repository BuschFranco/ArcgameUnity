using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContadorTiempo : MonoBehaviour
{
    public TextMeshProUGUI contadorTiempoUI;
    
    private float tiempoTranscurrido = 0f;
    public static float recordTime;

    void Update()
    {
        if (EndGame.Contador == true)
        {
            // Actualizamos el tiempo transcurrido
            tiempoTranscurrido += Time.deltaTime;
            contadorTiempoUI.text = tiempoTranscurrido.ToString("F2");
        }
        else if (EndGame.Contador == false) // Suponiendo que Contador se vuelve falso al finalizar el juego
        {
            // Llamar a la función para verificar si es un nuevo récord
            CheckRecordTime();
        }
    }

    // Método para verificar si el tiempo actual es mejor que el récord
    void CheckRecordTime()
    {
        //Llamamos al record actual y lo comparamos
        recordTime = PlayerPrefs.GetFloat("RecordTime", Mathf.Infinity);

        if (tiempoTranscurrido < recordTime)
        {
            // Guardamos el nuevo récord en PlayerPrefs
            recordTime = tiempoTranscurrido;
            PlayerPrefs.SetFloat("RecordTime", recordTime);
            PlayerPrefs.Save(); // Asegura que el tiempo se guarde permanentemente
            Debug.Log("¡Nuevo récord establecido!: " + recordTime.ToString("F2"));
        }
        else
        {
            Debug.Log("Tiempo final: " + tiempoTranscurrido.ToString("F2") + ". Récord actual: " + recordTime.ToString("F2"));
        }
    }
}
