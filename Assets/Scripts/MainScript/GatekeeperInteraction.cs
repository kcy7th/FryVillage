using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GatekeeperInteraction : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private int dialogueIndex = 0;
    private bool isTalking = false;

    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public GameObject choiceUI; // ������ UI (���� ���� / ����)

    private string[] dialogueLines = {
        "�� ������ ����Ϸ��� �Ƿ��� �����ؾ� �Ѵ�.",
        "������ ��Ƣ ������ ������ �̸��� �ʴ´�!",
        "������ ����ϸ� �ʵ� �̰��� �� �� ���� ���̴�."
    };

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !isTalking)
        {
            StartDialogue();
        }
        else if (isTalking && Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue();
        }
    }

    void StartDialogue()
    {
        isTalking = true;
        dialogueUI.SetActive(true);
        dialogueIndex = 0;
        dialogueText.text = dialogueLines[dialogueIndex];
    }

    void NextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[dialogueIndex];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);
        isTalking = false;
        choiceUI.SetActive(true); // ��ȭ ���� �� ������ UI Ȱ��ȭ
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
            dialogueUI.SetActive(false);
            choiceUI.SetActive(false);
        }
    }

    // ��ư �̺�Ʈ ����
    public void StartMiniGame()
    {
        SceneManager.LoadScene("MiniGameScene"); // �̴ϰ��� ������ �̵�
    }

    public void Cancel()
    {
        choiceUI.SetActive(false); // ������ UI �ݱ�
    }
}
