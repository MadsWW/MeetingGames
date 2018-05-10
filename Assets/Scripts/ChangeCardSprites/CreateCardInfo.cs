using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCardInfo : MonoBehaviour {

    public event PushCardBackInfoDelegate PushCardInfo;

    private CardInfo[] CardBackInfo = new CardInfo[5];
    private bool[] CardBackUnlocked = new bool[5];
    private CardInfo[] CardFrontInfo = new CardInfo[5];
    private bool[] CardFrontUnlocked = new bool[5];

    public Sprite[] CardBacksSprite;
    public Sprite[] CardFrontSprite;



	// Use this for initialization
	void Start ()
    {
        CreatCardBack();
        CreateCardFront();
        SendCardInfo();
    }

    private void CreatCardBack()
    {
        CardBackInfo[0] = new CardInfo(100, CardBackUnlocked[0], CardBacksSprite[0], CardType.CardBack);
        CardBackInfo[1] = new CardInfo(200, CardBackUnlocked[1], CardBacksSprite[1], CardType.CardBack);
        CardBackInfo[2] = new CardInfo(400, CardBackUnlocked[2], CardBacksSprite[2], CardType.CardBack);
        CardBackInfo[3] = new CardInfo(400, CardBackUnlocked[3], CardBacksSprite[3], CardType.CardBack);
        CardBackInfo[4] = new CardInfo(1000, CardBackUnlocked[4], CardBacksSprite[4], CardType.CardBack);
    }

    private void CreateCardFront()
    {
        CardFrontInfo[0] = new CardInfo(100, CardFrontUnlocked[0], CardFrontSprite[0], CardType.CardFront);
        CardFrontInfo[1] = new CardInfo(200, CardFrontUnlocked[1], CardFrontSprite[1], CardType.CardFront);
        CardFrontInfo[2] = new CardInfo(400, CardFrontUnlocked[2], CardFrontSprite[2], CardType.CardFront);
        CardFrontInfo[3] = new CardInfo(400, CardFrontUnlocked[3], CardFrontSprite[3], CardType.CardFront);
        CardFrontInfo[4] = new CardInfo(1000, CardFrontUnlocked[4], CardFrontSprite[4], CardType.CardFront);
    }

    //Sends Events Card'Back'Button
    // !!! Sends CardInfo * CardInfo (10 x 10 = 100) amount of events, can done be better??
    private void SendCardInfo()
    {
        for( int i = 0; i < CardBackInfo.Length; i++)
        {
            PushCardBackInfoEventArgs args = new PushCardBackInfoEventArgs();
            args.CardBackInfo = CardBackInfo[i];
            args.TypeOfCard = CardType.CardBack;
            args.CardInfoNumber = i;
            PushCardInfo(args);
        }

        for (int i = 0; i < CardFrontInfo.Length; i++)
        {
            PushCardBackInfoEventArgs args = new PushCardBackInfoEventArgs();
            args.CardBackInfo = CardFrontInfo[i];
            args.TypeOfCard = CardType.CardFront;
            args.CardInfoNumber = i;
            PushCardInfo(args);
        }
    }

    




}
