using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CakeUIItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cakeName, sellPrice, cookTime;
    [SerializeField] CakeIngredientUIItem cakeIngredientPrefab;
    [SerializeField] RectTransform cakeIngreParent;
    [SerializeField] CakeItem cakeItem;
    [SerializeField] Button cookButton;

    List<CakeIngredientUIItem> cakeIngredients = new List<CakeIngredientUIItem>();
    bool hasInited = false;

    private void Start()
    {
        cakeName.text = cakeItem.name;
        sellPrice.text = cakeItem.GetSellPrice().ToString();
        cookTime.text = cakeItem.GetCookTime().ToString();

        cookButton.onClick.RemoveAllListeners();
        cookButton.onClick.AddListener(Cook);
        if (HasEnoughIngredients())
        {
            cookButton.interactable = true;
        } else
        {
            cookButton.interactable = false;
        }
    }

    public void UpdateUI()
    {
        if (!hasInited)
        {
            hasInited = true;
            InitIncredientUIItems();
        } else
        {
            for (int i = 0; i < cakeIngredients.Count; i++)
            {
                cakeIngredients[i].UpdateTextUI();
            }
        } 
    }
    public void InitIncredientUIItems()
    {
        List<ItemHolder> ingres = cakeItem.GetIngredients();
        for (int i = 0; i < ingres.Count; i++)
        {
            CakeIngredientUIItem newIngre = Instantiate(cakeIngredientPrefab, cakeIngreParent);
            newIngre.SetIngredientItem(ingres[i]);
            newIngre.UpdateTextUI();
            cakeIngredients.Add(newIngre);
        }
    }
    public bool HasEnoughIngredients()
    {
        List<ItemHolder> ingres = cakeItem.GetIngredients();
        for (int i = 0; i < ingres.Count; i++)
        {
            if (Inventory.Instance.GetQuantity(ingres[i].InventoryItem) < ingres[i].Quantity) return false;
        }
        return true;
    }
    public void Cook()
    {
        if (HasEnoughIngredients())
        {
            Debug.LogError("Enough ingredient");
            List<ItemHolder> ingres = cakeItem.GetIngredients();
            for (int i = 0; i < ingres.Count; i++)
            {
                Inventory.Instance.SubtractQuantity(ingres[i].InventoryItem, ingres[i].Quantity);
                UpdateUI();
            }
            FindObjectOfType<CookingDialog>().ShowCookingPanel(cakeItem, cakeItem.GetCookTime());
        } else
        {
            Debug.LogError("Not Enough ingredientttttttttttttt");
        }
    }
}
