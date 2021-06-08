using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreConfig : MonoBehaviour
{
    [SerializeField] int currentLevel = 1;

    private void Start() {
        PlayerPrefWrapper.CurrentLevel = currentLevel;
    }
}
