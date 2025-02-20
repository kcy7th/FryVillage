using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NPCInteraction : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private int currentLineIndex = 0;
    private bool isTalking = false; // 현재 대화 중인지 확인

    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;  // 대화 내용 표시할 TMP UI
    private NPCController npcController; // NPC 이동 제어 (움직이는 NPC만 해당)

    private List<string> dialogueLines = new List<string>();

    void Start()
    {
        npcController = GetComponent<NPCController>();

        // NPC별 대사 설정
        switch (gameObject.name)
        {
            case "Farmer":
                dialogueLines = new List<string>
                {
                    "안녕하시오! 올해 감자 농사가 아주 잘 됐소.",
                    "맛있는 감자튀김을 만들려면 신선한 감자가 필수요!",
                    "땅이 좋고 정성을 들이면, 감자는 배신하지 않소."
                };
                break;

            case "Villager":
                dialogueLines = new List<string>
                {
                    "이 마을은 작지만 따뜻한 곳이야.",
                    "진짜 바삭한 감자튀김을 만드는 비법이 있다던데...",
                    "성문 앞에서 조심해! 아무나 들어갈 수 없는 곳이래."
                };
                break;

            case "Shopkeeper":
                dialogueLines = new List<string>
                {
                    "감자튀김이 필요해? 최고의 감자와 조미료가 준비되어 있지!",
                    "사람들은 감자가 땅속 보물이라던데, 한 번 사볼래?",
                    "좋은 재료가 좋은 감자튀김을 만든다고! 어때?"
                };
                break;

            case "Scientist":
                dialogueLines = new List<string>
                {
                    "감자튀김의 황금비율은 바삭함 60%, 부드러움 40%!",
                    "양자 열파를 이용한 새로운 감자튀김 튀김법을 연구 중이야!",
                    "과학이 미래를 바꾼다! 그리고 감자튀김도 더 맛있게 만들 거야!"
                };
                break;

            case "PotatoBeing":
                dialogueLines = new List<string>
                {
                    "...",
                    "왜 그렇게 쳐다보지? 감자가 말을 하면 이상한가?",
                    "난 한때 너희와 같았지... 하지만 이제 감자의 길을 선택했어."
                };
                break;

            case "Gatekeeper":
                dialogueLines = new List<string>
                {
                    "이 성문을 통과하려면 실력을 증명해야 한다.",
                    "진정한 전사는 도전에 겁먹지 않는다!",
                    "시험을 통과하면 너도 이곳에 들어갈 수 있을 것이다."
                };
                break;

            case "Monster":
                dialogueLines = new List<string>
                {
                    "크르르... 감자튀김 냄새가 너무 맛있군!",
                    "난 바삭한 감자튀김만을 원한다!",
                    "여길 지나가려면 내 도전을 받아들여야 한다!"
                };
                break;
        }
    }

    void Update()
    {
        // 플레이어가 근처에 있고 E 키를 눌렀을 때 대화 시작
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !isTalking)
        {
            StartDialogue();
        }

        // 대화 중이고 Space 키를 누르면 다음 대사 출력
        if (isTalking && Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue();
        }
    }

    void StartDialogue()
    {
        // 대화 UI 또는 대사 리스트가 없으면 실행하지 않음
        if (dialogueUI == null || dialogueText == null || dialogueLines.Count == 0)
        {
            return;
        }

        isTalking = true; // 대화 중 상태로 변경
        dialogueUI.SetActive(true);
        currentLineIndex = 0; // 항상 처음부터 시작
        dialogueText.text = dialogueLines[currentLineIndex];

        if (npcController != null)
        {
            npcController.canMove = false; // 대화 중 NPC 멈춤
        }
    }

    void NextDialogue()
    {
        currentLineIndex++;  // 다음 대사로 이동

        // 남은 대사가 있다면 출력
        if (currentLineIndex < dialogueLines.Count)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
        }
        else
        {
            EndDialogue();  // 모든 대사를 출력한 경우 대화 종료
        }
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);
        isTalking = false; // 대화가 끝나면 다시 대화 가능하도록 초기화

        if (npcController != null)
        {
            npcController.canMove = true; // 대화 종료 후 NPC 이동 재개
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 충돌 범위에 들어오면 대화 가능 상태로 변경
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 범위를 벗어나면 대화 종료
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            EndDialogue();
        }
    }
}
