using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionBase : ScriptableObject
{
    public abstract bool CheckIfCompleted();
    public abstract void UpdateUI();
    public abstract ItemHolder[] GetItems(); 
    //public abstract GameObject GetUIGameObject();
}
