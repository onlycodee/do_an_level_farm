using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Seed item")]
public class SeedItem : Item, IExchangeable
{
    [SerializeField] int unlockLevel;
    [SerializeField] PriceVariable price;
    public CropItem cropItem;

    public int GetUnlockLevel()
    {
        return unlockLevel;
    }

    public override void SpecificInit()
    {
    }

    public int GetSellPrice()
    {
        return price.SellPrice;
    }

    public int GetBuyPrice()
    {
        return price.BuyPrice;
    }
}
