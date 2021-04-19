using UnityEngine;

[CreateAssetMenu(menuName = "Items/Crop item")]
public class CropItem : Item, IExchangeable
{
    [SerializeField] PriceVariable price;
    [SerializeField] float grownTime;
    [SerializeField, Range(1, 100)] float diseasePercent, thirstyPercent;
    [SerializeField] CropType cropType;

    public CropType GetCropType()
    {
        return cropType;
    }

    public float GetThirstyPercent()
    {
        return thirstyPercent; 
    }
    
    public float GetDiseasedPercent()
    {
        return diseasePercent;
    }
    public int GetBuyPrice()
    {
        return price.BuyPrice;
    }

    public int GetSellPrice()
    {
        return price.SellPrice;
    }

    public float GetGrowTime()
    {
        return grownTime;
    }

    public override void SpecificInit()
    {
    }
}
