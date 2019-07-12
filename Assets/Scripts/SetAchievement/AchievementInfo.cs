using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class AchievementInfo : MonoBehaviour
{
    //Event that sets achievement data
    public event SetAchievementDataDelegate SetAchievementDataEvent;

    //Needs to be saved.
    private List<Achievement> _achievements = new List<Achievement>();

    // For creating new achievements. //TODO Make XML of the achievements instead of Harcoded
    private bool[] achievementUnlocked = new bool[5];
    private string[] achievementMessage = new string[5];
    public Sprite[] AchievementSprites = new Sprite[5];
    private int[] amountToUnlock = new int[] { 10, 10, 10, 20, 200 };
    private int[] reward = new int[] { 100, 100, 200, 200, 300 };
    private int[] amountAchieved = new int[] { 0, 0, 0, 0, 0 };

    public List<Achievement> Achievements { get { return _achievements; } }

    private void Awake()
    {
        LoadAchievements();
    }

    private void LoadAchievements()
    {
        if (DataManager.LoadData() == null)
        {
            CreateAchievement();
        }
        else
        {
            _achievements = DataManager.LoadData().Achievements;
        }
    }

    private void OnEnable()
    {
        AchievementButton.SetAchievementOnCompletedEvent += OnAchievementCompleted;
    }
    private void OnDisable()
    {
        AchievementButton.SetAchievementOnCompletedEvent -= OnAchievementCompleted;
    }

    //Creates all the achievements in the array
    #region CREATES_ACHIEVEMENTS

    //Creates all achievements 
    private void CreateAchievement()
    {
        for (int i = 0; i < achievementMessage.Length; i++)
        {
            Achievement achievement = new Achievement();
            achievement.AmountAchieved = amountAchieved[i];
            achievement.AmountToAchieve = amountToUnlock[i];
            achievement.CashReward = reward[i];
            achievement.IsUnlocked = achievementUnlocked[i];
            achievement.Message = achievementMessage[i];

            _achievements.Add(achievement);
        }
        SetAchievementMessage();
    }

    // Sets the message for all the achievements
    private void SetAchievementMessage()
    {
        achievementMessage[0] = string.Format("Win {0} Relax Mode Games", amountToUnlock[0]);
        achievementMessage[1] = string.Format("Win {0} Turn Mode Games", amountToUnlock[1]);
        achievementMessage[2] = string.Format("Win with {0} Turns Left", amountToUnlock[2]);
        achievementMessage[3] = string.Format("Win with {0} Seconds Left", amountToUnlock[3]);
        achievementMessage[4] = string.Format("Survive for {0} Seconds.", amountToUnlock[4]);
    }

    #endregion CREATES_ACHIEVEMENTS

    //Pushes data to AchievementButton.
    #region SET_ACHIEVEMENT_EVENT_FUNCTIONS

    public void SetAchievements()
    {
        CheckAchievement(0, _achievements[0]);
        CheckAchievement(1, _achievements[1]);
        CheckAchievement(2, _achievements[2]);
        CheckAchievement(3, _achievements[3]);
        CheckAchievement(4, _achievements[4]);
    }

    private void CheckAchievement(int achievementNumber, Achievement achievement)
    {
        SetAchievementDataEventArgs args = new SetAchievementDataEventArgs();
        args.AchievementNumber = achievementNumber;
        args.AnAchievement = achievement;

        SetAchievementDataEvent(args);
    }

    #endregion SET_ACHIEVEMENT_EVENT_FUNCTIONS

    //Sets the achievement in the achievement list when one is completed.
    private void OnAchievementCompleted(SetAchievementOnCompletedEventArgs e)
    {
        _achievements[e.achievementNumber] = e.achievement;
    }




}
