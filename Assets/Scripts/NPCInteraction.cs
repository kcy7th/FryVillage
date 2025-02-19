using UnityEngine;
using TMPro;

public class StaticNPCInteraction : MonoBehaviour
{
    public string[] dialogueLines; // NPC 대사 리스트
    private bool isPlayerNearby = false;
    private int currentLineIndex = 0;

    public GameObject dialogueUI; // UI 패널
    public TextMeshProUGUI dialogueText; // TMP 텍스트

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ShowDialogue();
        }

        if (dialogueUI.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue();
        }
    }

    void ShowDialogue()
    {
        if (dialogueUI == null || dialogueText == null || dialogueLines.Length == 0)
        {
            return;
        }

        dialogueUI.SetActive(true);
        currentLineIndex = 0;
        dialogueText.text = dialogueLines[currentLineIndex];
    }

    void NextDialogue()
    {
        currentLineIndex++;

        if (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            EndDialogue();
        }
    }
}
