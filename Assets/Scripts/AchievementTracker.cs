using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum AchievementType { RelaxWins, TurnWins, TurnsLeft, TimeLeft, TimeSurvived }


public  class AchievementTracker : MonoBehaviour
{
    public event UnlockAchievementDelegate UnlockAchievement;

    private static AchievementTracker aTracker = null;

    private DataManager dataManager;

    // data below should be saved

    private int relaxModeWins;
    private int turnModeWins;
    private int highestTurnsLeft;
    private int highestTimeLeft;
    private int highestTimeSurvived;

    private void Awake()
    {
        if (aTracker == null)
        {
            aTracker = this;
        }
        else
        {
            Destroy(gameObject);
        }

        dataManager = FindObjectOfType<DataManager>();
        if(dataManager.LoadData() != null)
        {
            GameData gameData = dataManager.LoadData();
            SetDataFromLoad(gameData);
        }


        DontDestroyOnLoad(gameObject);
    }

    private void SetDataFromLoad(GameData data)
    {
        relaxModeWins = data.relaxModeWins;
        turnModeWins = data.turnModeWins;
        highestTurnsLeft = data.highestTurnsLeft;
        highestTimeLeft = data.highestTimeLeft;
        highestTimeSurvived = data.highestTimeSurvived;
    }

    private GameData SetDataToSave()
    {
        GameData saveData = new GameData();
        saveData.relaxModeWins = relaxModeWins;
        saveData.turnModeWins = turnModeWins;
        saveData.highestTurnsLeft = highestTurnsLeft;
        saveData.highestTimeLeft = highestTimeLeft;
        saveData.highestTimeSurvived = highestTimeSurvived;

        return saveData;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += LoadedScene;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadedScene;

    }

    private void CheckAchievement(AchievementType aType, int amount)
    {
        UnlockAchievementEventArgs args = new UnlockAchievementEventArgs();
        args.AchieveType = aType;
        args.Amount = amount;

        UnlockAchievement(this, args);
    }

    //When menu scene is loaded set all achievements.
    private void LoadedScene(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            dataManager.SaveData(SetDataToSave());
        }
    }

    public void SetAchievements()
    {
        CheckAchievement(AchievementType.RelaxWins, relaxModeWins);
        CheckAchievement(AchievementType.TurnWins, turnModeWins);
        CheckAchievement(AchievementType.TurnsLeft, highestTurnsLeft);
        CheckAchievement(AchievementType.TimeLeft, highestTimeLeft);
        CheckAchievement(AchievementType.TimeSurvived, highestTimeSurvived);
    }


    //Only here when statistics page is made.
    #region //PROPERTIES

    public int RelaxModeWins
    {
        get
        {
            return relaxModeWins;
        }
    }

    public int TurnModeWins
    {
        get
        {
            return turnModeWins;
        }
    }

    public int HighestTurnsleft
    {
        get
        {
            return highestTurnsLeft;
        }
    }

    public int HighestTimeLeft
    {
        get
        {
            return highestTimeLeft;
        }
    }

    public int HighestTimeSurvived
    {
        get
        {
            return highestTimeSurvived;
        }
    }

    #endregion //PROPERTIES

    // Can be set in properties
    #region //SET_VARIABLES_FUNCTIONS

    public void SetRelaxModeWin()
    {
        relaxModeWins++;
    }

    public void SetTurnModeWin()
    {
        turnModeWins++;
    }

    public void SetHighestTurnsLeft(int turnsLeft)
    {
        if(turnsLeft > highestTurnsLeft)
        {
            highestTurnsLeft = turnsLeft;
        }
    }

    public void SetHighestTimeLeft(int timeLeft)
    {
        if(timeLeft > highestTimeLeft)
        {
            highestTimeLeft = timeLeft;
        }
    }

    public void SetHighestTimeSurvived(int survivedTime)
    {
        if(survivedTime > highestTimeSurvived)
        {
            highestTimeSurvived = survivedTime;
        }
    }

    #endregion //SET_VARIABLES_FUNCTIONS


}
