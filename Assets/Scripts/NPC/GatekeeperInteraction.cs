using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GatekeeperInteraction : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private int dialogueIndex = 0;
    private bool isTalking = false;
    private bool isDialogueFinished = false; // 대화가 끝났는지 여부

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
        // 플레이어가 근처에 있고 E 키를 누르면 대화 시작
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !isTalking)
        {
            StartDialogue();
        }
        // 대화 중이고 Space 키를 누르면 다음 대사 출력
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

        // 대화를 완료한 경우 선택지 UI 표시
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
        isDialogueFinished = false; // 다시 대화 가능하도록 초기화
        dialogueIndex = 0; // 대화 진행도 초기화
        choiceUI.SetActive(false);  // 선택지 패널 닫기
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

            // 플레이어가 단순 이동한 경우 선택지 UI가 뜨지 않도록 처리
            if (!isTalking && choiceUI != null)
            {
                EndDialogue();
            }
        }
    }

    // 버튼 이벤트 연결
    public void StartMiniGame()
    {
        SceneManager.LoadScene("MiniGameScene"); // 미니게임 씬으로 이동
    }

    public void Cancel()
    {
        choiceUI.SetActive(false);  // 선택지 UI 닫기
        ResetDialogue();  // 대화 상태 초기화
    }
}