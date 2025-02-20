using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public GameObject eventPanel; // 이벤트 판넬
    private bool hasTriggered = false; // 중복 실행 방지

    void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌체가 "Player" 태그 + 이벤트 아직 미실행 시
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;  // 중복 실행 방지
            eventPanel.SetActive(true); 
            Invoke(nameof(HideEvent), 2f);  // 2초 후 자동 비활성화
        }
    }

    void HideEvent()
    {
        // eventPanel이 null이 아닐 경우 비활성화
        if (eventPanel != null)
        {
            eventPanel.SetActive(false);
        }
    }
}
