using UnityEngine;
using UnityEngine.UI;

public class CropStateUI : MonoBehaviour
{
    [SerializeField] Image timerImg;
    [SerializeField] GameObject waterIcon, diseaseIcon;
    [SerializeField] GameObject waterBtn, healingBtn;

    private void Start()
    {
        ResetUI();
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
        waterIcon.SetActive(state);
    }
    public void SetActiveDiseaseIcon(bool state)
    {
        diseaseIcon.SetActive(state);
    }
    public void SetActiveBtnWatering(bool state)
    {
        waterBtn.SetActive(true);
    }
    public void SetActiveBtnHealing(bool state)
    {
        healingBtn.SetActive(true);
    }

    public void ResetUI()
    {
        SetActiveTimerImg(false);
        SetActiveWaterIcon(false);
        SetActiveDiseaseIcon(false);
    }
}
