using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] public GameObject endGame;
    [SerializeField]public RectTransform timerBar;   // Referencia al RectTransform de la barra de progreso
    [SerializeField]public RectTransform timerPoint; // Referencia al RectTransform del punto
    public float timeLimit = 60f;       // Tiempo total para completar el movimiento

    private float elapsedTime = 0f;

    public float startPoint;

    void Start()
    {
        startPoint = timerPoint.anchoredPosition.x;
    }

    void Update()
    {
        // Incrementar el tiempo transcurrido
        elapsedTime += Time.deltaTime;

        // Calcular la posición del punto en la barra en función del tiempo transcurrido
        float normalizedTime = Mathf.Clamp01(elapsedTime / timeLimit);

        // Calcular la nueva posición del punto
        Vector2 newPosition = new Vector2(startPoint + normalizedTime * timerBar.rect.width, timerPoint.anchoredPosition.y);

        // Actualizar la posición del punto
        timerPoint.anchoredPosition = newPosition;

        // Si se ha completado el tiempo, reinicia la escena o resetea el temporizador
        if (elapsedTime >= timeLimit)
        {
            elapsedTime = 0f; // Reinicia el tiempo
            // Aquí podrías reiniciar la escena si es necesario:
           SceneManager.LoadScene(SceneManager.GetActiveScene().name);
           Score.score = 0;
        }

        if(Score.score == 6)
        {
            endGame.gameObject.SetActive(true);
        }
        if(endGame.gameObject.activeSelf == true && EndGame.Contador == false){
            this.gameObject.SetActive(false);
        }

    }
}
