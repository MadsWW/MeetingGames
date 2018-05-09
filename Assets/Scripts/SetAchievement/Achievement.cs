using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Achievement
{

    // Make achievement from code instead of predefining them.
    // Build same as ChangeCardSprites scripts.

    // Variables set from achievementTracker

    private int amountToAchieve;
    private int amountAchieved;
    private int cashReward;
    private bool isUnlocked = false;
    private string message;


    //!!Work on naming the params, may cause confusion for outsider. 
    public Achievement(int achieveAmount, int achievedAmount, int reward, bool unlock, string achievInfo)
    {
        amountToAchieve = achieveAmount;
        amountAchieved = achievedAmount;
        cashReward = reward;
        isUnlocked = unlock;
        message = achievInfo;
    }

    public int AmountToAchieve
    {
        get
        {
            return amountToAchieve;
        }
    }

    public int AmountAchieved
    {
        get
        {
            return amountAchieved;
        }
    }

    public int CashReward
    {
        get
        {
            return cashReward;
        }
    }

    public bool IsUnlocked
    {
        get
        {
            return isUnlocked;
        }
    }

    public string Message
    {
        get
        {
            return message;
        }
    }

}
