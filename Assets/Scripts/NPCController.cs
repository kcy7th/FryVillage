using UnityEngine;

public class NPCController : MonoBehaviour
{
    public enum NPCType { Monster, Farmer }
    public NPCType npcType;

    public bool canMove = true; // 대화 중 이동 제한
    public float moveSpeed = 2f; // 이동 속도
    public float waitTime = 2f; // 이동 후 대기 시간

    public Vector2 moveAreaMin, moveAreaMax; // 몬스터 이동 범위
    public Transform[] waypoints; // 농부 이동 경로

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
        if (!canMove) return; // 대화 중이면 이동 X

        if (npcType == NPCType.Monster)
            MoveToTarget();
        else if (npcType == NPCType.Farmer)
            FollowWaypoints();
    }

    // 몬스터: 바운더리 내 랜덤 이동
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

        // 도착했으면 새로운 목표 위치 설정
        if (direction.magnitude < 0.1f)
            ChangePosition();

        UpdateAnimation(direction);
    }

    // 농부: 정해진 경로 이동
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

    // 블렌드 트리
    void UpdateAnimation(Vector2 direction)
    {
        animator.SetFloat("moveX", direction.normalized.x);
        animator.SetFloat("moveY", direction.normalized.y);
        animator.SetBool("isWalking", direction.sqrMagnitude > 0.01f);

        // 왼쪽 이동 시 FlipX 적용
        spriteRenderer.flipX = direction.x < 0;
    }
}
