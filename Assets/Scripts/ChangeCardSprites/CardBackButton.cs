using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardType { CardFront, CardBack }

public class CardBackButton : MonoBehaviour {
    
    //Number that pulls data from CreateCardInfo;
    public int cardSpriteNumber;
    public CardType cardType;

    //Data set when initialized
    private int price;
    private bool IsUnlocked = false;



    // GameObject Components
    private Button button;
    private Image buttonSprite;

    // Needed to check when event is triggered
    private CreateCardInfo createCardInfo;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonSprite = GetComponent<Image>();
        createCardInfo = FindObjectOfType<CreateCardInfo>();

        button.onClick.AddListener(SelectCard);

        CheckIfFree();
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
    private void SetInfo(object sender, PushCardBackInfoEventArgs e)
    {
        if (cardSpriteNumber == e.CardInfoNumber && cardType == e.TypeOfCard)
        {
            CardInfo cd = e.CardBackInfo;

            price = cd.Price;
            IsUnlocked = cd.IsUnlocked;
            buttonSprite.sprite = cd.CardSprite;

        }
    }

    //If item is free, the item is unlocked.
    private void CheckIfFree()
    {
        if(price == 0)
        {
            IsUnlocked = true;
            //Maybe turn 
        }
    }

    //Buy item if not unlocked else select it as cardback/front when clicked on.
    private void SelectCard()
    {
        if (!IsUnlocked)
        {
            //Buy - return bool if buy is succesfull
            //Set data to CreateCardInfo so bool IsUnlocked can be saved when bought.
            //Maybe also set price to 0 and save that info, just to be sure.
        }
        else
        {
            //Set Selected card front in gamemanager.
        }
    }



}
