using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CropFactory : ScriptableObject 
{
    [HideInInspector]
    public Crop[] cropPrefabs;
    //public List<Queue<Crop>> pooledCrops;

    public Crop GetCrop(CropType type)
    {
        int cropIndex = (int)type;
        Crop cropInstance = Instantiate(cropPrefabs[cropIndex]);
        Debug.Log("Create crop: " + cropPrefabs[cropIndex].name);
        return cropInstance;
    }

    public void Reclaim(Crop crop)
    {
    }
}

