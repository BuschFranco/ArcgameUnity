using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using DialogueEditor;
using TMPro;

public class EndGame : MonoBehaviour
{
    public static bool Contador;
    public Transform teleportTarget;
    [SerializeField] GameObject nextDayPoint;
    [SerializeField] private TextMeshProUGUI ObjetiveAlert;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Contador = false;
            
            nextDayPoint.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = teleportTarget.position;

            ObjetiveAlert.text = "Ya estás en tu casita de pana, podes ir a dormir o estár de chill";
            
        }
    }
}
