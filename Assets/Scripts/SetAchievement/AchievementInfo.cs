using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class AchievementInfo : MonoBehaviour
{
    //Event that sets achievement data
    public event SetAchievementDataDelegate SetAchievementDataEvent;


    // Class to save data.
    private DataManager dataManager;


    //Needs to be saved.
    private Achievement[] achievements = new Achievement[5];

    // For creating new achievements.
    private bool[] achievementUnlocked = new bool[5];
    private string[] achievementMessage = new string[5];
    public Sprite[] AchievementSprites = new Sprite[5];
    private int[] amountToUnlock = new int[] { 10, 10, 10, 20, 200 };
    private int[] reward = new int[] { 25, 25, 25, 50, 100 };
    private int[] amountAchieved = new int[] { 0, 0, 0, 0, 0 };


    //Saves/Loads data on Disable/Enable
    #region LOAD_METHOD

    private void OnEnable()
    {
        dataManager = new DataManager();
        LoadData();
    }
    private void OnDisable()
    {
        SaveData();
    }
    private void LoadData()
    {
        if(dataManager.LoadData() == null)
        {

            SetAchievementMessage();
            CreateAchievement();
            SetAchievements();
        }
        if (dataManager.LoadData() != null)
        {
            GameData data = new GameData();
            data = dataManager.LoadData();
            achievements = data.achievements;
            SetAchievements();
        }
    }

    private void SaveData()
    {
        GameData data = new GameData();
        data.achievements = achievements;
        dataManager.SaveData(data);
    }

    #endregion LOAD_METHOD

    //Creates all the achievements in the array
    #region CREATES_ACHIEVEMENTS

    // Sets the message for all the achievements
    private void SetAchievementMessage()
    {
        achievementMessage[0] = string.Format("Win {0} Relax Mode Games", amountToUnlock[0]);
        achievementMessage[1] = string.Format("Win {0} Turn Mode Games", amountToUnlock[1]);
        achievementMessage[2] = string.Format("Win with {0} Turns Left", amountToUnlock[2]);
        achievementMessage[3] = string.Format("Win with {0} Seconds Left", amountToUnlock[3]);
        achievementMessage[4] = string.Format("Survive for {0} Seconds.", amountToUnlock[4]);
    }

    //Creates all achievements 
    private void CreateAchievement()
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            achievements[i] = new Achievement(amountToUnlock[i], amountAchieved[i], reward[i], achievementUnlocked[i], achievementMessage[i]);
        }
    }

    #endregion CREATES_ACHIEVEMENTS

    //Pushes data to AchievementButton.
    #region SET_ACHIEVEMENT_EVENT_FUNCTIONS

    public void SetAchievements()
    {

        CheckAchievement(0, achievements[0]);
        CheckAchievement(1, achievements[1]);
        CheckAchievement(2, achievements[2]);
        CheckAchievement(3, achievements[3]);
        CheckAchievement(4, achievements[4]);

        print("Send all achievementbutton events");

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
