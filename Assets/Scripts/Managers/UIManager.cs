using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class UIManager : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button instructionsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;

    [Header("Panel Elements")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject creditsPanel;

    [Header("Audio Elements")]
    [SerializeField] private AudioSource backgroundMusic;
    

    private bool isPaused;
    private bool isActive;
   
    private void Start()
    {
        if (playButton)
            playButton.onClick.AddListener(StartGame);

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


        if (!backgroundMusic)
            Debug.Log("Please set Background music file 1");

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AudioManager.Instance.Play("Select");
            PauseGame();
        }
     
    }

    public void StartGame()
    {
        AudioManager.Instance.Play("Select");
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

        if (isPaused)
        {
            Time.timeScale = 0;
            PauseBackgorundMusic();
        }

        else
        {
            AudioManager.Instance.Play("Select");
            Time.timeScale = 1;
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

}

