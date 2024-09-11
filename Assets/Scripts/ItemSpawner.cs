using UnityEngine;
using System.Collections.Generic;


public class ItemSpawner : MonoBehaviour
{
    public GameObject[] spawnPoints; // Array para almacenar los puntos de spawn.
    public GameObject collectiblePrefab; // Prefab del ítem coleccionable que deseas spawnear.
    public int numberOfItemsToSpawn = 5; // Cantidad de ítems a generar.

    void Start()
    {
        SpawnCollectibles();
    }

    void SpawnCollectibles()
    {
        // Asegúrate de que no se generen más ítems que puntos de spawn.
        if (numberOfItemsToSpawn > spawnPoints.Length)
            numberOfItemsToSpawn = spawnPoints.Length;

        // Lista de los puntos de spawn disponibles.
        List<int> availablePoints = new List<int>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            availablePoints.Add(i);
        }

        // Spawn aleatorio de los ítems.
        for (int i = 0; i < numberOfItemsToSpawn; i++)
        {
            // Elige un índice aleatorio de los puntos disponibles.
            int randomIndex = Random.Range(0, availablePoints.Count);

            // Instancia el ítem en el punto de spawn seleccionado.
            Instantiate(collectiblePrefab, spawnPoints[availablePoints[randomIndex]].transform.position, Quaternion.identity);

            // Remueve el punto seleccionado de la lista para evitar repetirlo.
            availablePoints.RemoveAt(randomIndex);
        }
    }
}
