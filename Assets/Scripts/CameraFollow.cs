using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public float smoothSpeed = 5f; // 카메라 이동 속도
    public Vector3 offset = new Vector3(0f, 0f, -10f); // 카메라 위치 오프셋

    public Vector2 minLimit; // 최소 좌표
    public Vector2 maxLimit; // 최대 좌표

    void LateUpdate()
    {
        if (player == null) return; // 플레이어가 없으면 실행하지 않음

        Vector3 targetPosition = player.position + offset;

        // 카메라 이동 제한 적용
        targetPosition.x = Mathf.Clamp(targetPosition.x, minLimit.x, maxLimit.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minLimit.y, maxLimit.y);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
