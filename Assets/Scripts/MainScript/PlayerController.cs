using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastMoveDirection = new Vector2(0, 1);

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
            // Idle 상태에서도 마지막 이동 방향 유지
            animator.SetFloat("moveX", lastMoveDirection.x);
            animator.SetFloat("moveY", lastMoveDirection.y);
            animator.SetBool("isWalking", false);
        }
    }

    void FixedUpdate()
    {
        // Rigidbody2D 기반 이동 (Collider 충돌 반영)
        rb.velocity = movement.normalized * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"플레이어가 {collision.gameObject.name}과 충돌함!");
    }

}
