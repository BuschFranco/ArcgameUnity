using TMPro;
using UnityEngine;

public class Daño : MonoBehaviour
{
    public Transform teleportTarget; //Un pequeño tp que hace al jugador cuando recibe daño
    public GameObject eventHUD;
    public TextMeshProUGUI eventHUDtext;
    void Start()
    {
        eventHUD = GameObject.FindWithTag("EventsHUD");
        eventHUDtext = eventHUD.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other){
        if (other.CompareTag("Player")){
            GameStartParameters.life --;
            eventHUDtext.text = "Perdiste una vida (-1)\n" + eventHUDtext.text;
            other.transform.position = teleportTarget.position;
        }
    }
}
