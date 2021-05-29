using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Chicken item")]
public class ChickenItem : Item, IExchangeable 
{
    public PriceVariable price;

    public int GetBuyPrice()
    {
        return price.BuyPrice;
    }

    public int GetSellPrice()
    {
        return price.SellPrice;
    }
}
