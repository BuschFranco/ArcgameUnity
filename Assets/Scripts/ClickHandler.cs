using UnityEngine;
using DialogueEditor;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] GameObject conversationManager;
    [SerializeField] NPCConversation myDialogue;

    private  Collider cl;

    void Start()
    {
        cl = GetComponent<Collider>();
    }

    void OnMouseDown()
    {
        conversationManager.SetActive(true);
        ConversationManager.Instance.StartConversation(myDialogue);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        cl.enabled = false;

        // Suscribirnos al evento de cuando la conversación termina
        ConversationManager.OnConversationEnded += OnConversationEnd;
    }

    // Este método será llamado cuando termine la conversación
    private void OnConversationEnd()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cl.enabled = true;

        // Desuscribirnos del evento una vez que se haya ejecutado
        ConversationManager.OnConversationEnded -= OnConversationEnd;
    }
}
