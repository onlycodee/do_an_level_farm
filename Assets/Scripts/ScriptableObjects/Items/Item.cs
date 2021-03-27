using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public Sprite Avatar;
    public string Id = "";

    private void Awake()
    {
        Id = Guid.NewGuid().ToString(); 
    }

    private void OnValidate()
    {
        if (Id == "")
        {
            Id = Guid.NewGuid().ToString(); 
        }
    }

    public void Init()
    {
        SpecificInit();
    }
    public abstract void SpecificInit(); 
}
