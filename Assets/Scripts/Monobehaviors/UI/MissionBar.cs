using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBar : MonoBehaviour
{
    [SerializeField] MissionUIItem missionItemSlotPrefab;
    [SerializeField] Inventory inventory;
    [SerializeField] CropMission[] cropsMission;
    List<MissionUIItem> itemSlots = new List<MissionUIItem>();


    private void OnEnable()
    {
        InitItemSlots();
    }

    void InitItemSlots()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            Destroy(itemSlots[i].gameObject);
        }
        itemSlots.Clear();
        for (int i = 0; i < cropsMission.Length; i++)
        {
            foreach (var crop in cropsMission[i].requiredCrops)
            {
                MissionUIItem missionUI = Instantiate(missionItemSlotPrefab, transform);
                missionUI.SetAvatar(crop.InventoryItem.Avatar);
                missionUI.SetCropItem(crop);
                CropItemHolder cropInInventory = inventory.GetItemWithID(crop.InventoryItem.Id);
                if (cropInInventory != null)
                {
                    missionUI.SetQuantity(inventory.GetItemWithID(crop.InventoryItem.Id).Quantity, 
                                          crop.Quantity);
                } else
                {
                    missionUI.SetQuantity(0, crop.Quantity);
                } 
                itemSlots.Add(missionUI);
                if (cropsMission[i].HasCropCompleted(crop)) {
                    missionUI.Complete();
                } else
                {
                    missionUI.Uncomplete();
                }
            }
        }
    }

    public void UpdateUI()
    {
        Debug.Log("Update ui in mission bar");
        foreach (var item in itemSlots)
        {
            if (item)
            {
                item.UpdateUI();
            }
        }
    }
}
