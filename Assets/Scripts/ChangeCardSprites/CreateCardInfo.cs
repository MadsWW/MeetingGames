using System.Collections.Generic;
using UnityEngine;

public class CreateCardInfo : MonoBehaviour {

    public event PushCardBackInfoDelegate PushCardInfo;

    private DataManager _dataManager;

    public Sprite[] cardBacksSprite;
    public Sprite[] cardFrontSprite;

    private void Awake()
    {
        _dataManager = FindObjectOfType<DataManager>();
    }

    // !!! Sends CardInfo * CardInfo (10 x 10 = 100) amount of events, can done be better?? More custom events?
    public void SendCardInfo()
    {
        for( int i = 0; i < _dataManager.CardBackInfo.Count; i++)
        {
            PushCardBackInfoEventArgs args = new PushCardBackInfoEventArgs();
            args.Card = _dataManager.CardBackInfo[i];
            args.CardSprite = cardBacksSprite[i];
            PushCardInfo(args);
        }

        for (int i = 0; i < _dataManager.CardFrontInfo.Count; i++)
        {
            PushCardBackInfoEventArgs args = new PushCardBackInfoEventArgs();
            args.Card = _dataManager.CardFrontInfo[i];
            args.CardSprite = cardFrontSprite[i];
            PushCardInfo(args);
        }
    }
}
