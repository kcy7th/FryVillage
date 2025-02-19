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
    public GameObject choiceUI; // 선택지 UI (게임 시작 / 포기)

    private string[] dialogueLines = {
        "이 성문을 통과하려면 실력을 증명해야 한다.",
        "진정한 감튀 러버는 도전에 겁먹지 않는다!",
        "시험을 통과하면 너도 이곳에 들어갈 수 있을 것이다."
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
        choiceUI.SetActive(true); // 대화 종료 후 선택지 UI 활성화
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

    // 버튼 이벤트 연결
    public void StartMiniGame()
    {
        SceneManager.LoadScene("MiniGameScene"); // 미니게임 씬으로 이동
    }

    public void Cancel()
    {
        choiceUI.SetActive(false); // 선택지 UI 닫기
    }
}
