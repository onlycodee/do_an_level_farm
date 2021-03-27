using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBar : MonoBehaviour
{
    [SerializeField] MissionUIItem missionItemSlotPrefab;
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject nextLevelBtn;
    //[SerializeField] CropMission[] cropsMission;
    List<MissionUIItem> itemSlots = new List<MissionUIItem>();
    LevelData levelData;
    LevelManager levelManager = null;

    const string levelDataPath = "Levels/Level_";

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        //nextLevelBtn.SetActive(false);
        //Debug.LogError("Init coin: " + levelData.GetInitCoin())
        FindObjectOfType<CoinManager>().CurrentCoin = levelData.GetInitCoin();
    }

    private void OnEnable()
    {
        levelData = Resources.Load<LevelData>(levelDataPath + (levelManager.GetCurrentLevel() - 1));
        InitItemSlots();
    }

    void InitItemSlots()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            Destroy(itemSlots[i].gameObject);
        }
        itemSlots.Clear();
        MissionBase[] missions = levelData.GetMissions();
        for (int i = 0; i < missions.Length; i++)
        {
            foreach (var crop in missions[i].GetItems())
            {
                MissionUIItem missionUI = Instantiate(missionItemSlotPrefab, transform);
                missionUI.SetAvatar(crop.InventoryItem.Avatar);
                missionUI.SetCropItem(crop);
                ItemHolder cropInInventory = inventory.GetItemWithID(crop.InventoryItem.Id);
                if (cropInInventory != null)
                {
                    missionUI.SetQuantity(inventory.GetItemWithID(crop.InventoryItem.Id).Quantity,
                                          crop.Quantity);
                }
                else
                {
                    missionUI.SetQuantity(0, crop.Quantity);
                }
                itemSlots.Add(missionUI);

                CropMission cropMission = missions[i] as CropMission;
                if (cropMission != null)
                {
                    if (cropMission.IsItemCompleted(crop))
                    {
                        missionUI.Complete();
                    }
                    else
                    {
                        missionUI.Uncomplete();
                    }
                }
            }
        }
    }

    // event update ui
    public void UpdateUI()
    {
        foreach (var item in itemSlots)
        {
            if (item)
            {
                item.UpdateUI();
            } else
            {
                Debug.Log("Item is null");
            }
        }
        //if (levelData.CheckIfLevelCompleted())
        //{
        //    nextLevelBtn.SetActive(true);
        //}
    }
}
