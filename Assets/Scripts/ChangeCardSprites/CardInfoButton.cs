using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardType { CardFront, CardBack }

public class CardInfoButton : MonoBehaviour {

    public static event OnPurchaseCompletedDelegate PurchaseItemEvent;

    //Data set when initialized
    private CardInfo cardInfo;

    // GameObject Components
    private Button button;
    private Text text;
    private Image buttonSprite;

    // Needed to check when event is triggered
    private DataManager _dataManager;

    private void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
        buttonSprite = GetComponent<Image>();
        _dataManager = FindObjectOfType<DataManager>(); 
    }

    private void OnEnable()
    {
        button.onClick.AddListener(SelectCard);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(SelectCard);
    }
    
    //When PushCardBackInfoEvent is triggered
    public void SetInfo(CardInfo card, Sprite sprite)
    {
        cardInfo = card;
        buttonSprite.sprite = sprite;
        CheckUnlocked();
    }

    private void CheckUnlocked()
    {
        text.text = string.Empty;
        if (cardInfo.isUnlocked) return;
        text.text = "Cost: " + cardInfo.price.ToString(); 
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
        PurchaseItemEvent(args);
    }


}
