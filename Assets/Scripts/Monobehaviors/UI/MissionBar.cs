using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBar : MonoBehaviour
{
    [SerializeField] MissionUIItem missionItemSlotPrefab;
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject nextLevelBtn;
    [SerializeField] GameObject winDialog; 
    //[SerializeField] CropMission[] cropsMission;
    List<MissionUIItem> itemSlots = new List<MissionUIItem>();
    LevelData levelData;
    LevelManager levelManager = null;

    const string levelDataPath = "Levels/Level_";

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void LoadMissionData()
    {
        levelData = Resources.Load<LevelData>(levelDataPath + (levelManager.GetCurrentLevel()));
        InitializeStartSeeds();
        FindObjectOfType<GoldManager>().CurrentGold = levelData.GetInitCoin();
        //FindObjectOfType<SeedBarManager>().DisplaySeedItems();
        InitItemSlots();
        if (levelData.HasTime())
        {
            FindObjectOfType<LevelTimer>().SetTime(levelData.GetTime());
            FindObjectOfType<LevelTimer>().CountDownTime();
        }
    }

    private void OnEnable()
    {
    }

    private void InitializeStartSeeds()
    {
        foreach (var seed in levelData.GetInitSeeds())
        {
            Inventory.Instance.AddItem(seed.InventoryItem, seed.Quantity);
        }
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
                missionUI.Uncomplete();
                missionUI.SetAvatar(crop.InventoryItem.Avatar);
                missionUI.SetCropItem(crop);
                //ItemHolder cropInInventory = inventory.GetItemWithID(crop.InventoryItem.Id);
                //if (cropInInventory != null)
                //{
                //    missionUI.SetQuantity(cropInInventory.Quantity,
                //                          crop.Quantity);
                //}
                //else
                //{
                //    missionUI.SetQuantity(0, crop.Quantity);
                //}
                missionUI.SetQuantity(crop.InventoryItem.GetQuantityInInventory(), crop.Quantity);
                missionUI.UpdateUI();
                itemSlots.Add(missionUI);

                //ItemMission cropMission = missions[i] as ItemMission;
                //if (cropMission != null)
                //{
                //    if (cropMission.IsItemCompleted(crop))
                //    {
                //        missionUI.Complete();
                //    }
                //    else
                //    {
                //        missionUI.Uncomplete();
                //    }
                //}
            }
        }
    }

    public void UpdateItemUIAndCheckIfMissionCompleted()
    {
        Debug.Log("Update item ui and check if mission completed");
        UpdateUI();
        CheckIfMissionCompleted();
    }
    
    public void CheckIfMissionCompleted()
    {
        MissionBase[] missions = levelData.GetMissions();
        bool isCompleted = true;
        foreach (var mission in missions)
        {
            if (!mission.CheckIfCompleted())
            {
                isCompleted = false;
                break;
            }
        }
        if (isCompleted)
        {
            GameManager.Instance.Win();
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
    }
}
