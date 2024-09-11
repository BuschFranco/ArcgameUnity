using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player; // Referencia al jugador

    void Update()
    {
        // Calculamos la dirección hacia el jugador
        Vector3 direction = player.position - transform.position;

        // Calculamos la rotación hacia el jugador
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Aplicamos la rotación
        transform.rotation = lookRotation;

        // Aseguramos que el objeto 2D esté orientado correctamente en el plano (frente al jugador)
        // Esto mantiene el objeto en el plano 2D frente al jugador
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
