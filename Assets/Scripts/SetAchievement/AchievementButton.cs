using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour {

    public static event SetAchievementOnCompletedDelegate SetAchievementOnCompletedEvent;
    public static event PayOutOnCompletedDelegate PayOutOnCompletedEvent;

    //private go variable
    private Achievement achievement;

    //public go variable
    public int AchievementNumber;

    //GameObject Needed for event trigger.
    private AchievementInfo aInfo;



    //GamObject Child Components
    private Text text;
    //private Image image;

    private void OnEnable()
    {
        aInfo = FindObjectOfType<AchievementInfo>();
        text = GetComponentInChildren<Text>();
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
            CheckForCompleted();
        }
    }

    //Checks if the achievement is completed by checking if value achieved is equal or higher to value needed to complete
    private void CheckForCompleted()
    {
        if (achievement.AmountAchieved >= achievement.AmountToAchieve)
        {
            if (!achievement.IsUnlocked)
            {
                achievement.IsUnlocked = true;
                achievement.Message = "Completed!";
                SendOnCompleted();
                PayOut();
            }
            else //IsUnLocked
            {
                //Highlight if unlocked or something.
            }
        }
    }

    private void SendOnCompleted()
    {
        SetAchievementOnCompletedEventArgs args = new SetAchievementOnCompletedEventArgs();
        args.AchievementNumber = AchievementNumber;
        args.AnAchievement = achievement;
        SetAchievementOnCompletedEvent(args);
    }

    private void PayOut()
    {
        PayOutOnCompletedEventArgs args = new PayOutOnCompletedEventArgs();
        args.Reward = achievement.CashReward;
        PayOutOnCompletedEvent(args);
    }

    //Sets text element from gamobject
    private void SetAchievementText()
    {
        text.text = string.Format("{0}\nProgress: {1} / {2}", achievement.Message, achievement.AmountAchieved, achievement.AmountToAchieve);
    }
}
