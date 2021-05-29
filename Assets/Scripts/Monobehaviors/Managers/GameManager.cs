using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance;
    public bool isFinished = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(Instance.gameObject);
        Inventory.Instance.Reset();
    }

    private void Start()
    {
        LevelManager.Instance.LoadCurrentLevel();
    }

    public void Win()
    {
        if (isFinished) return;
        isFinished = true;
        FindObjectOfType<LevelTimer>().Pause();
        DialogController.Instance.ShowDialog(DialogType.WIN);
    }
    public void Lose()
    {
        if (isFinished) return;
        isFinished = true;
        DialogController.Instance.ShowDialog(DialogType.LOSE);
    }
}
