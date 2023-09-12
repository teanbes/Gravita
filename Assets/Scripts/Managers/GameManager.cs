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

    [Header("Components")]
    [SerializeField] private PlayerController playerPrefab;
   

    [HideInInspector] public String scoreText;
    [HideInInspector] public float scoringTime;
    [HideInInspector] public bool isGameStarted;
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

   
    // Update is called once per frame
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && isGameStarted)
        {
            scoringTime += Time.deltaTime;
            score = (Mathf.Round(scoringTime * scoreMultiplier));
            scoreText = score.ToString();

        }

       // if (tapToStartPanel && isGameStarted)
       // {
       //     playerPrefab.isStarted = true;
       //     tapToStartPanel.SetActive(false);
       //     playerPrefab.animator.SetBool("IsRunning", true);
       //     movingLevelGenerator.objectSpeed = levelSpeed;
       //     GameManager.instance.uiManager.UnpauseBackgorundMusic();
       //     
       // }
    }


}
