using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Signpost : MonoBehaviour
{
    private bool isPlayerNearby = false;

    public GameObject signUI; // UI �г�
    public TextMeshProUGUI signUIText; // TMP �ؽ�Ʈ

    private string signText; // ǥ���� �ؽ�Ʈ

    void Start()
    {
        // ǥ���Ǻ� ���� �ڵ� ����
        switch (gameObject.name)
        {
            case "FarmerSign":
                signText = "���� ����� ��. �ż��� ���ڴ� ���⼭ ���۵ȴ�!";
                break;

            case "VillagerSign":
                signText = "�̰��� ������ ���� �ֹε��� �Ƚ�ó�Դϴ�.";
                break;

            case "WellSign":
                signText = "�� �칰�� ���� Ƣ��� ����Ƣ���� �� �ٻ������ٰ� �Ѵ�.";
                break;

            case "ShopSign":
                signText = "�ְ�� ����Ƣ�� ��Ḧ �˴ϴ�! �ż��� ����, �⸧, ���̷����!";
                break;

            case "LabSign":
                signText = "����Ƣ���� �̷��� �����ϴ� ��. ���ο� Ƣ�� ��� ���� ��!";
                break;

            case "PotatoBeingSign":
                signText = "���ڿ� �ΰ��� ��踦 �ѳ���� ����... �����ΰ��� ���̴�.";
                break;

            case "CastleSign":
                signText = "�̰��� �������� ������ ����ؾ� �Ѵ�. �غ�ƴ°�?";
                break;

            default:
                signText = "�� ǥ���ǿ��� �ƹ��͵� ���� ���� �ʴ�.";
                break;
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
            return;
        }

        signUI.SetActive(true);
        signUIText.text = signText;

        Invoke(nameof(HideSign), 5f); // 3�� �� �ڵ� �ݱ�
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
