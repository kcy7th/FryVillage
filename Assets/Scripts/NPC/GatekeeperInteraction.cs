using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GatekeeperInteraction : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private int dialogueIndex = 0;
    private bool isTalking = false;
    private bool isDialogueFinished = false; // ��ȭ�� �������� ����

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
        // �÷��̾ ��ó�� �ְ� E Ű�� ������ ��ȭ ����
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !isTalking)
        {
            StartDialogue();
        }
        // ��ȭ ���̰� Space Ű�� ������ ���� ��� ���
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
        if (dialogueUI != null) dialogueUI.SetActive(false);
        isTalking = false;

        // ��ȭ�� �Ϸ��� ��� ������ UI ǥ��
        if (dialogueIndex >= dialogueLines.Length)
        {
            isDialogueFinished = true;
        }

        if (gameObject.name == "Gatekeeper" && isDialogueFinished && choiceUI != null)
        {
            choiceUI.SetActive(true);
        }
    }

    public void ResetDialogue()
    {
        isDialogueFinished = false; // �ٽ� ��ȭ �����ϵ��� �ʱ�ȭ
        dialogueIndex = 0; // ��ȭ ���൵ �ʱ�ȭ
        choiceUI.SetActive(false);  // ������ �г� �ݱ�
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

            // �÷��̾ �ܼ� �̵��� ��� ������ UI�� ���� �ʵ��� ó��
            if (!isTalking && choiceUI != null)
            {
                EndDialogue();
            }
        }
    }

    // ��ư �̺�Ʈ ����
    public void StartMiniGame()
    {
        SceneManager.LoadScene("MiniGameScene"); // �̴ϰ��� ������ �̵�
    }

    public void Cancel()
    {
        choiceUI.SetActive(false);  // ������ UI �ݱ�
        ResetDialogue();  // ��ȭ ���� �ʱ�ȭ
    }
}