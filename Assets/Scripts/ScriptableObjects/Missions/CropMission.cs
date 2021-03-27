using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CropMission : MissionBase
{
    public ItemHolder[] requiredItems;
    public override bool CheckIfCompleted()
    {
        foreach (var requiredItem in requiredItems)
        {
            if (!IsItemCompleted(requiredItem)) return false;
        }
        return true;
    }

    public override ItemHolder[] GetItems()
    {
        return requiredItems;
    }

    public bool IsItemCompleted(ItemHolder crop)
    {
        foreach (var item in Inventory.Instance.GetAllItems())
        {
            if (item.InventoryItem.Id == crop.InventoryItem.Id)
            {
                if (item.Quantity >= crop.Quantity) return true;
                else return false;
            }
        }
        return false;
    } 

    public override void UpdateUI()
    {
    }
}
