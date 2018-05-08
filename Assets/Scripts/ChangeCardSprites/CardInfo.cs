using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CardInfo
{
    private int price;
    private bool isUnlocked;
    private Sprite cardSprite;
    private CardType typeOfCard;

    public CardInfo(int cost, bool unlocked, Sprite sprite, CardType cardType)
    {
        price = cost;
        isUnlocked = unlocked;
        cardSprite = sprite;
        typeOfCard = cardType;
    }

    public int Price
    {
        get
        {
            return price;
        }
    }

    public bool IsUnlocked
    {
        get
        {
            return isUnlocked;
        }
    }

    public Sprite CardSprite
    {
        get
        {
            return cardSprite;
        }
    }

    public CardType TypeOfCard
    {
        get
        {
            return typeOfCard;
        }
    }

}
