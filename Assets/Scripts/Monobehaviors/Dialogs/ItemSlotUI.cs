using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemQuantityText;
    InventoryItem item;
    
    public void SetItem(InventoryItem itemToSet)
    {
        item = itemToSet;
    }

    public void UpdateUI()
    {
        if (itemImage.sprite != item.Avatar)
        {
            itemImage.sprite = item.Avatar;
        }
        itemQuantityText.text = Inventory.Instance.GetQuantity(item).ToString();
    }
}
