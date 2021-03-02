using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CropMission : MissionBase
{
    public List<CropItemHolder> requiredCrops;
    public override bool CheckIfFinished()
    {
        foreach (var requiredCrop in requiredCrops)
        {
            if (!HasCropCompleted(requiredCrop)) return false;
        }
        return true;
    }

    public bool HasCropCompleted(CropItemHolder crop)
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
