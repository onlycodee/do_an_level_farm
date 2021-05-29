using UnityEngine;

[CreateAssetMenu(menuName = "Items/Seed item")]
public class SeedItem : Item, IExchangeable 
{
    [SerializeField] int unlockLevel;
    [SerializeField] PriceVariable price;
    [SerializeField] CropItem cropItem;


    public CropType GetCropType()
    {
        return cropItem.GetCropType();
    }

    public int GetUnlockLevel()
    {
        return unlockLevel;
    }

    public int GetSellPrice()
    {
        return price.SellPrice;
    }

    public int GetBuyPrice()
    {
        return cropItem.GetBuyPrice();
    }
    public int GetGrowthTime()
    {
        return cropItem.GetGrowthTime();
    }
    public CropItem GetCropItem()
    {
        return cropItem;
    }
}
