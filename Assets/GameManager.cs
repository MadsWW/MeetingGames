using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	//Add to CheckCard Event
	private void Start ()
    {
        CardBehaviour.CheckCard += CheckCorrectCall;
	}

    //Remove from CheckCard event
    private void OnDestroy()
    {
        CardBehaviour.CheckCard -= CheckCorrectCall;
    }

    //Check if the two selectedCards are equal to eachother and resets the selected cards afterwards.
    private void CheckCorrectCall(object sender, CheckCardEventArgs e)
    {
        bool sameCards = e.CardOne.spriteRen.sprite == e.CardTwo.spriteRen.sprite;

        if (sameCards)
        {
            ResetSelectedCards();
            print("Correct");
        }
        else
        {
            CardBehaviour.selectOne.ResetPosition();
            CardBehaviour.selectTwo.ResetPosition();
            ResetSelectedCards();
            print("Incorrect");
        }
    }

    //Reset selectedCards to null and can selectcards again.
    private void ResetSelectedCards()
    {
        CardBehaviour.canSelectCards = true;
        CardBehaviour.selectOne = null;
        CardBehaviour.selectTwo = null;
    }
}
