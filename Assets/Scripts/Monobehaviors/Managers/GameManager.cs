using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Inventory.Instance.Reset();
    }

    private void Start()
    {
        LevelManager.Instance.LoadCurrentLevel();
    }
}
