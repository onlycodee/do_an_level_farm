using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionBase : ScriptableObject
{
    public abstract bool CheckIfFinished();
    public abstract void UpdateUI();
}
