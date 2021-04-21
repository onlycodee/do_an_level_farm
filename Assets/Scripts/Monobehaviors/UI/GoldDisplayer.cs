using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDisplayer : MonoBehaviour
{
    public void Show()
    {
        GetComponent<Canvas>().overrideSorting = true; 
    }

    public void Hide()
    {
        GetComponent<Canvas>().overrideSorting = false; 
    }
}
