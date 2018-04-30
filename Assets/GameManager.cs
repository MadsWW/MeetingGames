using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private int rows = 4;
    private int cols = 6;
    private int amountOfSets = 12;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    //Add to CheckCard Event
    private void OnEnable ()
    {
        CardBehaviour.CheckCard += CheckCorrectCall;
        SceneManager.sceneLoaded += LoadedScene;
	}

    //Remove from CheckCard event
    private void OnDisable()
    {
        CardBehaviour.CheckCard -= CheckCorrectCall;
        SceneManager.sceneLoaded -= LoadedScene;
    }


    //Sets rows/cols depending on menu button input.
    public void SetRowsCols(int row, int col)
    {
        rows = row;
        cols = col;
        CalcAmountOfSets();
    }

    //Calculates the amount of sets the player has to solve.
    private void CalcAmountOfSets()
    {
        amountOfSets = (rows * cols) / 2;
    }

    //Passes amount of rows/cols/sets to DeckBuilder when the memory scene is loaded.
    private void LoadedScene(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Memory" && FindObjectOfType<DeckBuilder>())
        {
            DeckBuilder buildDeck = FindObjectOfType<DeckBuilder>();

            buildDeck.GetSpriteSets(amountOfSets);
            buildDeck.PlaceCards(rows, cols);
        }
    }




#region CHECKCORRECTANSWER_METHODS

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
#endregion CHECKCORRCTANSWER_METHODS

}
