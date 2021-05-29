using System;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public Sprite Avatar;
    public string Id = "";
    public bool VisibleInInventory = true;

    private void Awake()
    {
        Id = Guid.NewGuid().ToString(); 
    }

    private void OnValidate()
    {
        if (Id == "")
        {
            Id = Guid.NewGuid().ToString(); 
        }
    }

    public virtual int GetQuantityInInventory()
    {
        if (Inventory.Instance != null)
        {
            return Inventory.Instance.GetQuantity(this);
        }
        return 0;
    }
    //public abstract void SpecificInit(); 
}
