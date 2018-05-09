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
    private Image image;

    private void Awake()
    {
        aInfo = FindObjectOfType<AchievementInfo>();
        text = GetComponentInChildren<Text>();
        image = GetComponentInChildren<Image>();

    }

    private void OnEnable()
    {
        aInfo.SetAchievementDataEvent += SetAchievement;
    }

    private void OnDisable()
    {
        aInfo.SetAchievementDataEvent -= SetAchievement;
    }

    private void SetAchievement(object sender, SetAchievementDataEventArgs e)
    {
        if (AchievementNumber == e.AchievementNumber)
        {
            achievement = e.AnAchievement;
            CheckForCompleted();
        }
    }

    private void CheckForCompleted()
    {
        SetAchievementText();

        if (achievement.AmountAchieved >= achievement.AmountToAchieve)
        {
            // if already unlock, no reward added;
            //achievement.IsUnlocked = true;
            //In Achievement for Set - GetType()  Achievementbutton;
            //Add reward to coinamount - still has to be implemented
            //Highlight if unlocked or something.
           
        }
    }

    private void SetAchievementText()
    {
        text.text = string.Format("{0}\nProgress: {1} / {2}", achievement.Message, achievement.AmountAchieved, achievement.AmountToAchieve);
    }
}
