using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDialog : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] ItemSlotUI itemSlotPrefab;
    [SerializeField] Transform farmProductParent;
    [SerializeField] Transform seedParent;
    [SerializeField] Button farmProductsBtn;
    [SerializeField] Button seedsBtn;

    List<ItemSlotUI> itemSlots = new List<ItemSlotUI>();
    bool seedView = true;

    private void OnEnable()
    {
        //if (itemSlots.Count != inventory.items.Count)
        //{
        //    InitItemSlots();
        //}

        //for (int i = 0; i < inventory.items.Count; i++)
        //{
        //    itemSlots[i].SetItemQuantityText(Inventory.Instance.GetQuantity(itemSlots[i].GetCropItem()));
        //}
        //InitItemSlots();
        OnFarmProductViewClicked();
    }

    void InitItemSlots()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            Destroy(itemSlots[i].gameObject);
        }
        itemSlots.Clear();
        for (int i = 0; i < inventory.items.Count; i++)
        {
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
            itemSlotInstance.SetItem(inventory.items[i].InventoryItem);
            itemSlotInstance.SetItemQuantityText(Inventory.Instance.GetQuantity(itemSlotInstance.GetItem()));
            itemSlots.Add(itemSlotInstance);
        }
    }

    // btn seed view event
    public void OnSeedViewClicked()
    {
        //if (seedView) return;
        //seedView = true;
        farmProductParent.gameObject.SetActive(false);
        seedParent.gameObject.SetActive(true);
        InitItemSlots();
        seedsBtn.GetComponent<Image>().color = Color.green;
        farmProductsBtn.GetComponent<Image>().color = Color.white;
    }
    // btn seed view event
    public void OnFarmProductViewClicked()
    {
        //if (!seedView) return;
        //seedView = false;
        farmProductParent.gameObject.SetActive(true);
        seedParent.gameObject.SetActive(false);
        InitItemSlots();
        seedsBtn.GetComponent<Image>().color = Color.white;
        farmProductsBtn.GetComponent<Image>().color = Color.green;
    }
}
