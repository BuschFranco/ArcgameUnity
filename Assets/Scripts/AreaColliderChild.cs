using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaColliderChild : MonoBehaviour
{
    public static bool inArea = false;
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
            inArea = true;
            Debug.Log("en area");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = false;
            Debug.Log("fuera de area");
        }
    }
}
