using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    public AudioClip mainSceneBGM;  // 메인 씬 BGM
    public AudioClip miniGameBGM;   // 미니게임 씬 BGM
    public AudioClip startBGM;  // 시작 씬 BGM

    private AudioSource audioSource;

    // 씬별 볼륨 설정
    [Range(0f, 1f)] public float mainSceneVolume = 1f;
    [Range(0f, 1f)] public float miniGameVolume = 0.5f;
    [Range(0f, 1f)] public float startVolume = 0.5f;

    void Awake()
    {
        // 중복된 BGMManager 존재 시 삭제
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 변경 시에도 유지
            audioSource = GetComponent<AudioSource>();

            PlayBGM(SceneManager.GetActiveScene().name);
        }
        else
        {
            Destroy(gameObject); // 중복 방지
            return;
        }

        // 씬이 로드될 때마다 새로운 BGM을 재생하도록 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 씬 바뀔 때 자동 호출
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGM(scene.name);
    }

    public void PlayBGM(string sceneName)
    {
        AudioClip clip = null;
        float volume = 1f;

        if (sceneName == "MainScene")
        {
            clip = mainSceneBGM;
            volume = mainSceneVolume;
        }
        else if (sceneName == "MiniGameScene")
        {
            clip = miniGameBGM;
            volume = miniGameVolume;
        }
        else if (sceneName == "StartScene")
        {
            clip = startBGM;
            volume = startVolume;
        }
        
        // 새로운 BGM이 기존 BGM과 다를 경우 변경
        if (clip != null && audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.volume = volume; // 볼륨 설정
            audioSource.Play();
        }
        else
        {
            audioSource.volume = volume; // 같은 BGM이 재생 중이면 볼륨만 변경
        }
    }
}
