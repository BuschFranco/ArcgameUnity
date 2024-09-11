using UnityEngine;

public class MovimientoArribaAbajo : MonoBehaviour
{
    public float velocidad = 4f; // Velocidad del movimiento
    public float amplitud = 0.5f;  // Amplitud del movimiento

    private Vector3 posicionInicial;

    void Start()
    {
        // Guardamos la posición inicial del objeto
        posicionInicial = transform.position;
    }

    void Update()
    {
        // Calculamos el desplazamiento vertical usando Mathf.Sin para un movimiento suave
        float desplazamientoY = Mathf.Sin(Time.time * velocidad) * amplitud;

        // Aplicamos el desplazamiento a la posición inicial del objeto
        transform.position = new Vector3(posicionInicial.x, posicionInicial.y + desplazamientoY, posicionInicial.z);
    }
}
