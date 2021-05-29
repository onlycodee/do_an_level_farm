using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CakeShop : MonoBehaviour
{
    [SerializeField] GameObject cakeHolder;
    [SerializeField] Image cakeImage;
    [SerializeField] TextMeshProUGUI cookTimerTxt;
    [SerializeField] GameObject cookDoneIcon;

    CakeItem cakeItem = null;
    float cookTimer = 0f;

    WaitForSeconds waitOneSecond = new WaitForSeconds(1f);

    private void Start()
    {
        cakeHolder.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
        if (playerMovement)
        {
            playerMovement.SetMovingState(false);
            CookingDialog cookingDialog = DialogController.Instance.ShowDialog(DialogType.COOKING) as CookingDialog; 
            if (cakeItem)
            {
                cookingDialog.SetCakeItem(cakeItem, cookTimer);
            }
        }
    }

    public void ResetCook()
    {
        cakeItem = null;
        cookTimer = 0f;
        cookDoneIcon.SetActive(false);
        cakeHolder.SetActive(false);
    }

    public void SetCakeItem(CakeItem cake, float remainTime)
    {
        Debug.LogError("Set cake itemmmmmmmmmmm: " + remainTime);
        cakeHolder.SetActive(true);
        cakeItem = cake;
        cakeImage.sprite = cake.Avatar;
        cookTimerTxt.text = (Mathf.RoundToInt(remainTime)).ToString();
        cookTimer = remainTime;
        cookDoneIcon.SetActive(false);
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
            cookTimerTxt.text = Mathf.RoundToInt(cookTimer).ToString();
            if (cookTimer <= 0f)
            {
                CookDone();
                break;
            } 
        }
    } 

    void CookDone()
    {
        Debug.Log("Cook done");
        cookDoneIcon.SetActive(true);
        cakeHolder.SetActive(false);
    } 
}
