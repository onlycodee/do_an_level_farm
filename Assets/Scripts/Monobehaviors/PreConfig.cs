using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreConfig : MonoBehaviour
{
    [SerializeField] int currentLevel = 1;
    [SerializeField] bool isDebug = false;

    private void Start() {
        if (isDebug) {
            PlayerPrefWrapper.CurrentLevel = currentLevel;
        }
    }
}
