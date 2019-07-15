using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardType { CardFront, CardBack }

public class CardInfoButton : MonoBehaviour {

    public static event OnPurchaseCompletedDelegate PurchaseItemEvent;

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
    private CreateCardInfo _createCardInfo;
    private GameManager _gameManager;
    private DataManager _dataManager;

    private void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
        buttonSprite = GetComponent<Image>();
        _gameManager = FindObjectOfType<GameManager>(); 
        _createCardInfo = FindObjectOfType<CreateCardInfo>();
        _dataManager = FindObjectOfType<DataManager>(); 
    }

    private void OnEnable()
    {
        _createCardInfo.PushCardInfo += SetInfo;
        button.onClick.AddListener(SelectCard);
    }

    private void OnDisable()
    {
        _createCardInfo.PushCardInfo -= SetInfo;
        button.onClick.RemoveListener(SelectCard);
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
            if (CheckEnoughCoins())
            {
                cardInfo.isUnlocked = true;
                text.text = string.Empty;
            }
            else
            {
                //(else)maybe setmessage not enough money.
            }
        }
        else
        {
            //TODO Set Selected card front in gamemanager.
        }
    }
    public bool CheckEnoughCoins()
    {
        if (cardInfo.price >= 0 && cardInfo.price <= _dataManager.Coins)
        {
            PurchaseItemSuccesfull();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void PurchaseItemSuccesfull()
    {
        OnPurchaseCompletedEventArgs args = new OnPurchaseCompletedEventArgs();
        args.Cost = cardInfo.price;
        print(cardInfo.price);
        PurchaseItemEvent(args);
    }


}
