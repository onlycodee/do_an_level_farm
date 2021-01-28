using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string Name;
    public Sprite Avatar;
    public void Init()
    {
        SpecificInit();
    }
    public abstract void SpecificInit(); 
}
