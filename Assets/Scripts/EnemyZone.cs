using UnityEngine;

public class ActivatePistolOnTrigger : MonoBehaviour
{
    public GameObject pistol; // Referencia al objeto Pistol del jugador
    public GameObject cantidadBalasHUD;

    // Detecta cuando un objeto entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que entró tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            // Activar el objeto Pistol
            pistol.SetActive(true);
            cantidadBalasHUD.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Verificamos si el objeto que entró tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            // Activar el objeto Pistol
            pistol.SetActive(false);
            cantidadBalasHUD.SetActive(false);
        }
    }
}
