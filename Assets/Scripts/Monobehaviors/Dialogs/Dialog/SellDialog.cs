using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellDialog : Dialog
{
    [SerializeField] TextMeshProUGUI numItemsTxt, revenueTxt;
    [SerializeField] Image avatarImg;
    [SerializeField] CanvasGroup sellBtn;
    [SerializeField] Transform goldTrans;

    ItemHolder itemHolder;
    int curQuantity = 0, maxQuantity = 0;
    int curRevenue = 0;
    bool hasSold = false;

    protected override void Start()
    {
        base.Start();
    }
    public void SetItemHolder(ItemHolder itemHolderParam)
    {
        itemHolder = itemHolderParam;
        maxQuantity = itemHolder.Quantity;
        UpdateTexts();
        avatarImg.sprite = itemHolderParam.InventoryItem.Avatar;
    }
    public void Sell()
    {
        if (curQuantity <= 0 || hasSold) return;
        sellBtn.interactable = false;
        hasSold = true;
        Inventory.Instance.SubtractQuantity(itemHolder.InventoryItem, curQuantity);
        GoldDisplayer goldDisplayer = FindObjectOfType<GoldDisplayer>();
        goldDisplayer.Show();
        GoldManager goldManager = FindObjectOfType<GoldManager>();
        MoveGoldAnim.Instance.MoveGolds(goldTrans.position, goldDisplayer.GetGoldIconTrans().position, 6, () =>
        {
            goldDisplayer.GetGoldIconTrans().DOScale(1.2f, .1f).OnComplete(() =>
            {
                goldDisplayer.GetGoldIconTrans().DOScale(1.0f, .05f);
            });
        }, () =>
        {
            goldManager.AddGold(curRevenue);
            //StartCoroutine(DelayClose(goldManager.GetAnimLength()));
        });
        //Close();
        StartCoroutine(DelayClose(.25f));
    }
    public IEnumerator DelayClose(float duration)
    {
        yield return new WaitForSeconds(duration);
        Close();
    }
    public void Increase()
    {
        if (curQuantity < maxQuantity)
        {
            curQuantity++;
            UpdateTexts();
        }
    }
    public void Decrease()
    {
        if (curQuantity > 0)
        {
            curQuantity--;
            UpdateTexts();
        }
    }
    public void UpdateTexts()
    {
        //CropItem crop = itemHolder.InventoryItem as CropItem;
        //SeedItem seedItem = itemHolder.InventoryItem as SeedItem;
        //if (crop)
        //{
        //    curRevenue = curQuantity * crop.GetSellPrice();
        //} else
        //{
        //    curRevenue = curQuantity;
        //}
        IExchangeable price = itemHolder.InventoryItem as IExchangeable;
        numItemsTxt.text = curQuantity + "/" + maxQuantity;
        curRevenue = curQuantity * price.GetSellPrice();
        revenueTxt.text = curRevenue.ToString();
    }
    public override void Close()
    {
        base.Close();
        FindObjectOfType<InventoryDialog>().InitItemSlots();
    }
}
