using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Inventory.Instance.Reset();
    }

    private void OnDestroy()
    {
        Inventory.Instance.Reset();
    }
}
