using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float acceleration = 5f; // Velocidad de aceleración
    public float jumpForce; // Fuerza del salto
    public Vector2 sensitivity;
    public new Transform camera;

    public float extraGravity = 5f; // Gravedad adicional
    public float cameraShakeIntensity = 0.1f; // Intensidad máxima del temblor de la cámara
    public float shakeSpeed = 10f; // Velocidad de la oscilación del temblor
    public float shakeAcceleration = 2f; // Velocidad con la que aumenta el temblor

    private float currentShakeIntensity = 0f; // Intensidad actual del temblor
    private bool isMoving = false; // Para verificar si el jugador está moviéndose
    private bool isGrounded; // Para verificar si el jugador está en el suelo
    private Vector3 cameraInitialPosition; // Para almacenar la posición inicial de la cámara
    private Vector3 currentVelocity = Vector3.zero; // Velocidad actual del jugador

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        cameraInitialPosition = camera.localPosition; // Guarda la posición inicial de la cámara
    }

    void Update()
    {
        Movement();
        MouseLook();

        // Verifica si el jugador está en el suelo y si presionó el botón de salto
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // Actualiza la intensidad del temblor progresivamente cuando el jugador se mueve
        if (isMoving)
        {
            // Incrementa progresivamente la intensidad del temblor
            currentShakeIntensity = Mathf.Lerp(currentShakeIntensity, cameraShakeIntensity, Time.deltaTime * shakeAcceleration);
        }
        else
        {
            // Reduce progresivamente la intensidad del temblor cuando el jugador deja de moverse
            currentShakeIntensity = Mathf.Lerp(currentShakeIntensity, 0f, Time.deltaTime * shakeAcceleration);
        }

        // Aplicamos el temblor de la cámara de manera suave, incluso cuando el jugador deja de moverse
        ApplyCameraShake();
    }

    private void Movement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 targetVelocity = Vector3.zero;

        if (hor != 0 || ver != 0)
        {
            isMoving = true;
            Vector3 direction = (transform.forward * ver + transform.right * hor).normalized;
            targetVelocity = direction * speed;
        }
        else
        {
            isMoving = false;
        }

        // Lerp para suavizar la aceleración (interpolación lineal)
        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        currentVelocity.y = rb.velocity.y;

        // Si no está en el suelo, añade gravedad extra
        if (!isGrounded)
        {
            currentVelocity.y -= extraGravity * Time.deltaTime;
        }

        rb.velocity = currentVelocity;
    }

    private void MouseLook()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        if (hor != 0)
        {
            transform.Rotate(0, hor * sensitivity.x, 0);
        }
        if (ver != 0)
        {
            Vector3 rotation = camera.localEulerAngles;
            rotation.x = (rotation.x - ver * sensitivity.y + 360) % 360;
            if (rotation.x > 80 && rotation.x < 180)
            {
                rotation.x = 80;
            }
            else if (rotation.x < 280 && rotation.x > 180)
            {
                rotation.x = 280;
            }

            camera.localEulerAngles = rotation;
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // Detecta colisiones con el suelo para saber si el jugador está en el suelo
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void ApplyCameraShake()
    {
        // Si el jugador está moviéndose, aplicamos el temblor a la cámara
        if (isMoving)
        {
            float shakeX = Mathf.PerlinNoise(Time.time * shakeSpeed, 0f) - 0.5f;
            float shakeY = Mathf.PerlinNoise(0f, Time.time * shakeSpeed) - 0.5f;

            Vector3 shakeOffset = new Vector3(shakeX, shakeY, 0) * currentShakeIntensity;
            camera.localPosition = Vector3.Lerp(camera.localPosition, cameraInitialPosition + shakeOffset, Time.deltaTime * shakeAcceleration);
        }
        else
        {
            // Si el jugador no se está moviendo, suavemente llevamos la cámara de vuelta a su posición inicial
            camera.localPosition = Vector3.Lerp(camera.localPosition, cameraInitialPosition, Time.deltaTime * shakeAcceleration);
        }
    }
}
