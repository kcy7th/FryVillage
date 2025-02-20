using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    public AudioClip mainSceneBGM;  // ���� �� BGM
    public AudioClip miniGameBGM;   // �̴ϰ��� �� BGM
    public AudioClip startBGM;  // ���� �� BGM

    private AudioSource audioSource;

    // ���� ���� ����
    [Range(0f, 1f)] public float mainSceneVolume = 1f;
    [Range(0f, 1f)] public float miniGameVolume = 0.5f;
    [Range(0f, 1f)] public float startVolume = 0.5f;

    void Awake()
    {
        // �ߺ��� BGMManager ���� �� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // �� ���� �ÿ��� ����
            audioSource = GetComponent<AudioSource>();

            PlayBGM(SceneManager.GetActiveScene().name);
        }
        else
        {
            Destroy(gameObject); // �ߺ� ����
            return;
        }

        // ���� �ε�� ������ ���ο� BGM�� ����ϵ��� �̺�Ʈ ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // �� �ٲ� �� �ڵ� ȣ��
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
        
        // ���ο� BGM�� ���� BGM�� �ٸ� ��� ����
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
