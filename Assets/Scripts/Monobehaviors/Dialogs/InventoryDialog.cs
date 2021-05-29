using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDialog : Dialog 
{
    [SerializeField] Inventory inventory;
    [SerializeField] ItemSlotUI itemSlotPrefab;
    [SerializeField] Transform farmProductParent;
    [SerializeField] Transform seedParent;
    [SerializeField] Button farmProductsBtn;
    [SerializeField] Button seedsBtn;
    [SerializeField] Sprite normalSprite, activeSprite;

    List<ItemSlotUI> itemSlots = new List<ItemSlotUI>();
    bool seedView = true;

    private void OnEnable()
    {
        OnFarmProductViewClicked();
    }

    public void InitItemSlots()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            Destroy(itemSlots[i].gameObject);
        }
        itemSlots.Clear();
        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].Quantity <= 0 || !inventory.items[i].InventoryItem.VisibleInInventory) continue;
            ItemSlotUI itemSlotInstance = Instantiate(itemSlotPrefab);
            SeedItem seedItem = inventory.items[i].InventoryItem as SeedItem;
            if (seedItem)
            {
                itemSlotInstance.transform.SetParent(seedParent);
            } else
            {
                itemSlotInstance.transform.SetParent(farmProductParent);
            }
            itemSlotInstance.transform.localScale = Vector3.one;
            itemSlotInstance.SetItemHolder(inventory.items[i]);
            itemSlots.Add(itemSlotInstance);
        }
    }

    // btn seed view event
    public void OnSeedViewClicked()
    {
        Debug.LogError("Seed view clickeddddddddddddd");
        seedsBtn.image.sprite = activeSprite;
        farmProductsBtn.image.sprite = normalSprite;
        farmProductParent.gameObject.SetActive(false);
        seedParent.gameObject.SetActive(true);
        InitItemSlots();
        seedsBtn.GetComponent<Image>().color = Color.green;
        farmProductsBtn.GetComponent<Image>().color = Color.white;
    }
    // btn seed view event
    public void OnFarmProductViewClicked()
    {
        Debug.LogError("Farm product view clickeddddddddddddd");
        seedsBtn.image.sprite = normalSprite;
        farmProductsBtn.image.sprite = activeSprite;
        farmProductParent.gameObject.SetActive(true);
        seedParent.gameObject.SetActive(false);
        InitItemSlots();
        seedsBtn.GetComponent<Image>().color = Color.white;
        farmProductsBtn.GetComponent<Image>().color = Color.green;
    }
}
