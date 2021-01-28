using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Inventory : ScriptableObject
{
    public List<ItemHolder> items;
    public static Inventory _instance;
    const string resourcePath = "Inventory";

    public static Inventory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<Inventory>(resourcePath);
            }
            if (_instance == null)
            {
                Debug.LogError("Can not find inventor resourse in : " + resourcePath);
            }
            return _instance;
        }
    }

    public void Reset()
    {
        items.Clear();
    }

    public void AddItem(InventoryItem itemToAdd, int quantity = 1)
    {
        int itemToAddIndex = GetItemIndex(itemToAdd);
        if (itemToAddIndex != -1)
        {
            items[itemToAddIndex].Quantity += quantity;
        } else
        {
            items.Add(new ItemHolder() { InventoryItem = itemToAdd, Quantity = 1 });
        }
    }
    public void RemoveItem(InventoryItem item, int quantity = 1)
    {
        int itemToAddIndex = GetItemIndex(item);
        if (itemToAddIndex != -1)
        {
            items[itemToAddIndex].Quantity -= quantity;
            if (items[itemToAddIndex].Quantity <= 0)
            {
                items.Remove(items[itemToAddIndex]);
            }
        }
    }
    public int GetQuantity(InventoryItem item)
    {
        int itemIndex = GetItemIndex(item); 
        if (itemIndex != -1)
        {
            return items[itemIndex].Quantity;
        }
        return 0;
    }
    int GetItemIndex(InventoryItem item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].InventoryItem == item) return i;
        }
        return -1;
    }
}
