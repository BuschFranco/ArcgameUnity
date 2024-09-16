using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para usar UI

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float acceleration = 5f;
    public float jumpForce;
    public Vector2 sensitivity;
    public new Transform camera;

    public float extraGravity = 5f;
    public float cameraShakeIntensity = 0.1f;
    public float shakeSpeed = 10f;
    public float shakeAcceleration = 2f;

    private float currentShakeIntensity = 0f;
    private bool isMoving = false;
    private bool isGrounded;
    private Vector3 cameraInitialPosition;
    private Vector3 currentVelocity = Vector3.zero;

    // Ajustes para el Raycast
    public Transform groundCheck; 
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // Variables para correr
    public float runSpeedMultiplier = 1.5f; // Multiplicador de velocidad al correr
    public float maxRunTime = 2f;           // Tiempo máximo de corrida (en segundos)
    public float runCooldownTime = 3f;      // Tiempo de enfriamiento para recargar estamina
    private float currentRunTime;           // Tiempo actual de corrida
    private bool isRunning = false;         // Estado de corrida
    private bool canRun = true;             // Si el jugador puede correr

    // Variable para la barra de estamina (Slider en el Canvas)
    public Slider staminaSlider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        cameraInitialPosition = camera.localPosition;
        currentRunTime = maxRunTime; // Inicia con el tiempo completo de sprint

        // Configurar el Slider para que tenga el rango de 0 a maxRunTime
        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxRunTime;
            staminaSlider.value = maxRunTime; // Comienza con la estamina llena
        }
    }

    void Update()
    {
        CheckGround();
        Movement();
        MouseLook();

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (isMoving)
        {
            currentShakeIntensity = Mathf.Lerp(currentShakeIntensity, cameraShakeIntensity, Time.deltaTime * shakeAcceleration);
        }
        else
        {
            currentShakeIntensity = Mathf.Lerp(currentShakeIntensity, 0f, Time.deltaTime * shakeAcceleration);
        }

        ApplyCameraShake();
        HandleRunning();

        // Actualiza la barra de estamina con el tiempo actual de corrida
        if (staminaSlider != null)
        {
            staminaSlider.value = currentRunTime; // El Slider refleja la estamina disponible
        }
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

            // Ajustar la velocidad dependiendo si está corriendo
            float currentSpeed = speed;
            if (isRunning && canRun)
            {
                currentSpeed *= runSpeedMultiplier; // Aumenta la velocidad mientras corre
            }

            targetVelocity = direction * currentSpeed;
        }
        else
        {
            isMoving = false;
        }

        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        currentVelocity.y = rb.velocity.y;

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

    private void ApplyCameraShake()
    {
        if (isMoving)
        {
            float shakeX = Mathf.PerlinNoise(Time.time * shakeSpeed, 0f) - 0.5f;
            float shakeY = Mathf.PerlinNoise(0f, Time.time * shakeSpeed) - 0.5f;

            Vector3 shakeOffset = new Vector3(shakeX, shakeY, 0) * currentShakeIntensity;
            camera.localPosition = Vector3.Lerp(camera.localPosition, cameraInitialPosition + shakeOffset, Time.deltaTime * shakeAcceleration);
        }
        else
        {
            camera.localPosition = Vector3.Lerp(camera.localPosition, cameraInitialPosition, Time.deltaTime * shakeAcceleration);
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void HandleRunning()
{
    // Comprueba si el jugador está presionando Shift para correr y si puede correr
    if (Input.GetKey(KeyCode.LeftShift) && currentRunTime > 0 && canRun)
    {
        isRunning = true;
        currentRunTime -= Time.deltaTime; // Reduce el tiempo de corrida

        // Si el tiempo de corrida llega a 0, el personaje no puede correr hasta que se recargue
        if (currentRunTime <= 0)
        {
            canRun = false;
            currentRunTime = 0; // Asegura que no caiga por debajo de 0
        }
    }
    else
    {
        isRunning = false;

        // Si no se está corriendo, recarga progresivamente el tiempo de corrida
        if (currentRunTime < maxRunTime)
        {
            currentRunTime += Time.deltaTime / runCooldownTime * maxRunTime; // Recarga de manera progresiva
        }

        // Si la estamina se ha recargado completamente, permite correr de nuevo
        if (currentRunTime >= maxRunTime)
        {
            canRun = true;
        }

        // Asegura que el tiempo de corrida no exceda el máximo
        currentRunTime = Mathf.Clamp(currentRunTime, 0, maxRunTime);
    }
}

}
