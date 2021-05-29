using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseDialog : Dialog 
{
    public void PlayAgain()
    {
        Close();
        FindObjectOfType<LevelManager>().LoadCurrentLevel();
    }
}
