using System;

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
    public string TurnLeft { get; set; }
}

public class ChangeTimeTextEventArgs : EventArgs
{
    public string TimeLeft { get; set; }
}

public class UnlockAchievementEventArgs : EventArgs
{
    public AchievementType AchieveType { get; set; }
    public int Amount { get; set; }
}
