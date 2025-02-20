using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    public AudioClip mainSceneBGM;  // ���� �� BGM
    public AudioClip miniGameBGM;   // �̴ϰ��� �� BGM
    public AudioClip startBGM;

    private AudioSource audioSource;

    // ���� ���� ����
    [Range(0f, 1f)] public float mainSceneVolume = 1f;
    [Range(0f, 1f)] public float miniGameVolume = 0.5f;
    [Range(0f, 1f)] public float startVolume = 1f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); // �ߺ� ����
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

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

        if (clip != null && audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.volume = volume; // ���� ����
            audioSource.Play();
        }
        else
        {
            audioSource.volume = volume; // ���� BGM�� ��� ���̸� ������ ����
        }
    }
}
