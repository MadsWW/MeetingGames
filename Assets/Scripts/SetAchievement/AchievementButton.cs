using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour {

    public static event SetAchievementOnCompletedDelegate SetAchievementOnCompletedEvent;
    public static event PayOutOnCompletedDelegate PayOutOnCompletedEvent;

    private Achievement _achievement;
    private int _achievementNumber;
    private Text text;

    private void OnEnable()
    {
        text = GetComponentInChildren<Text>();
    }

    public void SetInfo(Achievement achievement)
    {
        _achievement = achievement;
        SetAchievementText();
        CheckForCompleted();
    }

    private void SetAchievementText()
    {
        text.text = string.Format("{0}\nProgress: {1} / {2}", _achievement.Message, _achievement.AmountAchieved, _achievement.AmountToAchieve);
    }

    private void CheckForCompleted()
    {
        if (_achievement.AmountAchieved >= _achievement.AmountToAchieve)
        {
            if (!_achievement.IsUnlocked)
            {
                _achievement.IsUnlocked = true;
                _achievement.Message = "Completed!";
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
        args.AchievementNumber = _achievementNumber;
        args.AnAchievement = _achievement;
        SetAchievementOnCompletedEvent(args);
    }

    private void PayOut()
    {
        PayOutOnCompletedEventArgs args = new PayOutOnCompletedEventArgs();
        args.Reward = _achievement.CashReward;
        PayOutOnCompletedEvent(args);
    }
}
