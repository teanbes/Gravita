using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private int coinsAmount;
    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private int maxCoins = 3;
    [SerializeField] private int minCoins = 9;


    // Start is called before the first frame update
    void Start()
    {
        coinsAmount = Random.Range(minCoins, maxCoins);
        int extraOffset = coinsAmount / 2;

        for (int i = 0; i < coinsAmount; i++)
        {
            Vector3 coinInstanceOffset = new Vector2(i- extraOffset, 0);
            Instantiate(coinPrefab, transform.position + coinInstanceOffset, Quaternion.identity, transform );
        }
    }

}
