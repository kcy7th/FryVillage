using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastMoveDirection = new Vector2(0, -1); // �⺻��: �Ʒ� ����

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Ű �Է� �ޱ�
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // �̵� ���̸� ���� ��� �ݿ� & ������ �̵� ���� ����
        if (movement.sqrMagnitude > 0.01f)
        {
            lastMoveDirection = movement.normalized; // ������ �̵� ���� ����
            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);
            animator.SetBool("isWalking", true);
        }
        else
        {
            // Idle ���¿����� ������ �̵� ������ ����
            animator.SetFloat("moveX", lastMoveDirection.x);
            animator.SetFloat("moveY", lastMoveDirection.y);
            animator.SetBool("isWalking", false);
        }

        // ���� �̵� �� FlipX ���� (Idle ���¿����� ����)
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
