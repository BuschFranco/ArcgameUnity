using UnityEngine;
using UnityEngine.UI;

public class StartPoint : MonoBehaviour
{
    public GameObject timeBar; // Referencia a la barra de tiempo en el Canvas
    [SerializeField] private GameObject objetiveAlertUI;
    [SerializeField] private GameObject contadorTiempoUI;

    private void Start()
    {
        if (timeBar != null)
        {
            timeBar.SetActive(false); // Asegúrate de que la barra de tiempo esté desactivada al inicio
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (timeBar != null)
            {
                timeBar.SetActive(true); // Activa la barra de tiempo cuando el Player entra en el BoxCollider
                objetiveAlertUI.gameObject.SetActive(true);
                contadorTiempoUI.gameObject.SetActive(true);
            }
        }
    }
}
