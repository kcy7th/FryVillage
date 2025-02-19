using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NPCInteraction : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private int currentLineIndex = 0;
    private bool isTalking = false; // ���� ��ȭ ������ Ȯ��

    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    private NPCController npcController; // NPC �̵� ���� (�����̴� NPC�� �ش�)

    private List<string> dialogueLines = new List<string>();

    void Start()
    {
        npcController = GetComponent<NPCController>();

        // NPC�� ��� ����
        switch (gameObject.name)
        {
            case "Farmer":
                dialogueLines = new List<string>
                {
                    "�ȳ��Ͻÿ�! ���� ���� ��簡 ���� �� �Ƽ�.",
                    "���ִ� ����Ƣ���� ������� �ż��� ���ڰ� �ʼ���!",
                    "���� ���� ������ ���̸�, ���ڴ� ������� �ʼ�."
                };
                break;

            case "Villager":
                dialogueLines = new List<string>
                {
                    "�� ������ ������ ������ ���̾�.",
                    "��¥ �ٻ��� ����Ƣ���� ����� ����� �ִٴ���...",
                    "���� �տ��� ������! �ƹ��� �� �� ���� ���̷�."
                };
                break;

            case "Shopkeeper":
                dialogueLines = new List<string>
                {
                    "����Ƣ���� �ʿ���? �ְ��� ���ڿ� ���̷ᰡ �غ�Ǿ� ����!",
                    "������� ���ڰ� ���� �����̶����, �� �� �纼��?",
                    "���� ��ᰡ ���� ����Ƣ���� ����ٰ�! �?"
                };
                break;

            case "Scientist":
                dialogueLines = new List<string>
                {
                    "����Ƣ���� Ȳ�ݺ����� �ٻ��� 60%, �ε巯�� 40%!",
                    "���� ���ĸ� �̿��� ���ο� ����Ƣ�� Ƣ����� ���� ���̾�!",
                    "������ �̷��� �ٲ۴�! �׸��� ����Ƣ�赵 �� ���ְ� ���� �ž�!"
                };
                break;

            case "PotatoBeing":
                dialogueLines = new List<string>
                {
                    "...",
                    "�� �׷��� �Ĵٺ���? ���ڰ� ���� �ϸ� �̻��Ѱ�?",
                    "�� �Ѷ� ����� ������... ������ ���� ������ ���� �����߾�."
                };
                break;

            case "Gatekeeper":
                dialogueLines = new List<string>
                {
                    "�� ������ ����Ϸ��� �Ƿ��� �����ؾ� �Ѵ�.",
                    "������ ����� ������ �̸��� �ʴ´�!",
                    "������ ����ϸ� �ʵ� �̰��� �� �� ���� ���̴�."
                };
                break;

            case "Monster":
                dialogueLines = new List<string>
                {
                    "ũ����... ����Ƣ�� ������ �ʹ� ���ֱ�!",
                    "�� �ٻ��� ����Ƣ�踸�� ���Ѵ�!",
                    "���� ���������� �� ������ �޾Ƶ鿩�� �Ѵ�!"
                };
                break;
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !isTalking)
        {
            StartDialogue(); // ��ȭ ���� �� �׻� ó������ ����
        }

        if (isTalking && Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue();
        }
    }

    void StartDialogue()
    {
        if (dialogueUI == null || dialogueText == null || dialogueLines.Count == 0)
        {
            return;
        }

        isTalking = true; // ��ȭ �� ���·� ����
        dialogueUI.SetActive(true);
        currentLineIndex = 0; // �׻� ó������ ����
        dialogueText.text = dialogueLines[currentLineIndex];

        if (npcController != null)
        {
            npcController.canMove = false; // ��ȭ �� NPC ����
        }
    }

    void NextDialogue()
    {
        currentLineIndex++;

        if (currentLineIndex < dialogueLines.Count)
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
        isTalking = false; // ��ȭ�� ������ �ٽ� ��ȭ �����ϵ��� �ʱ�ȭ

        if (npcController != null)
        {
            npcController.canMove = true; // ��ȭ ���� �� NPC �̵� �簳
        }
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
