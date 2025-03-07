using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Signpost : MonoBehaviour
{
    private bool isPlayerNearby = false;  // 표지판 근처에 있는지 여부

    public GameObject signUI; // UI 패널
    public TextMeshProUGUI signUIText; // TMP 텍스트

    private string signText; // 표지판 텍스트

    void Start()
    {
        // 표지판별 문구 자동 설정
        switch (gameObject.name)
        {
            case "FarmerSign":
                signText = "여긴 농부의 집. 신선한 감자는 여기서 시작된다!";
                break;

            case "VillagerSign":
                signText = "이곳은 조용한 마을 주민의 안식처입니다.";
                break;

            case "WellSign":
                signText = "이 우물의 기름으로 튀기면 감자튀김이 더 바삭해진다고 한다.";
                break;

            case "ShopSign":
                signText = "최고급 감자튀김 재료를 팝니다! 신선한 감자, 기름, 조미료까지!";
                break;

            case "LabSign":
                signText = "감자튀김의 미래를 연구하는 곳. 새로운 튀김 기술 개발 중!";
                break;

            case "PotatoBeingSign":
                signText = "감자와 인간의 경계를 넘나드는 존재... 감자인간의 집이다.";
                break;

            case "CastleSign":
                signText = "케찹 공주님이 머무는 성";
                break;
        }
    }

    void Update()
    {
        // 플레이어가 근처에 있음 + E 누를 시
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ShowSign();
        }
    }

    void ShowSign()
    {
        if (signUI == null || signUIText == null)
        {
            return;  // UI 없을 시 실행 X
        }

        signUI.SetActive(true);
        signUIText.text = signText;

        Invoke(nameof(HideSign), 3f); // 3초 후 자동 닫기
    }

    void HideSign()
    {
        if (signUI != null)
        {
            signUI.SetActive(false);
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
            HideSign();
        }
    }
}
