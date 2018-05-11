using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour {

    private Achievement achievement;

    //GameObject Needed for event trigger.
    private AchievementInfo aInfo;

    public int AchievementNumber;

    //GamObject Child Components
    private Text text;
    //private Image image;

    private void OnEnable()
    {
        aInfo = FindObjectOfType<AchievementInfo>();
        text = GetComponentInChildren<Text>();
        print(gameObject.name + " Text avialable " + text);
        //image = GetComponentInChildren<Image>();
        aInfo.SetAchievementDataEvent += SetAchievement;
    }

    private void OnDisable()
    {
        aInfo.SetAchievementDataEvent -= SetAchievement;
    }

    //Gets send an achievements and set this variable achievement
    private void SetAchievement(SetAchievementDataEventArgs e)
    {
        if (AchievementNumber == e.AchievementNumber)
        {
            achievement = e.AnAchievement;
            SetAchievementText();
            //CheckForCompleted();
        }
    }

    //Checks if the achievement is completed by checking if value achieved is equal or higher to value needed to complete
    private void CheckForCompleted()
    {
        if (achievement.AmountAchieved >= achievement.AmountToAchieve)
        {
            if (!achievement.IsUnlocked)
            {
                //achievement.isUnlocked = true;
                //Add reward to coinamount - still has to be implemented
            }
            else //IsUnLocked
            {
                //Highlight if unlocked or something.
            }
        }
    }

    //Sets text element from gamobject
    private void SetAchievementText()
    {
        text.text = string.Format("{0}\nProgress: {1} / {2}", achievement.Message, achievement.AmountAchieved, achievement.AmountToAchieve);
    }
}
