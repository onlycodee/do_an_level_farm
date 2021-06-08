using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
{
    public Image itemImage;
    public TextMeshProUGUI itemQuantityText;
    ItemHolder itemHodler;
    
    public void SetItemHolder(ItemHolder itemHolderParam)
    {
        itemHodler = itemHolderParam;
        itemImage.sprite = itemHodler.InventoryItem.Avatar;
        itemQuantityText.text = itemHodler.Quantity.ToString();
    }

    public Item GetItem()
    {
        return itemHodler.InventoryItem;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Debug.LogError("Inventory item clickedddddddddddddddd");
        SellDialog sellDialog = DialogController.Instance.ShowDialog(DialogType.SELLING, DialogShow.OVER_CURRENT) as SellDialog;
        sellDialog.SetItemHolder(itemHodler);
    }
}
