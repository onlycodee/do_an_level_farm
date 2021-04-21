using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDialog : Dialog 
{
    public void NextLevel()
    {
        Close();
        FindObjectOfType<LevelManager>().LoadNextLevel();
    }
}
