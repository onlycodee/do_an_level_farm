using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Items/Egg item")]
public class ChickenEggItem : Item, IExchangeable
{
    [SerializeField] PriceVariable price;
    public int GetBuyPrice()
    {
        return price.BuyPrice;
    }

    public int GetSellPrice()
    {
        return price.SellPrice;
    }
}
