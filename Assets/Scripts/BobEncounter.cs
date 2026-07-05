using System;
using UnityEngine;

public class BobEncounter : MonoBehaviour
{
    [SerializeField] private DisplayPanelDialogue dialoguePanel;
    [SerializeField] private BobManager bob;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bob.SetDialogLine();
            bob.PlayDialogVoice();
            dialoguePanel.Display();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialoguePanel.Hide();
        }
    }
}