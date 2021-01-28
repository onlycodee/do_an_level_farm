using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDialog : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] ItemSlotUI itemSlotPrefab;
    [SerializeField] Transform itemSlotParent;

    List<ItemSlotUI> itemSlots = new List<ItemSlotUI>();

    private void OnEnable()
    {
        if (itemSlots.Count != inventory.items.Count)
        {
            InitItemSlots();
        }
        for (int i = 0; i < inventory.items.Count; i++)
        {
            itemSlots[i].UpdateUI();
        }
    }

    void InitItemSlots()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            Destroy(itemSlots[i].gameObject);
        }
        itemSlots.Clear();
        for (int i = 0; i < inventory.items.Count; i++)
        {
            ItemSlotUI itemSlotInstance = Instantiate(itemSlotPrefab, itemSlotParent);
            itemSlotInstance.SetItem(inventory.items[i].InventoryItem);
            itemSlots.Add(itemSlotInstance);
        }
    }

    //private void Start()
    //{
    //    isInitialized = true;
    //    for (int i = 0; i < inventory.items.Count; i++)
    //    {
    //        ItemSlotUI itemSlotInstance = Instantiate(itemSlotPrefab, itemSlotParent);
    //        itemSlotInstance.SetItem(inventory.items[i].InventoryItem);
    //        itemSlots.Add(itemSlotInstance);
    //    }
    //}
}
