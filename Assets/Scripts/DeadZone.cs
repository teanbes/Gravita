using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour
{

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<PlayerController>())
        {
            var playerController = collision.GetComponent<PlayerController>();
            playerController.isDead = true;
            playerController.rb.gravityScale = 0;
        }
    }
}
