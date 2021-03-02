using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Inventory : ScriptableObject
{
    public List<CropItemHolder> items;
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

    public void AddItem(CropItem itemToAdd, int quantity = 1)
    {
        int itemToAddIndex = GetItemIndex(itemToAdd);
        if (itemToAddIndex != -1)
        {
            items[itemToAddIndex].Quantity += quantity;
        } else
        {
            items.Add(new CropItemHolder() { InventoryItem = itemToAdd, Quantity = 1 });
        }
    }
    public IEnumerable<CropItemHolder> GetAllItems()
    {
        return items; 
    }

    public CropItemHolder GetItemWithID(string id)
    {
        foreach (var item in items)
        {
            if (item.InventoryItem.Id == id)
            {
                return item;
            }
        }
        return null;
    }
    public void RemoveItem(CropItem item, int quantity = 1)
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
    public int GetQuantity(CropItem item)
    {
        int itemIndex = GetItemIndex(item); 
        if (itemIndex != -1)
        {
            return items[itemIndex].Quantity;
        }
        return 0;
    }
    int GetItemIndex(CropItem item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].InventoryItem == item) return i;
        }
        return -1;
    }
}
