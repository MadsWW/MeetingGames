using System;

public class CheckCardEventArgs : EventArgs
{
    public CardBehaviour CardOne { get; set; }
    public CardBehaviour CardTwo { get; set; }
}
