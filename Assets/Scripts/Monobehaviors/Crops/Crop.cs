using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Crop : MonoBehaviour
{
    [SerializeField] float timeToGrow;
    [SerializeField] float price;

    Field field;
    MeshRenderer meshRenderer;
    public bool IsPlanted = false;
    public bool IsRiped = false;
    public CropType type;

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
    public void Harvest()
    {
        transform.DOMoveY(transform.position.y + 2f, 1f);
        transform.DOScale(.8f, 1f).OnComplete(() =>
        {
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
