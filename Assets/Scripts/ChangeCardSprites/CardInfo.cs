using System;
using System.Xml;
using System.Xml.Serialization;


// Make into properties
public class CardInfo
{
    public int spriteNumber;
    public int price;
    public bool isUnlocked;
    public CardType typeOfCard;

    public CardInfo()
    {
        spriteNumber = 0;
        price = 0;
        isUnlocked = false;
        typeOfCard = CardType.CardBack;
    }

}
