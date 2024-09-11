using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitHouse : MonoBehaviour
{
    public Transform teleportTarget;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        other.transform.position = teleportTarget.position;
        Destroy(this.gameObject);
    }
}
