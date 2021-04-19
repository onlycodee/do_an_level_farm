using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    public static LevelManager Instance;
    Scene curScene;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void LoadNextLevel()
    {
        Debug.LogError("Load next level");
        StartCoroutine(COLoadNextLevel());
    }
    public IEnumerator COLoadNextLevel()
    {
        int currentLevel = PlayerPrefWrapper.CurrentLevel;
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("level_" + currentLevel));
        Debug.LogError("Unload scene successful");
        if (currentLevel + 1 <= ConstantValue.TOTAL_LEVEL)
        {
            currentLevel++;
            PlayerPrefWrapper.CurrentLevel = currentLevel;
        }
        LoadCurrentLevel();
    }

    public void LoadCurrentLevel()
    {
        levelText.text = "Level: " + PlayerPrefWrapper.CurrentLevel.ToString();
        Debug.LogError("Current level: " + PlayerPrefWrapper.CurrentLevel);
        curScene = SceneManager.GetSceneByName("level_" + PlayerPrefWrapper.CurrentLevel);
        if (curScene.isLoaded)
        {
            //SceneManager.SetActiveScene(curScene);
        } else
        {
            StartCoroutine(COLoadCurrentLevelScene("level_" + PlayerPrefWrapper.CurrentLevel));
        }
    }
    public IEnumerator COLoadCurrentLevelScene(string sceneName)
    {
        yield return FindObjectOfType<Fader>().FadeIn();
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return FindObjectOfType<Fader>().FadeOut();
        Inventory.Instance.Reset();
        FindObjectOfType<MissionBar>().LoadMissionData();
        FindObjectOfType<SeedBarManager>().DisplaySeedItems();
    }

    public int GetCurrentLevel()
    {
        int currentLevel = PlayerPrefWrapper.CurrentLevel;
        return currentLevel;
    }
}
