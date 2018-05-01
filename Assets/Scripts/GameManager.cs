using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public event ChangeSetsTextDelegate ChangeSetsTextEvent;

    public enum GameMode { Relax, Turns, Time, Endless}

    public GameMode gameMode;

    private int rows;
    private int cols;
    private int amountOfSets;
    private int correctSets;

    private int turnLeft = 20;
    private int timeLeft = 100;


    //Dont Destroy Object.
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    //Add to events
    private void OnEnable ()
    {
        LevelSizeButton.BoardSize += SetupGame;
        SceneManager.sceneLoaded += LoadedScene;
        CardBehaviour.CheckCard += CheckCorrectCall;
	}

    //Remove from events
    private void OnDisable()
    {
        LevelSizeButton.BoardSize -= SetupGame;
        SceneManager.sceneLoaded -= LoadedScene;
        CardBehaviour.CheckCard -= CheckCorrectCall;
    }

    //Sets rows/cols depending on menu button input.
    private void SetupGame(object sender, SetBoardSizeEventArgs args)
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
        bool correctScene = scene.name == "Memory";
        bool scriptAvailable = FindObjectOfType<DeckBuilder>();

        if (correctScene && scriptAvailable)
        {
            DeckBuilder buildDeck = FindObjectOfType<DeckBuilder>();

            buildDeck.GetSpriteSets(amountOfSets);
            buildDeck.PlaceCards(rows, cols);

            SetSetsText();
        }

        switch (gameMode)
        {
            case GameMode.Time:
                InvokeRepeating("TimeGameMode", 1f, 1f);
                break;
            case GameMode.Endless:
                InvokeRepeating("EndlessGameMode", 1f, 1f);
                break;
            default:
                throw new InvalidEnumArgumentException("A non existing game mode has been chosen");
        }
    }

    //Check if the two selectedCards are equal to eachother and resets the selected cards afterwards.
    private void CheckCorrectCall(object sender, CheckCardEventArgs e)
    {
        bool sameCards = e.CardOne.spriteRen.sprite == e.CardTwo.spriteRen.sprite;

        if (sameCards)
        {
            ResetSelectedCards();
            CheckForWin();
        }
        else
        {
            CardBehaviour.selectOne.ResetPosition();
            CardBehaviour.selectTwo.ResetPosition();
            ResetSelectedCards();
        }
    }

    //Reset selectedCards to null and can selectcards again.
    private void ResetSelectedCards()
    {
        CardBehaviour.canSelectCards = true;
        CardBehaviour.selectOne = null;
        CardBehaviour.selectTwo = null;
    }

    //Checks if all sets have been completed
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

    //Sends event with text from amount of sets completed.
    private void SetSetsText()
    {
        ChangeSetsTextEventArgs args = new ChangeSetsTextEventArgs();
        args.CurrentSets = correctSets.ToString();
        args.AmountOfSets = amountOfSets.ToString();
        ChangeSetsTextEvent(gameObject, args);
    }


    private void DetermineGameMode()
    {
        switch (gameMode)
        {
            case GameMode.Relax:
                break;
            case GameMode.Turns:
                break;
            case GameMode.Time:
                break;
            case GameMode.Endless:
                break;
            default:
                break;
        }
    }

    private void RelaxGameMode()
    {

    }

    private void TurnGameMode()
    {

    }

    private void TimeGameMode()
    {
        timeLeft--;

        if(timeLeft <= 0)
        {
            //LoseCondition
            CancelInvoke("TimeGameMode");
        }
    }

    private void EndlessGameMode()
    {
        timeLeft--;

        if (timeLeft <= 0)
        {
            //LoseCondition
            CancelInvoke("EndlessGameMode");
        }
    }
}
