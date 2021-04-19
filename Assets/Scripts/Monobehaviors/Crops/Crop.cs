using UnityEngine;
using DG.Tweening;
using System;

public class Crop : MonoBehaviour
{
    [SerializeField] CropItem cropItem;
    [SerializeField] Inventory inventory;

    [SerializeField] BaseState normalState;
    [SerializeField] BaseState thirstyState;
    [SerializeField] BaseState ripedState;
    [SerializeField] BaseState diseasedState;

    [HideInInspector]
    public bool IsPlanted = false;
    [HideInInspector]
    public bool IsRiped = false;
    public CropType type;
    public CropFactory CropFactory;

    Field field;
    MeshRenderer meshRenderer;
    float growTimer = 0f;
    bool stopGrowing = false;
    BaseState curState = null;
    float thirstyTime, diseasedTime;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        ComputeTimes();
    }

    private void ComputeTimes()
    {
        curState = normalState;
        growTimer = 0f;
        thirstyTime = float.MaxValue;
        diseasedTime = float.MaxValue;
        float rdThirsty = UnityEngine.Random.Range(1f, 1000f);
        if (rdThirsty / 10f <= cropItem.GetThirstyPercent())
        {
            thirstyTime = cropItem.GetGrowTime() * UnityEngine.Random.Range(.5f, .9f);
        }
        else
        {
            float rdDiseased = UnityEngine.Random.Range(1f, 1000f);
            if (rdDiseased / 10f <= cropItem.GetDiseasedPercent())
            {
                diseasedTime = cropItem.GetGrowTime() * UnityEngine.Random.Range(.5f, .9f);
            }
        }
        //Debug.Log("Grown time: " + cropItem.GetGrowTime() + " thirsty time: " + thirstyTime + " diseased time: " + diseasedTime);
    }

    private void Update()
    {
        if (curState != null)
        {
            curState.Execute(this);
        }
    }

    public void Grow()
    {
        transform.localScale += Vector3.one * (Time.deltaTime / GetGrowTime());
        growTimer += Time.deltaTime;
        if (transform.localScale.x >= 1f)
        {
            ChangeState(ripedState);
        } else if (growTimer >= thirstyTime)
        {
            ChangeState(thirstyState);
            thirstyTime = float.MaxValue;
        }
        else if (growTimer >= diseasedTime)
        {
            ChangeState(diseasedState);
            diseasedTime = float.MaxValue;
        }
    }

    public void DisplayThirstyIcon()
    {
        field.cropStateUI.SetActiveWaterIcon(true);
        field.cropStateUI.SetActiveBtnWatering(true);
    }

    public void HideThirstyIcon()
    {
        field.cropStateUI.SetActiveWaterIcon(false);
        field.cropStateUI.SetActiveBtnWatering(false);
    }
    public void DisplayDiseasedIcon()
    {
        field.cropStateUI.SetActiveDiseaseIcon(true);
        field.cropStateUI.SetActiveBtnHealing(true);
    }

    public void HideDiseasedIcon()
    {
        field.cropStateUI.SetActiveDiseaseIcon(false);
        field.cropStateUI.SetActiveBtnHealing(false);
    }

    public void ChangeState(BaseState newState)
    {
        if (curState != null)
        {
            curState.Exit(this);
        }
        curState = newState;
        curState.Enter(this);
    }

    public void ChangeToNormalState()
    {
        ChangeState(normalState);
    }

    public void StopGrowing()
    {
        stopGrowing = true;
    }

    public void Harvest(Action onCropHarvested)
    {
        transform.DOMoveY(transform.position.y + 2f, 1f);
        transform.DOScale(.8f, 1f).OnComplete(() =>
        {
            inventory.AddItem(cropItem, 1);
            onCropHarvested();
            Destroy(gameObject);
        });
    } 

    public BaseState GetCurrentState()
    {
        return curState;
    }

    public float GetGrowTimer()
    {
        return growTimer;
    }

    public float GetGrowTime()
    {
        return cropItem.GetGrowTime();
    }

    public void SetField(Field fieldToSet)
    {
        field = fieldToSet;
    }

    public CropItem GetCropItem()
    {
        return cropItem;
    }
    public float GetGrowPercent()
    {
        return growTimer / GetGrowTime();
    }
}

public enum CropType
{
    WHEAT,
    CORN,
    BEET,
    GREENPLANT
}

