using UnityEngine;
using TMPro;

public class Signpost : MonoBehaviour
{
    public string signText = "Default Sign"; // 표지판 텍스트
    private bool isPlayerNearby = false;

    public GameObject signUI; // UI 패널
    public TextMeshProUGUI signUIText; // TMP 텍스트

    void Start()
    {
        // 폰트가 LiberationSans SDF로 적용되었는지 확인
        if (signUIText != null)
        {
            signUIText.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ShowSign();
        }
    }

    void ShowSign()
    {
        if (signUI == null || signUIText == null)
        {
            Debug.LogError("[Signpost] UI references missing! Check Inspector.");
            return;
        }

        signUI.SetActive(true);
        signUIText.text = signText;

        Invoke(nameof(HideSign), 3f);
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
