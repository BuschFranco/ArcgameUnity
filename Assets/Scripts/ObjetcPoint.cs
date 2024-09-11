using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetcPoint : MonoBehaviour
{

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
}
