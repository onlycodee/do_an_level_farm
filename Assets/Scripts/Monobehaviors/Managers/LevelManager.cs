using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void LoadNextLevel()
    {
        int currentLevel = PlayerPrefWrapper.CurrentLevel;
        if (currentLevel + 1 <= ConstantValue.TOTAL_LEVEL)
        {
            currentLevel++;
            PlayerPrefWrapper.CurrentLevel = currentLevel;
            //SceneManager.LoadScene("Level_" + PlayerPrefWrapper.CurrentLevel); 
        } 
    }

    public int GetCurrentLevel()
    {
        int currentLevel = PlayerPrefWrapper.CurrentLevel;
        return currentLevel;
    }
}
