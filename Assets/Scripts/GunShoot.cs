using UnityEngine;
using System.Collections;
using TMPro;

 
public class GunShoot : MonoBehaviour
{
    public GameObject bulletPrefab;       // Prefab de la bala que se disparará
    public Transform bulletSpawnPoint;    // Punto desde donde aparecerá la bala (enfrente de la pistola)
    public float bulletSpeed = 20f;       // Velocidad de la bala
    public float recoilAngle = -12f;      // Ángulo del retroceso en el eje Z
    public float recoilSpeed = 20f;       // Velocidad de la rotación del retroceso
    private Quaternion originalRotation;  // Rotación original de la pistola
    private bool isRecoiling = false;     // Indica si la pistola está en retroceso
    private float cantidadBalas = 7;

    public TextMeshProUGUI cantidadBalasHUD;


    void Start()
    {
        // Guardamos la rotación original de la pistola
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        // Detectamos si el jugador hace clic
        if (Input.GetMouseButtonDown(0) && !isRecoiling && cantidadBalas > 0)
        {
            // Disparar la bala
            ShootBullet();

            // Iniciar el retroceso
            StartCoroutine(Recoil());

            cantidadBalasHUD.text = "Munición: " + cantidadBalas;
        }
    }

    
    
    // Función que dispara la bala
    void ShootBullet()
{
    // Instanciar la bala
    GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, originalRotation);

    cantidadBalas--;

    // Agregar fuerza a la bala
    Rigidbody rb = bullet.GetComponent<Rigidbody>();
    
    // Aquí multiplicamos la dirección forward por la velocidad de la bala usando AddForce
    rb.AddForce(bulletSpawnPoint.forward * bulletSpeed, ForceMode.Impulse);

    Destroy(bullet, 4f);
}


    // Corrutina para manejar el retroceso de la pistola
    IEnumerator Recoil()
    {
        isRecoiling = true;

        // Rotar la pistola hacia el ángulo de retroceso (-12 en el eje Z)
        Quaternion recoilRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, recoilAngle);

        // Realizar la rotación rápidamente
        while (Quaternion.Angle(transform.localRotation, recoilRotation) > 0.1f)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, recoilRotation, recoilSpeed * Time.deltaTime);
            yield return null;
        }

        // Regresar la pistola a su rotación original
        while (Quaternion.Angle(transform.localRotation, originalRotation) > 0.1f)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, originalRotation, recoilSpeed * Time.deltaTime);
            yield return null;
        }

        isRecoiling = false;
    }
}
