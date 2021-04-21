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

    private void Start()
    {
        //nextLevelBtn.SetActive(false);
        //Debug.LogError("Init coin: " + levelData.GetInitCoin())
        //LoadMissionData();
    }

    public void LoadMissionData()
    {
        levelData = Resources.Load<LevelData>(levelDataPath + (levelManager.GetCurrentLevel()));
        InitItemSlots();
        InitializeStartSeeds();
        FindObjectOfType<CoinManager>().CurrentCoin = levelData.GetInitCoin();
        FindObjectOfType<SeedBarManager>().DisplaySeedItems();
    }

    private void OnEnable()
    {
    }

    private void InitializeStartSeeds()
    {
        Debug.LogError("num init seeds: " + levelData.GetInitSeeds().Length);
        foreach (var seed in levelData.GetInitSeeds())
        {
            Debug.LogError("Add seed item to inventory");
            Inventory.Instance.AddItem(seed.InventoryItem, seed.Quantity);
            Debug.LogError("Inventory after add: " + Inventory.Instance.Count());
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
    
    public void CheckIfMissionCompleted()
    {
        MissionBase[] missions = levelData.GetMissions();
        Debug.LogError("Mission counttttttttttt: " + missions.Length);
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
            //winDialog.SetActive(true);
            //LevelManager.Instance.LoadNextLevel();
            Debug.LogError("Current level completeeeeeeeeeeeee");
            DialogController.instance.ShowDialog(DialogType.WIN);
        } else
        {
            Debug.LogError("Not completeddddddddddddddd");
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
