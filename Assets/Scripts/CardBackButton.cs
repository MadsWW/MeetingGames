using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CardBackButton : MonoBehaviour {

    private int price;

    private bool IsUnlocked = false;

    private Sprite sprite;

    private Button button;
    public Image buttonSprite;

    private void Start()
    {
        button = GetComponent<Button>();
        buttonSprite = GetComponent<Image>();
        button.onClick.AddListener(SelectCard);
        CheckIfFree();
    }

    private void CheckIfFree()
    {
        if(price == 0)
        {
            IsUnlocked = true;
        }
    }

    private void SelectCard()
    {
        if (!IsUnlocked)
        {
            //Buy - return bool if buy is succesfull
        }
        else
        {
            //Set Selected card front in gamemanager.
        }
    }

    public void SetInfo(int cost, Sprite cardSprite)
    {
        price = cost;
        sprite = cardSprite;
        buttonSprite.sprite = cardSprite;
    }

}
