using UnityEngine;
using UnityEngine.UI; // Necesario si deseas mostrar el contador en la UI
using TMPro;
public class ContadorTiempo : MonoBehaviour
{
    public TextMeshProUGUI contadorTiempoUI; // Texto en UI para mostrar el contador (opcional)

    private float tiempoTranscurrido = 0f;

    void Update()
    {
        if(EndGame.Contador == true){
            // Actualizamos el tiempo transcurrido
             tiempoTranscurrido += Time.deltaTime;

             contadorTiempoUI.text = tiempoTranscurrido.ToString("F2");
        }
    }
        
}
