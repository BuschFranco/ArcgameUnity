using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 2f;        // Velocidad a la que la plataforma se mueve
    public float moveDistance = 5f;     // Distancia que la plataforma baja
    private Vector3 originalPosition;   // Posición original de la plataforma
    private bool isMovingDown = false;  // Bandera para indicar si la plataforma está bajando
    private bool isReturning = false;   // Bandera para indicar si la plataforma está volviendo a la posición original

    void Start()
    {
        // Guardamos la posición original de la plataforma
        originalPosition = transform.position;
    }

    void Update()
    {
        // Si la plataforma está bajando, moverla hacia abajo
        if (isMovingDown)
        {
            MovePlatformDown();
        }
        // Si la plataforma está volviendo a su posición original, moverla hacia arriba
        else if (isReturning)
        {
            MovePlatformUp();
        }
    }

    // Mueve la plataforma hacia abajo
    void MovePlatformDown()
    {
        // Si la plataforma no ha llegado a la distancia máxima, sigue bajando
        if (transform.position.y > originalPosition.y - moveDistance)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
        else
        {
            // Cuando llegue a la distancia máxima, empieza a regresar
            isMovingDown = false;
            isReturning = true;
        }
    }

    // Mueve la plataforma de vuelta a su posición original
    void MovePlatformUp()
    {
        if (transform.position.y < originalPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Cuando llegue a la posición original, detener el movimiento
            isReturning = false;
        }
    }

    // Detecta cuando el jugador toca la plataforma
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isMovingDown = true;   // Comienza a bajar cuando el jugador toca la plataforma
            isReturning = false;   // Asegurarse de que no esté volviendo mientras baja
        }
    }
}
