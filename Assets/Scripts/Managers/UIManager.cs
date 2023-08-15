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
    [SerializeField] private Button backButton;
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private Button quitButton;
    
    [Header("Panel Elements")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject creditsPanel;


    private bool isPaused;
    private bool isActive;

    [Header("Audio Elements")]
    [SerializeField] private AudioSource backgroundMusic1;
    [SerializeField] private AudioSource backgroundMusic2;

    private void Start()
    {
        if (playButton)
            playButton.onClick.AddListener(StartGame);

        if (instructionsButton)
            instructionsButton.onClick.AddListener(Instructions);

        if (creditsButton)
            creditsButton.onClick.AddListener(Credits);

        if (returnToMenuButton)
            returnToMenuButton.onClick.AddListener(BackToMainMenu);

        if (quitButton)
            quitButton.onClick.AddListener(GameQuit);

        if (!backgroundMusic1)
            Debug.Log("Please set Background music file 1");

        if (!backgroundMusic2)
            Debug.Log("Please set Background music file 2");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            PauseGame();
    }

    public void StartGame()
    {
       SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void GameQuit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void Credits()
    {
        creditsPanel.SetActive(true);
        isActive = true;
    }

    private void Instructions()
    {
        instructionsPanel.SetActive(true);
        isActive = true;
    }

    public void GoBack()
    {
        if (isActive)
        { 
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
            Time.timeScale = 1;
            UnpauseBackgorundMusic();

        }
    }

    public void PauseBackgorundMusic()
    {
        backgroundMusic1.Pause();
        backgroundMusic2.Pause();
    }

    public void UnpauseBackgorundMusic()
    {
        backgroundMusic1.UnPause();
        backgroundMusic2.UnPause();
    }

    public void SetInactive()
    {
        GoBack();
    }

}

