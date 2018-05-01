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
