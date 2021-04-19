using DG.Tweening;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameEvent cropHarvestedEvent;
    public GrownTimeDisplay grownTimeDisplay;
    public CropStateUI cropStateUI;
    public GameObject diedCrop;
    MeshRenderer meshRenderer;
    Crop currentCrop = null;
    FieldState curState;

    public bool HasCrop
    {
        get => curState == FieldState.HAS_CROP;
        private set { }
    }
    public bool HasDiedCrop
    {
        get => curState == FieldState.HAS_DIED_CROP; 
    }
    public FieldState State
    {
        get => curState;
        set => curState = value;
    }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void ResetState()
    {
        currentCrop = null;
        cropStateUI.ResetUI();
    }

    public void SetCrop(Crop cropToSet)
    {
        curState = FieldState.HAS_CROP;
        currentCrop = cropToSet;
    }

    public Crop GetCurrentCrop()
    {
        return currentCrop;
    }

    public void Harvest()
    {
        diedCrop.gameObject.SetActive(true);
        diedCrop.transform.localScale = Vector3.zero;
        diedCrop.transform.DOScale(1.0f, .5f);
        curState = FieldState.HAS_DIED_CROP;
        currentCrop.Harvest(UpdateCropMissionUI);
        ResetState();
    }

    void UpdateCropMissionUI()
    {
        cropHarvestedEvent.NotifyAll();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && HasCrop)
        {
            cropStateUI.SetActiveTimerImg(true);
            cropStateUI.SetFillOfTimerImg(currentCrop.GetGrowPercent());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            cropStateUI.SetActiveTimerImg(false);
        }
    }
    public void Hoeing()
    {
        //Debug.LogError("Hoeinggggggggggg");
        curState = FieldState.NORMAL;
        diedCrop.transform.DOScale(1.2f, .3f)
            .OnComplete(() =>
            {
                diedCrop.transform.DOScale(1.0f, .15f)
                .OnComplete(() =>
                {
                    diedCrop.gameObject.SetActive(false);
                });
            });
    }
    public void Watering()
    {
        if (currentCrop != null)
        {
            currentCrop.ChangeToNormalState();
        }
    }
    public void Healing()
    {
        if (currentCrop != null)
        {
            currentCrop.ChangeToNormalState();
        }
    }
    public enum FieldState
    {
        NORMAL,
        HAS_CROP,
        HAS_DIED_CROP
    }
}
