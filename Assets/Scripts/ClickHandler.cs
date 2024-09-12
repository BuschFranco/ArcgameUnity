using UnityEngine;
using DialogueEditor;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] GameObject conversationManager;
    [SerializeField] NPCConversation myDialogue;

    private Collider cl;
    private bool isConversationActive = false;

    void Start()
    {
        cl = GetComponent<Collider>();
    }

    void Update()
    {
        // Verificar si la conversación está activa y si el jugador ha salido del área
        if (isConversationActive && AreaColliderChild.inArea == false)
        {
            EndConversation();
        }
    }

    void OnMouseDown()
    {
        Debug.Log("click");

        // Verificar si el jugador está dentro del área
        if (AreaColliderChild.inArea == true)
        {
            conversationManager.SetActive(true);
            ConversationManager.Instance.StartConversation(myDialogue);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            cl.enabled = false;

            // Activar el flag de conversación activa
            isConversationActive = true;

            // Suscribirnos al evento de cuando la conversación termina
            ConversationManager.OnConversationEnded += OnConversationEnd;
        }
    }

    // Este método será llamado cuando termine la conversación
    private void OnConversationEnd()
    {
        EndConversation();
    }

    // Método para terminar la conversación, ya sea cuando el jugador sale del área o cuando la conversación finaliza
    private void EndConversation()
    {
        if (isConversationActive)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            cl.enabled = true;

            // Desactivar el flag de conversación activa
            isConversationActive = false;

            // Desuscribirnos del evento para evitar múltiples suscripciones
            ConversationManager.OnConversationEnded -= OnConversationEnd;

            // Desactivar el gestor de conversación si es necesario
            conversationManager.SetActive(false);
        }
    }
}
