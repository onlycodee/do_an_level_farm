using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public enum FieldState
    {
        NORMAL,
        LACKWATER,
        DISEASED
    }
    MeshRenderer meshRenderer;
    Crop currentCrop = null;
    public FieldState State { get; set; }
    public bool HasCrop
    {
        get => currentCrop != null;
        private set { }
    } 

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ResetState()
    {
        currentCrop = null;
        State = FieldState.NORMAL;
    }
    public void SetCrop(Crop cropToSet)
    {
        currentCrop = cropToSet; 
    }

    public void Interac()
    {
        if (currentCrop.IsRiped)
        {
            currentCrop.Harvest();
            ResetState();
        }
    }
}
