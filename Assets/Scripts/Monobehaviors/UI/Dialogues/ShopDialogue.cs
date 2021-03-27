using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDialogue : BaseDialogue 
{
    [SerializeField] List<Item> items;
    [SerializeField] ShopItem shopItemSlotPrefab;
    [SerializeField] RectTransform itemParent;

    List<ShopItem> shopUIItems = new List<ShopItem>();

    private void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            ShopItem newItem = Instantiate(shopItemSlotPrefab, itemParent);
            newItem.SetItem(items[i]);
            shopUIItems.Add(newItem);
        }
    }
}
