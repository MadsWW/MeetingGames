using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardType { CardFront, CardBack }

public class CardInfoButton : MonoBehaviour {

    public static event SetCardInfoUnlockedDelegate SetCardInfoEvent;

    // Public variables. To detect which type and sprite it is.
    public int cardSpriteNumber;
    public CardType cardType;

    //Data set when initialized
    private CardInfo cardInfo;



    // GameObject Components
    private Button button;
    private Text text;
    private Image buttonSprite;

    // Needed to check when event is triggered
    private CreateCardInfo createCardInfo;
    private GameManager gManager;

    private void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
        buttonSprite = GetComponent<Image>();
        gManager = FindObjectOfType<GameManager>(); 
        createCardInfo = FindObjectOfType<CreateCardInfo>();

        button.onClick.AddListener(SelectCard);

        
    }

    private void OnEnable()
    {
        createCardInfo.PushCardInfo += SetInfo;

    }

    private void OnDisable()
    {
        createCardInfo.PushCardInfo -= SetInfo;
    }
    
    //When PushCardBackInfoEvent is triggered
    private void SetInfo(PushCardBackInfoEventArgs e)
    {
        if (cardSpriteNumber == e.Card.spriteNumber && cardType == e.Card.typeOfCard)
        {
            cardInfo = e.Card;
            buttonSprite.sprite = e.CardSprite;
            CheckUnlocked();
        }
    }

    private void CheckUnlocked()
    {
        if (cardInfo.isUnlocked)
        {
            text.text = string.Empty;
        }
        else
        {
            text.text = "Cost: " + cardInfo.price.ToString(); 
        }
    }

    //Buy item if not unlocked else select it as cardback/front when clicked on.
    private void SelectCard()
    {
        if (!cardInfo.isUnlocked)
        {
            //Set data to CreateCardInfo so bool IsUnlocked can be saved when bought.
            if (gManager.CheckEnoughCoins(cardInfo.price))
            {
                cardInfo.isUnlocked = true;
                text.text = string.Empty;
            }
            //(else)maybe setmessage not enough money.
        }
        else
        {
            //Set Selected card front in gamemanager.
        }
    }

    private void PushCardInfo()
    {
        SetCardInfoUnlockedEventArgs args = new SetCardInfoUnlockedEventArgs();
        args.cardNumber = cardInfo.spriteNumber;
        args.Card = cardInfo;
        SetCardInfoEvent(args);
    }


}
