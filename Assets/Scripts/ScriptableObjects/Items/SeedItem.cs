using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Seed item")]
public class SeedItem : CropItem
{
    [SerializeField] int unlockLevel;
    [SerializeField, Range(1, 100)] float diseasePercent, thirstyPercent; 

    public int GetUnlockLevel()
    {
        return unlockLevel;
    }

    public float GetThirstyPercent()
    {
        return thirstyPercent; 
    }
    
    public float GetDiseasedPercent()
    {
        return diseasePercent;
    }
}
