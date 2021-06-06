using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI sellPriceText, buyPriceText, itemName;
    [SerializeField] Image avatar;
    [SerializeField] Button buyButton;
    [SerializeField] GameObject lockMask;
    [SerializeField] TextMeshProUGUI levelOpenTxt;
    [SerializeField] RectTransform floatingTextPrefab;

    //[SerializeField] GameEvent onItemBuyedSuccess;

    Item item;
    GoldManager coinManager;

    private void Start()
    {
        coinManager = FindObjectOfType<GoldManager>();
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
            if (coinManager.HasEnoughGold(seedItem.GetBuyPrice()))
            {
                coinManager.SubtractGold(seedItem.GetBuyPrice());
                Inventory.Instance.AddItem(seedItem);
                FloatingUIItemController.Instance.Show(item.Avatar, 1, avatar.transform.position - Vector3.up * 20, avatar.transform.position + Vector3.up * 10);
            } else
            {
                ToastManager.Instance.ShowNotifyWorldPosition("NOT ENOUGH MONEY", transform.position);
            }

        }
    }

    public void ShowDetail()
    {
        SeedItem seedItem = item as SeedItem;
        if (seedItem == null)
        {
            Debug.LogError("Seed item is nullllllllllllllllll");
        } else
        {
            FindObjectOfType<ShopDialogue>().ShowDetail(seedItem.GetCropItem());
        }
    }
}
