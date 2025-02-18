using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastMoveDirection = new Vector2(0, -1); // 기본값: 아래 방향

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 키 입력 받기
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // 이동 중이면 방향 즉시 반영 & 마지막 이동 방향 저장
        if (movement.sqrMagnitude > 0.01f)
        {
            lastMoveDirection = movement.normalized; // 마지막 이동 방향 저장
            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);
            animator.SetBool("isWalking", true);
        }
        else
        {
            // Idle 상태에서도 마지막 이동 방향을 유지
            animator.SetFloat("moveX", lastMoveDirection.x);
            animator.SetFloat("moveY", lastMoveDirection.y);
            animator.SetBool("isWalking", false);
        }

        // 왼쪽 이동 시 FlipX 적용 (Idle 상태에서도 유지)
        if (lastMoveDirection.x < 0)
            spriteRenderer.flipX = true;
        else if (lastMoveDirection.x > 0)
            spriteRenderer.flipX = false;
    }

    void FixedUpdate()
    {
        rb.velocity = movement.normalized * moveSpeed;
    }
}
