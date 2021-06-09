using TMPro;
using UnityEngine;

public class StartGameDialog : Dialog 
{
    [SerializeField] TextMeshProUGUI timeTxt, goldText;
    [SerializeField] RectTransform startItemParent, targetItemParent;
    [SerializeField] ItemUI startItemPrefab, targetItemPrefab; 
    public LevelData levelData;
    protected override void Start()
    {
        base.Start();
        SetLevelData(levelData);
    }
    public void SetLevelData(LevelData levelDataParam) {
        levelData = levelDataParam;
        for (int i = 0; i < levelDataParam.GetInitSeeds().Length; i++) {
            ItemUI instance = Instantiate(startItemPrefab, startItemParent); 
            instance.SetItemHolder(levelData.GetInitSeeds()[i]);
        }
        MissionBase[] missions = levelData.GetMissions();;
        foreach (var mission in missions) {
            ItemHolder[] missionItems = mission.GetItems(); 
            for (int i = 0; i < missionItems.Length; i++) {
                ItemUI instance = Instantiate(targetItemPrefab, targetItemParent); 
                instance.SetItemHolder(missionItems[i]);
            }
        } 
    }
}
