using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

[DefaultExecutionOrder(1)]
public class GameManager : MonoBehaviour
{

    private static GameManager _instance = null;

    public static GameManager instance
    {
        get => _instance;
    }

    public PlayerController playerPrefab;
    [HideInInspector] public PlayerController playerInstance = null;
    [HideInInspector] public Transform currentSpawnPoint;

    [HideInInspector] public String scoreText;
    [HideInInspector] public float scoringTime;
    private float score;
    private float scoreMultiplier = 2.7f;


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

   
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            scoringTime += Time.deltaTime;
            score = (Mathf.Round(scoringTime * scoreMultiplier));
            scoreText = score.ToString();

        }
    }
}
