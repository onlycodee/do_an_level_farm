using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CookingPanel : MonoBehaviour
{
    [SerializeField] Image avatar;
    [SerializeField] TextMeshProUGUI cookTimeTxt;
    [SerializeField] Button getBtn;
    [SerializeField] int initPosition, donePosition;

    CakeItem cakeItem;
    WaitForSeconds waitOneSecond = new WaitForSeconds(1);
    float cookTimer;


    private void OnEnable()
    {
        getBtn.interactable = false;
    }

    public void SetCakeItem(CakeItem item, float remainTime = 0)
    {
        avatar.GetComponent<RectTransform>().SetYAnchoredPosition(initPosition);
        cakeItem = item;
        avatar.sprite = cakeItem.Avatar;
        cookTimeTxt.text = remainTime.ToString(); //cakeItem.GetCookTime().ToString();
        cookTimer = remainTime;
        if (cookTimer <= 0f)
        {
            CookDone();
        } else
        {
            StartCoroutine(CountdownTime());
        }
    }

    IEnumerator CountdownTime()
    {
        while (cookTimer > 0)
        {
            yield return null;
            cookTimer -= Time.deltaTime;
            cookTimeTxt.text = Mathf.RoundToInt(cookTimer).ToString();
            if (cookTimer <= 0)
            {
                CookDone();
                break;
            } 
        }
    } 

    public float GetCookTimer()
    {
        return cookTimer;
    }

    public CakeItem GetCurrentCakeItem()
    {
        return cakeItem;
    }
    
    void CookDone()
    {
        avatar.GetComponent<RectTransform>().SetYAnchoredPosition(donePosition);
        cookTimeTxt.transform.parent.gameObject.SetActive(false);
        getBtn.interactable = true;
    }

    public void TakeCake()
    {
        RectTransform pos = avatar.GetComponent<RectTransform>();
        FloatingUIItemController.Instance.ShowWithAnchoredPosition(cakeItem.Avatar, 1, pos.anchoredPosition
            , pos.anchoredPosition + Vector2.up * 150,
            () =>
            {
                Inventory.Instance.AddItem(cakeItem);
                FindObjectOfType<CookingDialog>().ShowCakeMenu();
                cakeItem = null;
            });
    }
    //IEnumerator COTakeCake()
    //{
    //    Inventory.Instance.AddItem(cakeItem);
    //    FindObjectOfType<CookingDialog>().ShowCakeMenu();
    //    onItemQuantityChanged.NotifyAll();
    //    cakeItem = null;
    //}
}
