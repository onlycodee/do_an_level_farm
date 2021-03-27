using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemQuantityText;
    Item item;
    
    public void SetItem(Item itemToSet)
    {
        item = itemToSet;
        itemImage.sprite = item.Avatar;
    }

    public void SetItemQuantityText(int value)
    {
        itemQuantityText.text = value.ToString();
    }

    public Item GetItem()
    {
        return item;
    }

    //public void UpdateUI()
    //{
    //    if (itemImage.sprite != item.Avatar)
    //    {
    //        itemImage.sprite = item.Avatar;
    //    }
    //    itemQuantityText.text = Inventory.Instance.GetQuantity(item).ToString();
    //}
}
