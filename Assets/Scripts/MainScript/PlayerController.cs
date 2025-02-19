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
            // Idle ���¿����� ������ �̵� ���� ����
            animator.SetFloat("moveX", lastMoveDirection.x);
            animator.SetFloat("moveY", lastMoveDirection.y);
            animator.SetBool("isWalking", false);
        }
    }

    void FixedUpdate()
    {
        // Rigidbody2D ��� �̵� (Collider �浹 �ݿ�)
        rb.velocity = movement.normalized * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"�÷��̾ {collision.gameObject.name}�� �浹��!");
    }

}
