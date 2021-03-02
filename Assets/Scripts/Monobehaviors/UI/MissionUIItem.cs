using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionUIItem : MonoBehaviour
{
    [SerializeField] Image avatarImg;
    [SerializeField] TextMeshProUGUI quantityTxt;
    [SerializeField] GameObject tickDone;

    CropItemHolder cropItem;
    int targetQuantity = 0;

    public void SetQuantity(int currentQuantity, int targetQuantity)
    {
        quantityTxt.text = $"{currentQuantity}/{targetQuantity}";//quantity.ToString();
        this.targetQuantity = targetQuantity;
    }

    public void SetAvatar(Sprite avatarSprite)
    {
        avatarImg.sprite = avatarSprite;
    }

    public void Complete()
    {
        tickDone.SetActive(true);
    }

    public void Uncomplete()
    {
        tickDone.SetActive(false);
    }

    public void SetCropItem(CropItemHolder cropItem)
    {
        this.cropItem = cropItem;
    }

    public void UpdateUI()
    {
        CropItemHolder cropInInventory = Inventory.Instance.GetItemWithID(cropItem.InventoryItem.Id); 
        if (cropInInventory == null)
        {
            quantityTxt.text = $"{0}/{targetQuantity}";//quantity.ToString();
            Uncomplete();
        } else
        {
            quantityTxt.text = $"{cropInInventory.Quantity}/{targetQuantity}";//quantity.ToString();
            if (cropInInventory.Quantity >= targetQuantity)
            {
                Complete();
            }
        }
    }
}
