using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastMoveDirection = new Vector2(0, 1);

    private Vector3 savedPlayerPosition; // 플레이어 위치 저장 변수
    private bool hasSavedPosition = false; // 위치 저장 여부 체크

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            return; // StartScene에서는 실행하지 않음
        }

        if (FindObjectsOfType<PlayerController>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        SceneManager.sceneLoaded += OnSceneLoaded;
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
        rb.velocity = movement.normalized * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"플레이어가 {collision.gameObject.name}과 충돌함!");
    }

    public void SavePlayerPosition()
    {
        savedPlayerPosition = transform.position;
        hasSavedPosition = true;
    }

    // 메인 씬으로 돌아올 때 위치 복원
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StartScene")
        {
            return; // StartScene에서는 실행하지 않음
        }

        Debug.Log($"씬 로드됨: {scene.name}");

        if (scene.name == "MainScene" && hasSavedPosition)
        {
            transform.position = savedPlayerPosition;
        }
    }

    // 미니 게임으로 이동할 때 호출
    public void GoToMiniGame()
    {
        SavePlayerPosition(); // 위치 저장
        SceneManager.LoadScene("MiniGameScene");
    }

    // 미니 게임 종료 후 메인 씬으로 이동
    public void ReturnToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
