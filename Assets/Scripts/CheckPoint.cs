using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] NPCConversation myDialogue;
    [SerializeField] GameObject conversationManager;
    [SerializeField] GameObject dialogueCloud;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueCloud.SetActive(false);
           
            conversationManager.SetActive(true);
            ConversationManager.Instance.StartConversation(myDialogue);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueCloud.SetActive(true);

            ConversationManager.Instance.EndConversation();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            conversationManager.SetActive(false);
        }
    }
}
