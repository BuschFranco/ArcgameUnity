using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daño : MonoBehaviour
{
    public Transform teleportTarget; //Un pequeño tp que hace al jugador cuando recibe daño
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other){
        if (other.CompareTag("Player")){
            GameStartParameters.life --;

            other.transform.position = teleportTarget.position;
        }
    }
}
