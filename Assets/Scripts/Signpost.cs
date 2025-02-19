using UnityEngine;
using TMPro;

public class Signpost : MonoBehaviour
{
    public string signText = "Default Sign"; // ǥ���� �ؽ�Ʈ
    private bool isPlayerNearby = false;

    public GameObject signUI; // UI �г�
    public TextMeshProUGUI signUIText; // TMP �ؽ�Ʈ

    void Start()
    {
        // ��Ʈ�� LiberationSans SDF�� ����Ǿ����� Ȯ��
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
