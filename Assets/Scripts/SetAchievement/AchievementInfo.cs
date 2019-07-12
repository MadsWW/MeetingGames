using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class AchievementInfo : MonoBehaviour
{
    //TODO Make Achievements into binary file and use that to load data.


    //Event that sets achievement data
    public event SetAchievementDataDelegate SetAchievementDataEvent;
    public event CreateAchievementsDelegate CreateAchievementsEvent;

    private GameManager _gameManager;


    // For creating new achievements. //TODO Make XML of the achievements instead of Harcoded
    private bool[] achievementUnlocked = new bool[5];
    private string[] achievementMessage = new string[5];
    public Sprite[] AchievementSprites = new Sprite[5];
    private int[] amountToUnlock = new int[] { 10, 10, 10, 20, 200 };
    private int[] reward = new int[] { 100, 100, 200, 200, 300 };
    private int[] amountAchieved = new int[] { 0, 0, 0, 0, 0 };


    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    //Creates all the achievements in the array
    #region CREATES_ACHIEVEMENTS

    //Creates all achievements 
    public void CreateAchievement()
    {
        SetAchievementMessage();
        for (int i = 0; i < achievementMessage.Length; i++)
        {
            Achievement achievement = new Achievement();
            achievement.AmountAchieved = amountAchieved[i];
            achievement.AmountToAchieve = amountToUnlock[i];
            achievement.CashReward = reward[i];
            achievement.IsUnlocked = achievementUnlocked[i];
            achievement.Message = achievementMessage[i];

            CreateAchievementEventArgs args = new CreateAchievementEventArgs();
            args.achievement = achievement;
            CreateAchievementsEvent(args);
        }

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
        for(int i = 0; i < _gameManager.Achievements.Count; i++)
        {
            print(i);
            CheckAchievement(i, _gameManager.Achievements[i]);
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
