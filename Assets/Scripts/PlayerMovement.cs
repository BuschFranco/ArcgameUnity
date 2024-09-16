using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        cameraInitialPosition = camera.localPosition;
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

    // Uso de Raycast para detectar el piso
    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
}
