using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Items/Cake item")]
public class CakeItem : Item, IExchangeable
{
    [SerializeField] List<ItemHolder> ingredients;
    [SerializeField] float cookTime;
    [SerializeField] int sellPrice, buyPrice;

    public List<ItemHolder> GetIngredients()
    {
        return ingredients;
    }

    public int GetSellPrice()
    {
        return sellPrice;
    }

    public int GetBuyPrice()
    {
        return buyPrice;
    }
    public float GetCookTime()
    {
        return cookTime;
    } 
}
