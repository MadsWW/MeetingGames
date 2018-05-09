using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameMode { Relax, Turns, Time, Endless }

public class GameManager : MonoBehaviour {

    //!! Rework To Do - Check Line:232,176, 236
    //!! 176: All CheckTurn/Time/Sets Method could be one separate function with 2 int or float params saves space.
    //!! 232: Place resetboard in DeckBuilder Class
    //!! 236: Add method for endless gamemode to add less then the more rounds the player won.


    public event ChangeSetsTextDelegate ChangeSetsLeft;
    public event ChangeTurnTextDelegate ChangeTurnLeft;
    public event ChangeTimeTextDelegate ChangeTimeLeft;

    //Can be private
 
    public GameMode gameMode;

    private static GameManager gManager = null;

    //Will be set from menu buttons if necessairy
    private int rows = 2;
    private int cols = 1;
    private int amountOfSets = 1;
    private int correctSets;

    //Card Front Sprites
    public Sprite[] CardDeck;
    //Card Back Sprite
    public Sprite CardBack;

    private int turnLeft = 30;
    private int timeLeft = 100;
    private int timeSurvived = 0;

    DeckBuilder buildDeck;
    AchievementInfo aInfo;
    LevelManager lManager;

    //More understandable if divided into 5 seperate integers.
    private int[] amountAchievedPerGameMode = new int[] { 0, 0, 0, 0, 0 };


