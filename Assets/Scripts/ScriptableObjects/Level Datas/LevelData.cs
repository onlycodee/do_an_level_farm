using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "Level/Game level")]
public class LevelData : ScriptableObject 
{
    [SerializeField] int initGold = 0;
    [SerializeField] MissionBase[] missions;

    public int GetInitCoin()
    {
        return initGold;
    }

    public MissionBase[] GetMissions()
    {
        return missions;
    }

    public bool CheckIfLevelCompleted()
    {
        foreach (MissionBase mission in missions)
        {
            if (!mission.CheckIfCompleted())
            {
                return false;
            }
        }
        return true;
    }
}
