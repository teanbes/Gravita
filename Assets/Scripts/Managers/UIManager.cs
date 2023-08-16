using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get;  set; }

    [Header("Button")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button instructionsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;

    [Header("Panel Components")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] public GameObject gameOverPanel;
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] public GameObject scorePanel;

    [Header("Audio Components")]
    [SerializeField] private AudioSource backgroundMusic;

    [Header("Score Components")]
    [SerializeField] private TextMeshProUGUI currentScoreText;
    public String scoreText;
    private float score;
    private float scoringTime;
    private float scoreMultiplier = 2.7f;


    private bool isPaused;
    private bool isActive;
    [HideInInspector] public bool isDead;


   
    private void Start()
    {

        if (playButton)
        {
            playButton.onClick.AddListener(() => Invoke("StartGame", 2f));
            playButton.onClick.AddListener(() => AudioManager.Instance.Play("Select"));
        }
             
        if (instructionsButton)
            instructionsButton.onClick.AddListener(Instructions);

        if (creditsButton)
            creditsButton.onClick.AddListener(Credits);

        if (resumeButton)
            resumeButton.onClick.AddListener(PauseGame);

        if (returnToMenuButton)
            returnToMenuButton.onClick.AddListener(GameQuit);

        if (settingsButton)
            settingsButton.onClick.AddListener(GameSettings);

        if (quitButton)
            quitButton.onClick.AddListener(GameQuit);

        if (backButton)
            backButton.onClick.AddListener(BackToPauseMenu);

        if (!backgroundMusic && SceneManager.GetActiveScene().buildIndex == 1)
            Debug.Log("Please set Background music file 1");

        if (SceneManager.GetActiveScene().buildIndex == 2)
            UpdateScoreDisplay();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AudioManager.Instance.Play("Select");
            PauseGame();
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            scoringTime = Time.time;
            IncreaseScore(scoringTime);
            UpdateScoreDisplay();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void GameSettings()
    {
        AudioManager.Instance.Play("Select");
        settingsPanel.SetActive(true);
    }

    private void BackToPauseMenu()
    {
        AudioManager.Instance.Play("Select");
        settingsPanel.SetActive(false);
    }

    public void BackToMainMenu()
    {
        AudioManager.Instance.Play("Select");
        SceneManager.LoadScene(0);
    }
    public void GameQuit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void Credits()
    {
        AudioManager.Instance.Play("Select");
        creditsPanel.SetActive(true);
        isActive = true;
    }

    private void Instructions()
    {
        AudioManager.Instance.Play("Select");
        instructionsPanel.SetActive(true);
        isActive = true;
    }

    public void GoBack()
    {
        if (isActive)
        {
            AudioManager.Instance.Play("Select");
            instructionsPanel.SetActive(false);
            creditsPanel.SetActive(false);
            isActive = false;
        }
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        scorePanel.SetActive(false);

        if (isPaused)
        {
            Time.timeScale = 0;
            PauseBackgorundMusic();
        }

        else
        {
            AudioManager.Instance.Play("Select");
            Time.timeScale = 1;
            scorePanel.SetActive(true);
            UnpauseBackgorundMusic();
        }
    }

    public void PauseBackgorundMusic()
    {
        backgroundMusic.Pause();
    }

    public void UnpauseBackgorundMusic()
    {
        backgroundMusic.UnPause();
    }

    public void SetInactive()
    {
        GoBack();
    }

    //Score Manager functions
    public void IncreaseScore(float amount)
    {
        score = (Mathf.Round(amount * scoreMultiplier)) ;
    }
    public void UpdateScoreDisplay()
    {
        scoreText = score.ToString();
        currentScoreText.text = scoreText;
    }

    public void PlayerDeath()
    {
      
    }

}

