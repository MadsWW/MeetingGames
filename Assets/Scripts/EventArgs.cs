﻿using System;

//Rename File To Resemble what is inside it.

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

public class UnlockAchievementEventArgs : EventArgs
{
    public AchievementType AchieveType { get; set; }
    public int Amount { get; set; }
}

public class PushCardBackInfoEventArgs : EventArgs
{
    public int CardInfoNumber { get; set; }
    public CardType TypeOfCard { get; set; }
    public CardInfo CardBackInfo { get; set; }

}