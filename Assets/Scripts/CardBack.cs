using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CardBack
{
    private int price;
    private Sprite cardSprite;

    public CardBack(int cost, Sprite deckSprite)
    {
        price = cost;
        cardSprite = deckSprite;
    }

    public int Price
    {
        get
        {
            return price;
        }
    }

    public Sprite CardSprite
    {
        get
        {
            return cardSprite;
        }
    }
}
