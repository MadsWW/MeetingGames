using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public event ChangeSetsTextDelegate ChangeSetsTextEvent;

    private int rows;
    private int cols;
    private int amountOfSets;
    private int correctSets;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    //Add to CheckCard Event
    private void OnEnable ()
    {
        CardBehaviour.CheckCard += CheckCorrectCall;
        SceneManager.sceneLoaded += LoadedScene;
        LevelSizeButton.BoardSize += SetupGame;
	}

    //Remove from CheckCard event
    private void OnDisable()
    {
        CardBehaviour.CheckCard -= CheckCorrectCall;
        SceneManager.sceneLoaded -= LoadedScene;
        LevelSizeButton.BoardSize -= SetupGame;
    }


    //Sets rows/cols depending on menu button input.
    public void SetupGame(object sender, SetBoardSizeEventArgs args)
    {
        correctSets = 0;
        rows = args.BoardHeight;
        cols = args.BoardWidth;
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

            SetSetsText();
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
            CheckForWin();
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

    private void CheckForWin()
    {
        correctSets++;
        SetSetsText();

        if(correctSets >= amountOfSets)
        {
            //Return to Menu or Reset and Go again
            //
        }

    }

    private void SetSetsText()
    {
        ChangeSetsTextEventArgs args = new ChangeSetsTextEventArgs();
        args.CurrentSets = correctSets.ToString();
        args.AmountOfSets = amountOfSets.ToString();
        ChangeSetsTextEvent(gameObject, args);
    }

}
