using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    private Transform player; // 초기값 제거
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0.02f, -6f, -10f);

    public Vector2 minLimit;
    public Vector2 maxLimit;

    void Start()
    {
        FindPlayer(); // 시작할 때 플레이어 찾기

        // 씬이 로드될 때마다 플레이어 다시 찾기
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
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
        if (player == null) return;

        Vector3 targetPosition = player.position + offset;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minLimit.x, maxLimit.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minLimit.y, maxLimit.y);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
