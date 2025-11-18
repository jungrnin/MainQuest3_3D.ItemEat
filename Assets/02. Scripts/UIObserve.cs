using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIObserve : MonoBehaviour
{
    public static UIObserve Instance;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI keyAppearText;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject clearPanel;

    

    private void Awake()
    {
        Instance = this;

        keyAppearText.gameObject.SetActive(false);

        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false); 
        clearPanel.SetActive(false);
    }

    private void OnEnable()
    {
        Timer.OnTimeChanged += UpdateTime;
        Timer.OnTimeOver += ShowGameOver;

        ScoreItemManager.OnScoreChanged += UpdateScore;
        ScoreItemManager.OnKeySpawn += ShowKeyAppear;
    }
    private void OnDisable()
    {
        Timer.OnTimeChanged -= UpdateTime;
        Timer.OnTimeOver -= ShowGameOver;

        ScoreItemManager.OnScoreChanged -= UpdateScore;
        ScoreItemManager.OnKeySpawn -= ShowKeyAppear;
    }

    private void UpdateTime(float time)
    {
        timeText.text = $"Time : {time : 0}";
    }
    private void UpdateScore(int current, int total)
    {
        scoreText.text = $"{current} / {total}";
    }

    private void ShowKeyAppear()
    {
        StartCoroutine(KeyAppearRoutine());
    }

    private IEnumerator KeyAppearRoutine()
    {
        keyAppearText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        keyAppearText.gameObject.SetActive(false);
    }
    
    public void ShowPause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SFXManager.Instance.StopBGM();
    }

    public void HidePause()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SFXManager.Instance.PlayBGM();
    }

    public void ShowGameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SFXManager.Instance.StopBGM();
    }
    
    public void ShowClear()
    {
        Time.timeScale = 0;
        clearPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SFXManager.Instance.StopBGM();
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
