using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            AudioManager.Instance.Play("Coin");
            GameManager.instance.levelCoins++;
            Destroy(gameObject);

        }
    }
}
