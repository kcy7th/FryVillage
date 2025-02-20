using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastMoveDirection = new Vector2(0, 1);  // 기본 방향 값

    private Vector3 savedPlayerPosition; // 저장된 플레이어 위치
    private bool hasSavedPosition = false; // 위치 저장 여부

    private void Awake()
    {
        // StartScene에서는 실행하지 않음
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            return;
        }

        // 중복된 PlayerController가 존재할 경우 현재 오브젝트를 삭제
        if (FindObjectsOfType<PlayerController>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            // 씬이 변경되어도 파괴되지 않도록 설정
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 씬 로드 이벤트
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        // 키 입력 받기
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // 이동 중이면 방향 즉시 반영
        if (movement.sqrMagnitude > 0.01f)
        {
            lastMoveDirection = movement.normalized;  // 마지막 이동 방향 저장
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
        // 이동 방향에 따라 속도를 설정
        rb.velocity = movement.normalized * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"플레이어가 {collision.gameObject.name}과 충돌함!");
    }

    // 플레이어 위치 저장
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

    // 오브젝트가 삭제될 때 이벤트 등록 해제
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
