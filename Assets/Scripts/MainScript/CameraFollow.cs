using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    private Transform player; // �ʱⰪ ����
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0.02f, -6f, -10f);

    public Vector2 minLimit;
    public Vector2 maxLimit;

    void Start()
    {
        FindPlayer(); // ������ �� �÷��̾� ã��

        // ���� �ε�� ������ �÷��̾� �ٽ� ã��
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindPlayer(); // ���� �ٲ�� �÷��̾� �ٽ� ã��
    }

    void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // �±׷� ã��
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
