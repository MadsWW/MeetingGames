using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AchievementType { RelaxWins, TurnWins, TurnsLeft, TimeLeft, TimeSurvived }


//! Because this is not Monobehaviour does Achievements list get cleared automaticly? or do I need to clear it on disable?
//! Instead of static class maybe initialise this in gamemanager?
public static class AchievementTracker
{
    public static event UnlockAchievementDelegate UnlockAchievement;
    public static List<Achievement> Achievements = new List<Achievement>();

    // data below should be saved

    private static int relaxModeWins;
    private static int turnModeWins;
    private static int highestTurnsLeft;
    private static int highestTimeLeft;
    private static int highestTimeSurvived;

    #region //PROPERTIES

    public static int RelaxModeWins
    {
        get
        {
            return relaxModeWins;
        }
    }

    public static int TurnModeWins
    {
        get
        {
            return turnModeWins;
        }
    }

    public static int HighestTurnsleft
    {
        get
        {
            return highestTurnsLeft;
        }
    }

    public static int HighestTimeLeft
    {
        get
        {
            return highestTimeLeft;
        }
    }

    public static int HighestTimeSurvived
    {
        get
        {
            return highestTimeSurvived;
        }
    }

    #endregion //PROPERTIES

    #region //SET_VARIABLES_FUNCTIONS

    public static void SetRelaxModeWin()
    {
        relaxModeWins++;
        CheckAchievement(AchievementType.RelaxWins, relaxModeWins);
    }

    public static void SetTurnModeWin()
    {
        turnModeWins++;
        CheckAchievement(AchievementType.TurnWins, turnModeWins);
    }

    public static void SetHighestTurnsLeft(int turnsLeft)
    {
        if(turnsLeft > highestTurnsLeft)
        {
            highestTurnsLeft = turnsLeft;
            CheckAchievement(AchievementType.TurnsLeft, highestTurnsLeft);
        }
    }

    public static void SetHighestTimeLeft(int timeLeft)
    {
        if(timeLeft > highestTimeLeft)
        {
            highestTimeLeft = timeLeft;
            CheckAchievement(AchievementType.TimeLeft, highestTimeLeft);
        }
    }

    public static void SetHighestTimeSurvived(int survivedTime)
    {
        if(survivedTime > highestTimeSurvived)
        {
            highestTimeSurvived = survivedTime;
            CheckAchievement(AchievementType.TimeSurvived, highestTimeSurvived);
        }
    }

    #endregion //SET_VARIABLES_FUNCTIONS

    private static void CheckAchievement(AchievementType aType, int amount)
    {
        UnlockAchievementEventArgs args = new UnlockAchievementEventArgs();
        args.AchieveType = aType;
        args.Amount = amount;
        UnlockAchievement(null, args);
    }
}
