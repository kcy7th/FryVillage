using UnityEngine;

public class NPCController : MonoBehaviour
{
    public enum NPCType { Monster, Farmer }
    public NPCType npcType;

    public bool canMove = true; // ��ȭ �� �̵� ����
    public float moveSpeed = 2f; // �̵� �ӵ�
    public float waitTime = 2f; // �̵� �� ��� �ð�

    public Vector2 moveAreaMin, moveAreaMax; // ���� �̵� ����
    public Transform[] waypoints; // ��� �̵� ���

    private Vector2 targetPosition;
    private int currentWaypoint = 0;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (npcType == NPCType.Monster)
        {
            InvokeRepeating(nameof(ChangePosition), 0f, waitTime);
        }
    }

    void Update()
    {
        if (!canMove) return; // ��ȭ ���̸� �̵� X

        if (npcType == NPCType.Monster)
            MoveToTarget();
        else if (npcType == NPCType.Farmer)
            FollowWaypoints();
    }

    // ����: �ٿ���� �� ���� �̵�
    void ChangePosition()
    {
        targetPosition = new Vector2(
            Random.Range(moveAreaMin.x, moveAreaMax.x),
            Random.Range(moveAreaMin.y, moveAreaMax.y)
        );
    }

    void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        Vector2 direction = targetPosition - (Vector2)transform.position;

        // ���������� ���ο� ��ǥ ��ġ ����
        if (direction.magnitude < 0.1f)
            ChangePosition();

        UpdateAnimation(direction);
    }

    // ���: ������ ��� �̵�
    void FollowWaypoints()
    {
        if (waypoints.Length == 0) return;

        Transform targetPoint = waypoints[currentWaypoint];
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        Vector2 direction = targetPoint.position - transform.position;
        UpdateAnimation(direction);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    // ���� Ʈ��
    void UpdateAnimation(Vector2 direction)
    {
        animator.SetFloat("moveX", direction.normalized.x);
        animator.SetFloat("moveY", direction.normalized.y);
        animator.SetBool("isWalking", direction.sqrMagnitude > 0.01f);

        // ���� �̵� �� FlipX ����
        spriteRenderer.flipX = direction.x < 0;
    }
}
