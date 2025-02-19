using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public float smoothSpeed = 5f; // ī�޶� �̵� �ӵ�
    public Vector3 offset = new Vector3(0f, 0f, -10f); // ī�޶� ��ġ ������

    public Vector2 minLimit; // �ּ� ��ǥ
    public Vector2 maxLimit; // �ִ� ��ǥ

    void LateUpdate()
    {
        if (player == null) return; // �÷��̾ ������ �������� ����

        Vector3 targetPosition = player.position + offset;

        // ī�޶� �̵� ���� ����
        targetPosition.x = Mathf.Clamp(targetPosition.x, minLimit.x, maxLimit.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minLimit.y, maxLimit.y);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
