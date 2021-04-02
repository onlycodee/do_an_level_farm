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
        curState = normalState; 
        growTimer = 0f;
        float rdThirsty = UnityEngine.Random.Range(1f, 1000f);
        if (rdThirsty / 10f <= cropItem.GetThirstyPercent())
        {
            thirstyTime = UnityEngine.Random.Range(cropItem.GetGrowTime() * .5f, cropItem.GetGrowTime() * .9f);
            //Debug.LogError("Thirsty time: " + thirstyTime);
        } else
        {
            thirstyTime = float.MaxValue;
        }
        float rdDiseased = UnityEngine.Random.Range(1f, 1000f);
        if (rdDiseased / 10f <= cropItem.GetDiseasedPercent())
        {
            diseasedTime = UnityEngine.Random.Range(cropItem.GetGrowTime() * .5f, cropItem.GetGrowTime() * .9f);
            //Debug.LogError("diseased time: " + diseasedTime);
        } else
        {
            diseasedTime = float.MaxValue;
        }
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
        }
    }

    public void DisplayThirstyIcon()
    {
        field.cropStateUI.SetActiveWaterIcon(true);
    }
    public void HideThirstyIcon()
    {
        field.cropStateUI.SetActiveWaterIcon(false);
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

