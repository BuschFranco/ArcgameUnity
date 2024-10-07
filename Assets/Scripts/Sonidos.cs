using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonidos : MonoBehaviour
{

    [SerializeField] private AudioClip dañoSound;
    [SerializeField] private AudioClip dañoSoundKings; //FALTA ADAPTAR
    void Update()
    {



        
    }

    void OnDestroy(){
        if (gameObject.CompareTag("Enemy"))
        {
            AudioSource.PlayClipAtPoint(dañoSound, transform.position, 1f); // Volumen a 0.3
        }
        if (gameObject.CompareTag("EnemyKing"))
        {
            AudioSource.PlayClipAtPoint(dañoSound, transform.position, 1f); // Volumen a 0.3
        }
    }
}
