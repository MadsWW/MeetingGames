using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour {

    public AchievementType TypeOfAchievement;
    public int AmountToAchieve;
    public int AmountAchieved;
    public string Message;
    private bool IsUnlocked = false;

    public Text AchievementText;

    private void Awake()
    {
        AchievementTracker.Achievements.Add(this);
        SetAchievementText();
    }

    private void OnEnable()
    {
        AchievementTracker.UnlockAchievement += CheckForCompleted;
    }

    private void OnDisable()
    {
        AchievementTracker.UnlockAchievement -= CheckForCompleted;
    }

    private void CheckForCompleted(object sender, UnlockAchievementEventArgs e)
    {
        AmountAchieved = e.Amount;

        if(TypeOfAchievement == e.AchieveType && AmountToAchieve == e.Amount)
        {
            IsUnlocked = true;
            //Enable the reward.
        }
    }

    private void SetAchievementText()
    {
        AchievementText.text = string.Format("{0}\nBest: {1} / {2}", Message, AmountAchieved, AmountToAchieve);
    }
}
