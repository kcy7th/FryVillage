using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    private Transform player; // 플레이어 Transform
    public float smoothSpeed = 5f;  // 카메라 이동 속도
    public Vector3 offset = new Vector3(0.02f, -6f, -10f);  // 위치 오프셋

    public Vector2 minLimit;  // 이동 제한 최소 값
    public Vector2 maxLimit;  // 이동 제한 최대 값

    void Start()
    {
        FindPlayer(); // 시작할 때 플레이어 찾기

        // 씬이 로드될 때마다 플레이어 다시 찾기
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // 씬 이동 시 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindPlayer(); // 씬이 바뀌면 플레이어 다시 찾기
    }

    void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // 태그로 찾기
    }

    void LateUpdate()
    {
        // 플레이어가 없으면 실행하지 않음
        if (player == null) return;

        // 목표 위치 설정 (플레이어 위치 + 오프셋)
        Vector3 targetPosition = player.position + offset;

        // 목표 위치 설정 (플레이어 위치 + 오프셋)
        targetPosition.x = Mathf.Clamp(targetPosition.x, minLimit.x, maxLimit.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minLimit.y, maxLimit.y);

        // 부드러운 이동
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
