using UnityEngine;

[CreateAssetMenu(menuName = "Items/Crop item")]
public class CropItem : Item, IExchangeable
{
    [SerializeField] int sellPrice, buyPrice;
    [SerializeField] float grownTime;
    public int GetBuyPrice()
    {
        return buyPrice;
    }

    public int GetSellPrice()
    {
        return sellPrice;
    }

    public float GetGrowTime()
    {
        return grownTime;
    }

    public override void SpecificInit()
    {
    }
}
