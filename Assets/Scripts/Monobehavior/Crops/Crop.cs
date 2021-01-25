using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public float timeToGrow;
    public float price;
    public bool isPlanted = false;
    public CropType type;

    protected virtual void Start()
    {
        transform.localScale = Vector3.zero;
    }

    public void StartPlanting()
    {
        isPlanted = true;
    }
}
public enum CropType
{
    WHEAT,
    CORN,
    BEET,
    GREENPLANT
}
