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
    public int index;

}

//public struct InventoryColor
//{
//    public Color color;
//    public int price;

//}

public class UIShop : MonoBehaviour
{

    [SerializeField] private CollorToSell[] skinColors;
    [SerializeField] private GameObject playerSkinButton;
    [SerializeField] private Transform playerSkinParent;
    [SerializeField] private SpriteRenderer playerDisplay;

    private void Start()
    {
        LoadBoughtSkins(); // Load the bought skins at the beginning
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < skinColors.Length; i++)
        {
            Color color = skinColors[i].color;
            int price = PlayerPrefs.GetInt(GetPriceKey(color), skinColors[i].price); // Use saved price or default price
            int index = skinColors[i].index;
            
            GameObject newButton = Instantiate(playerSkinButton, playerSkinParent);

            newButton.transform.GetChild(0).GetComponent<Image>().color = color;
            newButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = price.ToString("#,#");

            if (price == 0)
            {
                newButton.transform.GetChild(2).gameObject.SetActive(false);
            }

            newButton.GetComponent<Button>().onClick.AddListener(() => PurchaseColor(color, price, index));
        }

        playerDisplay.color = GameManager.instance.playerSkinColor;
    }

    private void PurchaseColor(Color color, int price, int index)
    {
        if (EnoughMoney(price))
        {
            // Mark the skin as bought before deducting coins
            PlayerPrefs.SetInt(GetSkinKey(color), 1);

            GameManager.instance.playerSkinColor = color;
            playerDisplay.color = color;
            GameManager.instance.Save(color.r, color.g, color.b);

            // Set the price to 0 for the bought skin
            PlayerPrefs.SetInt(GetPriceKey(color), 0);


            // Deduct coins after marking the skin as bought
            int newBalanceOfCoins = PlayerPrefs.GetInt("TotalGameCoins", 0) - price;
            GameManager.instance.totalGameCoins = newBalanceOfCoins;

            // Update UI after purchase
            UpdateSkinUI(color, 0);

            skinColors[index].price = 0;
            

            Button buttonComponent = FindButtonByColor(index);
            if (buttonComponent != null)
            {
                Debug.Log($"Skin color new price: {buttonComponent.GetInstanceID()}");

                int newprice = PlayerPrefs.GetInt(GetPriceKey(color), skinColors[index].price);

                buttonComponent.onClick.RemoveAllListeners();
                Debug.Log("Removing listeners");
                buttonComponent.onClick.AddListener(() => PurchaseColor(color, newprice, index));
            }

            // Purchased successful
        }
        // Not enough coins
    }

    private bool EnoughMoney(int price)
    {
        int coinsBalance = PlayerPrefs.GetInt("TotalGameCoins", 0);

        if (coinsBalance >= price)
        {
            int newBalanceOfCoins = coinsBalance - price;
            GameManager.instance.totalGameCoins = newBalanceOfCoins;

            // Purchased successful
            return true;
        }

        // Not enough coins
        return false;
    }

    private void LoadBoughtSkins()
    {
        for (int i = 0; i < skinColors.Length; i++)
        {
            Color color = skinColors[i].color;
            string key = GetSkinKey(color);

            // Check if the skin is bought
            if (PlayerPrefs.GetInt(key, 0) == 1)
            {
                GameManager.instance.playerSkinColor = color;
                playerDisplay.color = color;
            }
        }
    }

    private void UpdateSkinUI(Color color, int price)
    {
        foreach (Transform buttonTransform in playerSkinParent)
        {
            Color buttonColor = buttonTransform.GetChild(0).GetComponent<Image>().color;
            if (buttonColor.Equals(color))
            {
                // Reload the saved price for the purchased skin
                int updatedPrice = PlayerPrefs.GetInt(GetPriceKey(color), price);

                buttonTransform.GetChild(1).GetComponent<TextMeshProUGUI>().text = updatedPrice.ToString("#,#");
                buttonTransform.GetChild(2).gameObject.SetActive(false);
                
                break;
            }
        }
    }

    private Button FindButtonByColor(int index)
    {
        Button button = playerSkinParent.GetChild(index).GetComponent<Button>();
            if (button)
            {
                return button;
            } return null;
    }

    private string GetSkinKey(Color color)
    {
        return $"Skin_{color.r}_{color.g}_{color.b}";
    }

    private string GetPriceKey(Color color)
    {
        return $"SkinPrice_{color.r}_{color.g}_{color.b}";
    }

    public void ResetAllSavedValues()
    {
        PlayerPrefs.DeleteAll();
    }

    //[SerializeField] private CollorToSell[] skinColors;
    //[SerializeField] private GameObject playerSkinButton;
    //[SerializeField] private Transform  playerSkinParent;
    //[SerializeField] private SpriteRenderer playerDisplay;

    ////private InventoryColor[] inventory;
    ////private int amountOfSkinsBought;

    //private void Awake()
    //{
    //    //amountOfSkinsBought = GameManager.instance.amountOfSkinsOwned;
    //}

    //// Start is called before the first frame update
    //void Start()
    //{
    //    for (int i = 0; i < skinColors.Length; i++) 
    //    {
    //        Color color = skinColors[i].color;
    //        int price = skinColors[i].price;

    //        GameObject newButton = Instantiate(playerSkinButton, playerSkinParent);

    //        newButton.transform.GetChild(0).GetComponent<Image>().color = color;
    //        newButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = price.ToString("#,#");

    //        newButton.GetComponent<Button>().onClick.AddListener(() => PurchaseColor(color, price));
    //    }

    //    playerDisplay.color = GameManager.instance.playerSkinColor;

    //}

    //public void PurchaseColor(Color color, int price)
    //{
    //    if (EnoughMoney(price)) 
    //    { 
    //        GameManager.instance.playerSkinColor = color;
    //        playerDisplay.color = color;
    //        GameManager.instance.Save(color.r, color.g, color.b);
    //    }
    //}

    //private bool EnoughMoney(int price )
    //{
    //    int coinsBalance = PlayerPrefs.GetInt("TotalGameCoins", 0);

    //    if (coinsBalance > price) 
    //    {
    //        int newBalanceOfCoins = coinsBalance - price;
    //        GameManager.instance.totalGameCoins = newBalanceOfCoins;

    //        // Purchased successful
    //        return true;
    //    }

    //    // Not enough coins
    //    return false;
    //}


}
