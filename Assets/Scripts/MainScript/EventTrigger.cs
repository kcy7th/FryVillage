using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public GameObject eventPanel; // �̺�Ʈ �ǳ�
    private bool hasTriggered = false; // �ߺ� ���� ����

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            eventPanel.SetActive(true); 
            Invoke(nameof(HideEvent), 2f); 
        }
    }

    void HideEvent()
    {
        if (eventPanel != null)
        {
            eventPanel.SetActive(false);
        }
    }
}
