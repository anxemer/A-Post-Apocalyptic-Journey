using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] NPCConversation myConservation;
    [SerializeField] GameObject npcConversationPanel;
    private bool isPlayerInTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerInTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerInTrigger = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F) && !collision.gameObject.GetComponent<PlayerController>().isInConversationProgress)
            {
                ConversationManager.Instance.StartConversation(myConservation);
            }
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger)
        {
            ShowConversationPanel();
        }
        else
        {
            CloseConversationPanel();
        }
    }

    private void ShowConversationPanel()
    {
        npcConversationPanel.SetActive(true);
    }
    private void CloseConversationPanel()
    {
        npcConversationPanel.SetActive(false);
    }
}
