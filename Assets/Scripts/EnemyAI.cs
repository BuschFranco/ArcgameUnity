using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // La posición del jugador
    public float detectionRange = 20f; // El rango de detección del enemigo
    private NavMeshAgent agent; // El componente NavMeshAgent del enemigo
    private Transform originalPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Obtener el NavMeshAgent del enemigo
        originalPosition.position = agent.transform.position;
    }

    void Update()
    {
        // Calcular la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Si el jugador está dentro del rango de detección
        if (distanceToPlayer <= detectionRange)
        {
            // El enemigo sigue al jugador
            agent.SetDestination(player.position);
        }
        else if (distanceToPlayer >= detectionRange)
        {
            // Si el jugador está fuera del rango, detener al enemigo
            agent.SetDestination(originalPosition.position);
        }
    }

    // Esto dibuja una esfera en la escena para visualizar el rango de detección
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
