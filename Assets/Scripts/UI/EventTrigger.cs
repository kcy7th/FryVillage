using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public GameObject eventPanel; // �̺�Ʈ �ǳ�
    private bool hasTriggered = false; // �ߺ� ���� ����

    void OnTriggerEnter2D(Collider2D other)
    {
        // �浹ü�� "Player" �±� + �̺�Ʈ ���� �̽��� ��
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;  // �ߺ� ���� ����
            eventPanel.SetActive(true); 
            Invoke(nameof(HideEvent), 2f);  // 2�� �� �ڵ� ��Ȱ��ȭ
        }
    }

    void HideEvent()
    {
        // eventPanel�� null�� �ƴ� ��� ��Ȱ��ȭ
        if (eventPanel != null)
        {
            eventPanel.SetActive(false);
        }
    }
}
