﻿using System;
using UnityEngine;


public class CheckCardEventArgs : EventArgs
{
    public CardBehaviour CardOne { get; set; }
    public CardBehaviour CardTwo { get; set; }
}

public class SetBoardSizeEventArgs : EventArgs
{
    public int BoardWidth { get; set; }
    public int BoardHeight { get; set; }
}

public class ChangeSetsTextEventArgs : EventArgs
{
    public string CurrentSets { get; set; }
    public string AmountOfSets { get; set; }
}

public class ChangeTurnTextEventArgs : EventArgs
{
    public int TurnLeft { get; set; }
}

public class ChangeTimeTextEventArgs : EventArgs
{
    public int TimeLeft { get; set; }
}

public class ChangeCoinTextEventArgs : EventArgs
{
    public int Coins { get; set; }
}

public class SetAchievementDataEventArgs: EventArgs
{
    public int AchievementNumber { get; set; }
    public Achievement AnAchievement { get; set; }
}

public class SetAchievementOnCompletedEventArgs : EventArgs
{
    public int AchievementNumber { get; set; }
    public Achievement AnAchievement { get; set; }
}

public class AchievedAmountForAchievementEventArgs : EventArgs
{
    public int AchievementNumber { get; set; }
    public int AmountAchieved { get; set; }
}

public class PayOutOnCompletedEventArgs : EventArgs
{
    public int Reward { get; set; }
}

public class OnPurchaseCompletedEventArgs : EventArgs
{
    public int Cost { get; set; }
}

public class SendUpdatedCardInfoEventArgs : EventArgs
{
    public CardInfo CardInfo { get; set; }
}




