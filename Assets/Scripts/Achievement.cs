using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour {

    // Make achievement from code instead of predefining them.
    // From xml if you only want to load a part of them so less will be in memory.
    // Build Achievement Prefab from code when achievement menu is loaded.


    public AchievementType TypeOfAchievement;
    public int AmountToAchieve;
    public int AmountAchieved;
    public string Message;
    private bool IsUnlocked = false;
    private AchievementTracker aTracker;

    //Unlockable rewarde here;

    public Text AchievementText;

    private void Awake()
    {
        aTracker = FindObjectOfType<AchievementTracker>();
        SetAchievementText();
    }

    private void OnEnable()
    {
        aTracker.UnlockAchievement += CheckForCompleted;
    }

    private void OnDisable()
    {
        aTracker.UnlockAchievement -= CheckForCompleted;
    }

    private void CheckForCompleted(object sender, UnlockAchievementEventArgs e)
    {
        if(TypeOfAchievement == e.AchieveType && e.Amount > AmountAchieved)
        {
            AmountAchieved = e.Amount;
            SetAchievementText();
        }

        if(TypeOfAchievement == e.AchieveType && AmountToAchieve == e.Amount)
        {
            IsUnlocked = true;
            //Enable the reward.
        }
    }

    private void SetAchievementText()
    {
        AchievementText.text = string.Format("{0}\nProgress: {1} / {2}", Message, AmountAchieved, AmountToAchieve);
    }
}