    //Dont Destroy Object.
    private void Awake()
    {
        if (gManager == null)
        {
            gManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        aInfo = FindObjectOfType<AchievementInfo>();
        lManager = FindObjectOfType<LevelManager>();

    }

    //Sets the GameMode from menu button - OnClick() does not support enum/thats why its integer.
    public void SetGameMode(GameMode mode)
    {
        gameMode = mode;
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

    #region BOARD_SIZE_EVENT_METHODS
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

    #endregion BOARD_SIZE_EVENT_METHODS

    //Passes amount of rows/cols/sets to DeckBuilder when the memory scene is loaded.
    private void LoadedScene(Scene scene, LoadSceneMode mode)
    {
        bool memoryScene = scene.name == "Memory";
        bool menuScene = scene.name == "Menu";
        bool scriptAvailable = FindObjectOfType<DeckBuilder>();

        if (memoryScene && scriptAvailable)
        {
            buildDeck = FindObjectOfType<DeckBuilder>();

            buildDeck.GetSpriteSets(amountOfSets);
            buildDeck.PlaceCards(rows, cols);

            correctSets = 0;
            SetSetsText();
            SetTurnText(0);
            SetTimeText(0);

            switch (gameMode)
            {
                case GameMode.Relax:
                    break;
                case GameMode.Turns:
                    turnLeft = 30;
                    SetTurnText(turnLeft);
                    break;
                case GameMode.Time:
                    timeLeft = 100;
                    SetTimeText(timeLeft);
                    InvokeRepeating("CheckTimeLeft", 1f, 1f);
                    break;
                case GameMode.Endless:
                    timeLeft = 100;
                    SetTimeText(timeLeft);
                    InvokeRepeating("CheckTimeLeft", 1f, 1f);
                    break;
                default:
                    throw new InvalidEnumArgumentException("An invalid game mode has been chosen");
            }
        }

        if (menuScene)
        {
            //Load data from achievementmanager to achievedamount var.
            //Set Data from achievedamount to achievementmanager
        }
    }

    #region CHECK_SELECTEDCARDCORRECT_METHODS

    //Check if the two selectedCards are equal to eachother and resets the selected cards afterwards.
    private void CheckCorrectCall(object sender, CheckCardEventArgs e)
    {
        if (gameMode == GameMode.Turns)
        {
            CheckTurnLeft();
        }
        
        bool sameCards = e.CardOne.spriteRen.sprite == e.CardTwo.spriteRen.sprite;
        if (sameCards)
        {
            ResetSelectedCards();
            CheckSetsLeft();
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

    #endregion CHECK_SELECTEDCARDCORRECT_METHODS

    #region SET_TEXT_FUNCTIONS

    //Sends event with text from amount of sets completed.
    private void SetSetsText()
    {
        ChangeSetsTextEventArgs args = new ChangeSetsTextEventArgs();
        args.CurrentSets = correctSets.ToString();
        args.AmountOfSets = amountOfSets.ToString();
        ChangeSetsLeft(gameObject, args);
    }

    //Sends event with amount of turns left.
    private void SetTurnText(int turn)
    {
        ChangeTurnTextEventArgs args = new ChangeTurnTextEventArgs();
        args.TurnLeft = turn;
        ChangeTurnLeft(gameObject, args);
    }

    //Sends event with amount of time left.
    private void SetTimeText(int time)
    {
        ChangeTimeTextEventArgs args = new ChangeTimeTextEventArgs();
        args.TimeLeft = time;
        ChangeTimeLeft(gameObject, args);
    }

    #endregion SET_TEXT_FUNCTIONS

    #region SET_HIGHEST_ACHIEVED_METHODS

    private int SetHighestTurnLeft()
    {
        if(turnLeft > amountAchievedPerGameMode[2])
        {
            return turnLeft;
        }
        else
        {
            return amountAchievedPerGameMode[2];
        }
    }

    private int SetHighestTimeLeft()
    {
        if(timeLeft > amountAchievedPerGameMode[3])
        {
            return timeLeft;
        }
        else
        {
            return amountAchievedPerGameMode[3];
        }
    }

    private int SetHighestTimeSurvived()
    {
        if(timeSurvived > amountAchievedPerGameMode[4])
        {
            return timeSurvived;
        }
        else
        {
            return amountAchievedPerGameMode[4];
        }
    }

    #endregion SET_HIGHEST_ACHIEVED_METHODS

    #region CHECK_WHATISLEFT_PERGAMEMODE_METHODS
    //Checks if all sets have been completed and passes that to event.
    private void CheckSetsLeft()
    {
        correctSets++;
        SetSetsText();

        if (correctSets >= amountOfSets)
        {
            if (gameMode == GameMode.Endless)
            {
                timeSurvived += timeLeft;
                ResetBoard();
            }
            else
            {
                Win();

                switch (gameMode)
                {
                    case GameMode.Relax:
                        amountAchievedPerGameMode[0]++;
                        break;
                    case GameMode.Turns:
                        amountAchievedPerGameMode[1]++;
                        amountAchievedPerGameMode[2] = SetHighestTurnLeft();
                        break;
                    case GameMode.Time:
                        amountAchievedPerGameMode[3] = SetHighestTimeLeft();
                        break;
                }
            }
        }
    }

    //Checks how much turns are left and passes that to event.
    private void CheckTurnLeft()
    {
        turnLeft--;
        SetTurnText(turnLeft);

        if(turnLeft <= 0)
        {
            Lose();
        }
    }

    //Checks how much time is left and passes that to event.
    private void CheckTimeLeft()
    {


        timeLeft--;
        SetTimeText(timeLeft);

        if (timeLeft <= 0)
        {
            if (gameMode == GameMode.Endless)
            {
                amountAchievedPerGameMode[4] = SetHighestTimeSurvived();
            }

            CancelInvoke("CheckTimeLeft");
            Lose();
        }
    }

    //better if this would be places in DeckBuilder class
    private void ResetBoard()
    {
        correctSets = 0;
        SetSetsText();
        timeLeft += 30;
        buildDeck.ClearBoard();
        buildDeck.GetSpriteSets(amountOfSets);
        buildDeck.PlaceCards(rows, cols);
    }

    #endregion CHECK_WHATISLEFT_PERGAMEMODE_METHODS


    //ui pop when game is won.
    private void Win()
    {
        //Whatever happens the player wins the round.
        lManager.LoadScene("Menu");
        CancelInvoke("CheckTimeLeft");
    }

    //ui pop when game is lost.
    private void Lose()
    {
        //Whatever happens when the player loses the round.
        lManager.LoadScene("Menu");
    }
}
