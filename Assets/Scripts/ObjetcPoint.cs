using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetcPoint : MonoBehaviour
{
    [SerializeField] private AudioClip destructionSound;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Score.score ++;
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        // Verificamos si el objeto tiene la etiqueta "Objective"
        if (gameObject.CompareTag("Objetive"))
        {
            // Reproducimos el sonido en el momento de la destrucción
            AudioSource.PlayClipAtPoint(destructionSound, transform.position);
        }
    }
}
