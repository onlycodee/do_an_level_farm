using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Crop : MonoBehaviour
{
    [SerializeField] CropItem cropItem;
    [SerializeField] Inventory inventory;
    [SerializeField] float timeToGrow;

    Field field;
    MeshRenderer meshRenderer;
    public bool IsPlanted = false;
    public bool IsRiped = false;
    public CropType type;
    public CropFactory CropFactory;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public void StartPlanting()
    {
        IsPlanted = true;
        transform.DOScale(1f, timeToGrow).OnComplete(() =>
        {
            IsRiped = true;
        });
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
    public void SetField(Field fieldToSet)
    {
        field = fieldToSet;
    }
}

public enum CropType
{
    WHEAT,
    CORN,
    BEET,
    GREENPLANT
}
