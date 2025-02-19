using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    public GameObject gameIntroPanel; // ���� ���� UI
    public GameObject gameOverPanel; // ���� ���� UI
    public GameObject player; // �÷��̾� ������Ʈ
    public TextMeshProUGUI currentScoreText; // ���� ���� UI
    public TextMeshProUGUI highScoreText; // �ְ� ���� UI

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
        gameIntroPanel.SetActive(true); // ���� ���� �г� Ȱ��ȭ
        gameOverPanel.SetActive(false); // ���� ���� �г� ��Ȱ��ȭ
        player.SetActive(false); // ���� ���� �� �÷��̾� ��Ȱ��ȭ
    }

    public void StartGame()
    {
        gameIntroPanel.SetActive(false); // ���� ���� �г� ����
        player.SetActive(true); // �÷��̾� Ȱ��ȭ
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        gameOverPanel.SetActive(true); // ���� ���� �г� Ȱ��ȭ

        // �ְ� ���� ����
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        // ���� UI ������Ʈ
        currentScoreText.text = "���� ����: " + currentScore;
        highScoreText.text = "�ְ� ����: " + highScore;

        uiManager.SetRestart();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        PlayerPrefs.Save(); // �ְ� ���� ����

        // �� �̵� ���� ��� ���� ������Ʈ ���� (DontDestroyOnLoad ����)
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.scene.name != "DontDestroyOnLoad")
            {
                Destroy(obj);
            }
        }

        SceneManager.LoadScene("MainScene"); // ���� ������ �̵�
    }

    public void AddScore(int score)
    {
        currentScore += score;
        uiManager.UpdateScore(currentScore);
        Debug.Log("Score: " + currentScore);
    }

}