using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartParameters : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Score.objetive = 0;
        Score.score = 0;
        EndGame.Contador = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
