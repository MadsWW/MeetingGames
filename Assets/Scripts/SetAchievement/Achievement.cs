using System;
using System.Xml;
using System.Xml.Serialization;
// Make into get properties
public class Achievement
{
    public int AmountToAchieve;
    public int AmountAchieved;
    public int CashReward;
    public bool IsUnlocked;
    public string Message;

    public Achievement()
    {
        AmountToAchieve = 1;
        AmountAchieved = 1;
        CashReward = 1;
        IsUnlocked = false;
        Message = "non message";
    }

}
