using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour
{
    private UIManager uiManager;
    private GameObject score;
    private TextMeshProUGUI scoretext;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            uiManager.PauseGame();
            uiManager.scorePanel.SetActive(false);
            uiManager.gameOverPanel.SetActive(true);

            score = GameObject.FindWithTag("Score");
            scoretext = score.GetComponent<TextMeshProUGUI>();
            scoretext.text = uiManager.scoreText;

        }
    }
}
