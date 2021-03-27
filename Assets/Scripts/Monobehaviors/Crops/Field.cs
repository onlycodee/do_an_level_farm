using UnityEngine;

public class Field : MonoBehaviour
{
    public GameEvent cropHarvestedEvent;
    public GrownTimeDisplay grownTimeDisplay;
    public CropStateUI cropStateUI;
    MeshRenderer meshRenderer;
    Crop currentCrop = null;


    public bool HasCrop
    {
        get => currentCrop != null;
        private set { }
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
        currentCrop = cropToSet;
    }

    public Crop GetCurrentCrop()
    {
        return currentCrop;
    }

    public void Interac()
    {
        if (currentCrop.IsRiped)
        {
            currentCrop.Harvest(UpdateCropMissionUI);
            ResetState();
            //cropStateUI.SetActive(false);
        }
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
}
