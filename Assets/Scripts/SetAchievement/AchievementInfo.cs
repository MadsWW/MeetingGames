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


    //Gets dataManager to get to access to save/load functions.
    private void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
    }

    //Creates achievements + Sends event
    private void Start()
    {
        SetAchievementMessage();
        CreateAchievement();
        SetAchievements();
    }

    //Saves/Loads data on Disable/Enable
    #region SAVE_LOAD_METHODS

    private void OnEnable()
    {
        LoadData();
    }

    private void OnDisable()
    {
        SaveData();
    }


    private void LoadData()
    {
        GameData gd = dataManager.LoadData();
        achievements = gd.AchievementsData;
    }

    private void SaveData()
    {
        GameData data = new GameData();
        data.AchievementsData = achievements;
        dataManager.SaveData(data);
    }
    #endregion SAVE_LOAD_FUNTIONS

    //Creates all the achievements in the array
    #region CREATES_ACHIEVEMENTS

    // string data from xml and then for loop can be achieved.
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
        for (int i = 0; i < achievements.Length; i++)
        {
            CheckAchievement(i, achievements[i]);
        }
    }

    private void CheckAchievement(int achievementNumber, Achievement achievement)
    {
        SetAchievementDataEventArgs args = new SetAchievementDataEventArgs();
        args.AchievementNumber = achievementNumber;
        args.AnAchievement = achievement;

        SetAchievementDataEvent(this, args);
    }

    #endregion SET_ACHIEVEMENT_EVENT_FUNCTIONS

    //Only here when statistics page is made.
    //returns amountachieved data from specific achievement.
    public int GetAchievementInfo(int achievementNr)
    {
        return achievements[achievementNr].AmountAchieved;
    }





}
