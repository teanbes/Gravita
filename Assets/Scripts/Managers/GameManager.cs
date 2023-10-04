using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

[DefaultExecutionOrder(1)]
public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    public static GameManager instance
    {
        get => _instance;
    }

    [Header("Components")]
    [SerializeField] private PlayerController playerPrefab;

    [HideInInspector] public String scoreText;
    [HideInInspector] public float scoringTime;
    [HideInInspector] public bool isGameStarted;
    [HideInInspector] public int difficulty;
    [HideInInspector] public int numDifficultyLevels = 3; // test value
    private float score;
    private float scoreMultiplier = 2.7f;

    public int coins;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        difficulty = 1;
    }


    // Update is called once per frame
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && isGameStarted)
        {
            scoringTime += Time.deltaTime;
            score = (Mathf.Round(scoringTime * scoreMultiplier));
            scoreText = score.ToString();
            DifficultyLevel();
            Debug.Log("difficulty" + difficulty);

        }
    }

    private void DifficultyLevel()
    {
        if (score > 10 && score < 20) // temp values for testing
            difficulty = 2;

        if (score >=20) // temp values for testing
            difficulty = 3;
    }

}
