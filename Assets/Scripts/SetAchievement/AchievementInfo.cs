using UnityEngine;

public  class AchievementInfo : MonoBehaviour
{
    public event SetAchievementDataDelegate SetAchievementDataEvent;

    private DataManager _dataManager;


    private void Awake()
    {
        _dataManager = FindObjectOfType<DataManager>();
    }

    //Pushes data to AchievementButton.
    #region SET_ACHIEVEMENT_EVENT_FUNCTIONS

    public void SetAchievements()
    {
        for(int i = 0; i < _dataManager.Achievements.Count; i++)
        {
            CheckAchievement(i, _dataManager.Achievements[i]);
        }
    }

    private void CheckAchievement(int achievementNumber, Achievement achievement)
    {
        SetAchievementDataEventArgs args = new SetAchievementDataEventArgs();
        args.AchievementNumber = achievementNumber;
        args.AnAchievement = achievement;

        SetAchievementDataEvent(args);
    }

    #endregion SET_ACHIEVEMENT_EVENT_FUNCTIONS
}
