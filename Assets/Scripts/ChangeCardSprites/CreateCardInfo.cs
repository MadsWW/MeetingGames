﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCardInfo : MonoBehaviour {

    public event PushCardBackInfoDelegate PushCardInfo;

    private DataManager _dataManager;

    private List<CardInfo> cardBackInfo = new List<CardInfo>();
    private bool[] cardBackUnlocked = new bool[5];
    private int[] cardBackCosts = new int[] { 100, 200, 400, 400, 1000 };
    public Sprite[] cardBacksSprite;

    private List<CardInfo> cardFrontInfo = new List<CardInfo>();
    private bool[] cardFrontUnlocked = new bool[5];
    private int[] cardFrontCosts = new int[] { 100, 200, 400, 400, 1000 };
    public Sprite[] cardFrontSprite;

    private void OnEnable()
    {
        _dataManager = FindObjectOfType<DataManager>();
        CardInfoButton.SetCardInfoEvent += GetCardInfoFromButton;
    }

    private void OnDisable()
    {
        CardInfoButton.SetCardInfoEvent += GetCardInfoFromButton;
    }



    //Creates the data if there is non.
    #region CREATE_CARDINFO_DATA

    public void CreatCardBack()
    {
        for(int i = 0; i < cardBackUnlocked.Length; i++)
        {
            CardInfo info = new CardInfo();
            info.spriteNumber = i;
            info.price = cardBackCosts[i];
            info.isUnlocked = cardBackUnlocked[i];
            info.typeOfCard = CardType.CardBack;

            cardBackInfo.Add(info);
        }
    }

    public void CreateCardFront()
    {
        for (int i = 0; i < cardFrontUnlocked.Length; i++)
        {
            CardInfo info = new CardInfo();
            info.spriteNumber = i;
            info.price = cardFrontCosts[i];
            info.isUnlocked = cardFrontUnlocked[i];
            info.typeOfCard = CardType.CardFront;

            cardFrontInfo.Add(info);
        }
    }

    #endregion CREATE_CARDINFO_DATA


    //Sends on buttonpress from menu.
    // !!! Sends CardInfo * CardInfo (10 x 10 = 100) amount of events, can done be better?? More custom events?
    public void SendCardInfo()
    {
        for( int i = 0; i < cardBackInfo.Count; i++)
        {
            PushCardBackInfoEventArgs args = new PushCardBackInfoEventArgs();
            args.Card = cardBackInfo[i];
            args.CardSprite = cardBacksSprite[i];
            PushCardInfo(args);
        }

        for (int i = 0; i < cardFrontInfo.Count; i++)
        {
            PushCardBackInfoEventArgs args = new PushCardBackInfoEventArgs();
            args.Card = cardFrontInfo[i];
            args.CardSprite = cardFrontSprite[i];
            PushCardInfo(args);
        }
    }

    private void GetCardInfoFromButton(SetCardInfoUnlockedEventArgs e)
    {
        if(e.Card.typeOfCard == CardType.CardBack)
        {
            cardBackInfo[e.cardNumber] = e.Card;
        }
        else if( e.Card.typeOfCard == CardType.CardFront)
        {
            cardFrontInfo[e.cardNumber] = e.Card;
        }
    }

    




}
