using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        Inventory.Instance.Reset();
    }
}
