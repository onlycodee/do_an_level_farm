using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI sellPriceText, buyPriceText, itemName;
    [SerializeField] Image avatar;
    [SerializeField] Button buyButton;
    [SerializeField] GameObject lockMask;
    [SerializeField] TextMeshProUGUI levelOpenTxt;

    Item item;
    CoinManager coinManager;


    private void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
        SeedItem seedItem = item as SeedItem;
        if (seedItem)
        {
            sellPriceText.text = seedItem.GetSellPrice().ToString();
            buyPriceText.text = seedItem.GetBuyPrice().ToString();
            itemName.text = seedItem.name;
            avatar.sprite = seedItem.Avatar;
            if (seedItem.GetUnlockLevel() <= LevelManager.Instance.GetCurrentLevel())
            {
                lockMask.SetActive(false);
            } else
            {
                lockMask.SetActive(true);
                levelOpenTxt.text = "Level unlock: " + seedItem.GetUnlockLevel().ToString();
            }
        }
    }

    public void SetItem(Item itemToSet)
    {
        item = itemToSet;
    }

    public void BuyItem()
    {
        SeedItem seedItem = item as SeedItem;
        if (seedItem)
        {
            if (coinManager.HasEnoughCoint(seedItem.GetBuyPrice()))
            {
                coinManager.SubtractCoin(seedItem.GetBuyPrice());
                Inventory.Instance.AddItem(seedItem);
            }
        }
    }
}
