using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour {

    public static event CheckCardDelegate CheckCard;

    public static CardBehaviour selectOne;
    public static CardBehaviour selectTwo;
    public static bool canSelectCards = true;

    public SpriteRenderer spriteRen;

    private Quaternion faceUpPosition = new Quaternion(0, 0, 0, 0);
    private Quaternion faceDownPosition = new Quaternion(0, 180, 0, 0);

    private bool canSelectThisCard = true;

    //Flips card to flipped when set.
    private void Start()
    {
        ResetPosition();
    }

    //Flips card to faceup position when clicked.
    private void OnMouseDown()
    {
        if (SetSelectedCard())
        {
            gameObject.transform.rotation = faceUpPosition;
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
        CheckCard(e);
    }

    //Sets sprite when instantiated
    public void SetCardInfo(Sprite cardSprite)
    {
        spriteRen.sprite = cardSprite;
    }

    //Puts cards back into play.
    public void ResetPosition()
    {
        gameObject.transform.rotation = faceDownPosition;
        canSelectThisCard = true;
    }

}
