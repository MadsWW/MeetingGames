using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Achievement
{
    public int amountToAchieve;
    public int amountAchieved;
    public int cashReward;
    public bool isUnlocked = false;
    public string message;


    //!!Work on naming the params, may cause confusion for outsider. 
    public Achievement(int achieveAmount, int achievedAmount, int reward, bool unlock, string achievInfo)
    {
        amountToAchieve = achieveAmount;
        amountAchieved = achievedAmount;
        cashReward = reward;
        isUnlocked = unlock;
        message = achievInfo;
    }

}
