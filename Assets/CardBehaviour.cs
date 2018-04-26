using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour {

    public static event CheckCardDelegate CheckCard;

    public static CardBehaviour selectOne;
    public static CardBehaviour selectTwo;
    public static bool canSelectCards = true;

    public SpriteRenderer spriteRen;

    private Quaternion normalRotation = new Quaternion(0, 0, 0, 0);
    private Quaternion flippedRotation = new Quaternion(0, 180, 0, 0);

    private bool canSelectThisCard = true;

    //Flips card to flipped when set.
    private void Start()
    {
        ResetPosition();
    }

    //Flips card to normal around when clicked.
    private void OnMouseDown()
    {
        if (SetSelectedCard())
        {
            gameObject.transform.rotation = normalRotation;
        }
    }

    private bool SetSelectedCard()
    {
        if (canSelectCards && canSelectThisCard)
        {
            if (!selectOne)
            {
                selectOne = this;
                canSelectThisCard = false;
                return true;
            }
            else if (selectOne && selectOne != this)
            {
                selectTwo = this;
                canSelectCards = false;
                canSelectThisCard = false;
                Invoke("TriggerCheckCard", 1f);
                return true;                
            }
        }
        return false;
    }

    private void TriggerCheckCard()
    {
        CheckCardEventArgs e = new CheckCardEventArgs();
        e.CardOne = selectOne;
        e.CardTwo = selectTwo;
        CheckCard(gameObject, e);
    }

    //Sets sprite when instantiated
    public void SetCardInfo(Sprite cardSprite)
    {
        spriteRen.sprite = cardSprite;
    }

    //Puts cards back into play.
    public void ResetPosition()
    {
        gameObject.transform.rotation = flippedRotation;
        canSelectThisCard = true;
    }

}
