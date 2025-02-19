using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    public GameObject gameIntroPanel; // 게임 시작 UI
    public GameObject gameOverPanel; // 게임 오버 UI
    public GameObject player; // 플레이어 오브젝트
    public TextMeshProUGUI currentScoreText; // 현재 점수 UI
    public TextMeshProUGUI highScoreText; // 최고 점수 UI

    private int currentScore = 0;
    private int highScore = 0;
    UIManager uiManager;

    public static GameManager Instance
    {
        get { return gameManager; }
    }

    public UIManager UIManager
    {
        get { return uiManager; }
    }

    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<UIManager>();

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Start()
    {
        uiManager.UpdateScore(0);
        ShowGameIntro();
    }
    void ShowGameIntro()
    {
        gameIntroPanel.SetActive(true); // 게임 시작 패널 활성화
        gameOverPanel.SetActive(false); // 게임 오버 패널 비활성화
        player.SetActive(false); // 게임 시작 전 플레이어 비활성화
    }

    public void StartGame()
    {
        gameIntroPanel.SetActive(false); // 게임 시작 패널 숨김
        player.SetActive(true); // 플레이어 활성화
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        gameOverPanel.SetActive(true); // 게임 오버 패널 활성화

        // 최고 점수 갱신
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        // 점수 UI 업데이트
        currentScoreText.text = "현재 점수: " + currentScore;
        highScoreText.text = "최고 점수: " + highScore;

        uiManager.SetRestart();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        PlayerPrefs.Save(); // 최고 점수 저장

        // 씬 이동 전에 모든 게임 오브젝트 정리 (DontDestroyOnLoad 방지)
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.scene.name != "DontDestroyOnLoad")
            {
                Destroy(obj);
            }
        }

        SceneManager.LoadScene("MainScene"); // 메인 씬으로 이동
    }

    public void AddScore(int score)
    {
        currentScore += score;
        uiManager.UpdateScore(currentScore);
        Debug.Log("Score: " + currentScore);
    }

}