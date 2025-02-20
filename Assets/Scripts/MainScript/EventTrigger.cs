using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public GameObject eventPanel; // 이벤트 판넬
    private bool hasTriggered = false; // 중복 실행 방지

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
