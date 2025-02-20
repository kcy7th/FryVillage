using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastMoveDirection = new Vector2(0, 1);  // �⺻ ���� ��

    private Vector3 savedPlayerPosition; // ����� �÷��̾� ��ġ
    private bool hasSavedPosition = false; // ��ġ ���� ����

    private void Awake()
    {
        // StartScene������ �������� ����
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            return;
        }

        // �ߺ��� PlayerController�� ������ ��� ���� ������Ʈ�� ����
        if (FindObjectsOfType<PlayerController>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            // ���� ����Ǿ �ı����� �ʵ��� ����
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // �� �ε� �̺�Ʈ
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        // Ű �Է� �ޱ�
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // �̵� ���̸� ���� ��� �ݿ�
        if (movement.sqrMagnitude > 0.01f)
        {
            lastMoveDirection = movement.normalized;  // ������ �̵� ���� ����
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
        // �̵� ���⿡ ���� �ӵ��� ����
        rb.velocity = movement.normalized * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"�÷��̾ {collision.gameObject.name}�� �浹��!");
    }

    // �÷��̾� ��ġ ����
    public void SavePlayerPosition()
    {
        savedPlayerPosition = transform.position;
        hasSavedPosition = true;
    }

    // ���� ������ ���ƿ� �� ��ġ ����
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StartScene")
        {
            return; // StartScene������ �������� ����
        }

        Debug.Log($"�� �ε��: {scene.name}");

        if (scene.name == "MainScene" && hasSavedPosition)
        {
            transform.position = savedPlayerPosition;
        }
    }

    // �̴� �������� �̵��� �� ȣ��
    public void GoToMiniGame()
    {
        SavePlayerPosition(); // ��ġ ����
        SceneManager.LoadScene("MiniGameScene");
    }

    // �̴� ���� ���� �� ���� ������ �̵�
    public void ReturnToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    // ������Ʈ�� ������ �� �̺�Ʈ ��� ����
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
