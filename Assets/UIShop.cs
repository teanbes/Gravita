using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public struct CollorToSell
{
    public Color color;
    public int price;

}

public class UIShop : MonoBehaviour
{
    [SerializeField] private CollorToSell[] skinColors;
    [SerializeField] private GameObject playerSkinButton;
    [SerializeField] private Transform  playerSkinParent;
    [SerializeField] private SpriteRenderer playerDisplay;

    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < skinColors.Length; i++) 
        {
            Color color = skinColors[i].color;
            int price = skinColors[i].price;

            GameObject newButton = Instantiate(playerSkinButton, playerSkinParent);

            newButton.transform.GetChild(0).GetComponent<Image>().color = color;
            newButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = price.ToString("#,#");

            newButton.GetComponent<Button>().onClick.AddListener(() => PurchaseColor(color, price));
        }

        playerDisplay.color = GameManager.instance.playerSkinColor;
    }

    public void PurchaseColor(Color color, int price)
    {
        if (EnoughMoney(price)) 
        { 
            GameManager.instance.playerSkinColor = color;
            playerDisplay.color = color;
            GameManager.instance.Save(color.r, color.g, color.b);
        }
    }

    private bool EnoughMoney(int price )
    {
        int coinsBalance = PlayerPrefs.GetInt("TotalGameCoins", 0);

        if (coinsBalance > price) 
        {
            int newBalanceOfCoins = coinsBalance - price;
            GameManager.instance.totalGameCoins = newBalanceOfCoins;

            // Purchased successful
            return true;
        }

        // Not enough coins
        return false;
    }

   
}
