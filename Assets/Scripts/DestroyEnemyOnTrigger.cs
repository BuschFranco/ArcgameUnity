using UnityEngine;

public class DestroyEnemyOnTrigger : MonoBehaviour
{
    // Este método se llama cuando otro objeto entra en el Trigger del objeto actual
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto con el que colisiona tiene la etiqueta "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Destruir el objeto enemigo
            Destroy(other.gameObject);
        }
        if (other.CompareTag("EnemyKing"))
        {
            // Destruir el objeto enemigo
            Destroy(other.gameObject);
        }
    }
}
