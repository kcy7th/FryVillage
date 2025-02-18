using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private bool isTalking = false;
    private NPCController npcController;

    void Start()
    {
        npcController = GetComponent<NPCController>();
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (!isTalking)
            {
                StartDialogue();
            }
            else
            {
                EndDialogue();
            }
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
            if (isTalking) EndDialogue();
        }
    }

    void StartDialogue()
    {
        isTalking = true;
        npcController.canMove = false;
        Debug.Log("대화 시작");
    }

    void EndDialogue()
    {
        isTalking = false;
        npcController.canMove = true;
        Debug.Log("대화 종료");

    }
}
