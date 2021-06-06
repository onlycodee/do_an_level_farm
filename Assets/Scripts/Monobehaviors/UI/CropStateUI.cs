using UnityEngine;
using UnityEngine.UI;

public class CropStateUI : MonoBehaviour
{
    [SerializeField] Image timerImg;
    [SerializeField] GameObject waterIcon, diseaseIcon;
    //[SerializeField] GameObject waterBtn, healingBtn;

    private void Start()
    {
        ResetUI();
        //SetActiveWaterIcon(false);
    }

    public void SetFillOfTimerImg(float percent)
    {
        timerImg.fillAmount = percent;
    }
    public void SetActiveTimerImg(bool state)
    {
        timerImg.gameObject.SetActive(state);
    }
    public void SetActiveWaterIcon(bool state)
    {
        //Debug.LogError("Set active water icon: " + state);
        waterIcon.SetActive(state);
    }
    public void SetActiveDiseaseIcon(bool state)
    {
        diseaseIcon.SetActive(state);
    }
    //public void SetActiveBtnWatering(bool state)
    //{
    //    waterBtn.SetActive(state);
    //}
    //public void SetActiveBtnHealing(bool state)
    //{
    //    healingBtn.SetActive(state);
    //}

    public void ResetUI()
    {
        SetActiveTimerImg(false);
        SetActiveWaterIcon(false);
        SetActiveDiseaseIcon(false);
        //waterBtn.SetActive(false);
        //healingBtn.SetActive(false);
    }
}
