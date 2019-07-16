using System;
using System.ComponentModel;
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

    public static event SaveGameDelegate SaveGameEvent;
    public static event LoadGameDelegate LoadGameEvent;

    public static event PayOutOnCompletedDelegate OnGameWonEvent;
    public static event AchievedAmountForAchievementDelegate AmountAchievedEvent;


 
    private GameMode gameMode;

    private static GameManager _gameManager;

    //Will be set from menu buttons if necessairy
    private int rows = 6;
    private int cols = 4;
    private int amountOfSets = 12;
    private int correctSets;

    //Card Front Sprites
    [SerializeField] private Sprite[] _cardDeck;
    //Card Back Sprite
    [SerializeField] private Sprite _cardBack;

    //Start values for gamemodes
    private int turnLeft = 30;
    private int timeLeft = 60;
    private int timeSurvived = 0;

    //Classes needed for methods
    private DeckBuilder _deckBuilder;
    private LevelManager _levelManager;
    private UIController _uiController;
    private DataManager _dataManager;


    private void Awake()
    {
        Singleton();

        _levelManager = FindObjectOfType<LevelManager>();

    }

    private void Singleton()
    {
        if (_gameManager == null)
        {
            _gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    #region EVENT_SUBSCRIPTION

    private void OnEnable ()
    {
        _dataManager = FindObjectOfType<DataManager>();
        _dataManager.ChangeCoinTextEvent += SetCoinText;
        LevelSizeButton.BoardSize += SetupGame;
        SceneManager.sceneLoaded += LoadedScene;
        CardBehaviour.CheckCard += CheckCorrectCall;
	}

    private void OnDisable()
    {
        if (_dataManager) { _dataManager.ChangeCoinTextEvent -= SetCoinText; }
        LevelSizeButton.BoardSize -= SetupGame;
        SceneManager.sceneLoaded -= LoadedScene;
        CardBehaviour.CheckCard -= CheckCorrectCall;
    }

    #endregion EVENT_SUBSCRIPTION


    public void SetGameMode(GameMode mode)
    {
        gameMode = mode;
    }

    #region BOARD_SIZE_EVENT_METHODS
    //Sets rows/cols depending on menu button input.
    private void SetupGame(SetBoardSizeEventArgs args)
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

    #region SCENE_LOADED_METHOD
    //Passes amount of rows/cols/sets to DeckBuilder when the memory scene is loaded.
    private void LoadedScene(Scene scene, LoadSceneMode mode)
    {
        _dataManager = FindObjectOfType<DataManager>();

        bool memoryScene = scene.name == "Memory";
        bool scriptAvailable = FindObjectOfType<DeckBuilder>();

        if (memoryScene && scriptAvailable)
        {
            BuildMemoryDeck();
            GameModeSetup();
        }
        else
        {
            ChangeCoinTextEventArgs args = new ChangeCoinTextEventArgs();
            args.Coins = _dataManager.Coins;
            SetCoinText(args);

            
        }
    }

    private void GameModeSetup()
    {
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

    private void BuildMemoryDeck()
    {
        _deckBuilder = FindObjectOfType<DeckBuilder>();
        _deckBuilder.GetSpriteSets(amountOfSets);
        _deckBuilder.PlaceCards(rows, cols);
    }

    #endregion SCENE_LOADED_METHOD

    #region CHECKCARD_EVENT_METHODS

    //Check if the two selectedCards are equal to eachother and resets the selected cards afterwards.
    private void CheckCorrectCall(CheckCardEventArgs e)
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

    #endregion CHECKCARD_EVENT_METHODS

    #region SET_TEXT_FUNCTIONS

    private void SetSetsText()
    {
        ChangeSetsTextEventArgs args = new ChangeSetsTextEventArgs();
        args.CurrentSets = correctSets.ToString();
        args.AmountOfSets = amountOfSets.ToString();
        ChangeSetsLeft(args);
    }

    private void SetTurnText(int turn)
    {
        ChangeTurnTextEventArgs args = new ChangeTurnTextEventArgs();
        args.TurnLeft = turn;
        ChangeTurnLeft(args);
    }

    private void SetTimeText(int time)
    {
        ChangeTimeTextEventArgs args = new ChangeTimeTextEventArgs();
        args.TimeLeft = time;
        ChangeTimeLeft(args);
    }

    private void SetCoinText(ChangeCoinTextEventArgs args)
    {
        if (_uiController = FindObjectOfType<UIController>())
        {
            _uiController = FindObjectOfType<UIController>();
            _uiController.CoinText.text = args.Coins.ToString() + " Coins";
        }
    }

    #endregion SET_TEXT_FUNCTIONS

    #region SET_HIGHEST_ACHIEVED_METHODS

    private int SetHighestTurnLeft()
    {
        if(turnLeft > _dataManager.Achievements[2].AmountAchieved)
        {
            return turnLeft;
        }
        else
        {
            return _dataManager.Achievements[2].AmountAchieved;
        }
    }

    private int SetHighestTimeLeft()
    {
        if(timeLeft > _dataManager.Achievements[3].AmountAchieved)
        {
            return timeLeft;
        }
        else
        {
            return _dataManager.Achievements[3].AmountAchieved;
        }
    }

    private int SetHighestTimeSurvived()
    {
        if(timeSurvived > _dataManager.Achievements[4].AmountAchieved)
        {
            return timeSurvived;
        }
        else
        {
            return _dataManager.Achievements[4].AmountAchieved;
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
            switch (gameMode)
            {
                case GameMode.Relax:
                    OnGameWon(10); //TODO Change to settings instead of hardcoded
                    AmountAchievedForAchievement(0, _dataManager.Achievements[0].AmountAchieved += 1);
                    Win();
                    break;
                case GameMode.Turns:
                    OnGameWon(25);
                    AmountAchievedForAchievement(1, _dataManager.Achievements[1].AmountAchieved += 1);
                    AmountAchievedForAchievement(2, SetHighestTurnLeft());
                    Win();
                    break;
                case GameMode.Time:
                    OnGameWon(25);
                    AmountAchievedForAchievement(3, SetHighestTimeLeft());
                    Win();
                    break;
                case GameMode.Endless:
                    timeSurvived += timeLeft;
                    OnGameWon(25);
                    AmountAchievedForAchievement(4, SetHighestTimeSurvived());
                    ResetBoard();
                    break;
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
            Lose();
        }
    }

    //better if this would be places in DeckBuilder class
    private void ResetBoard()
    {
        correctSets = 0;
        SetSetsText();
        timeLeft += 30;
        _deckBuilder.ClearBoard();
        _deckBuilder.GetSpriteSets(amountOfSets);
        _deckBuilder.PlaceCards(rows, cols);
    }

    #endregion CHECK_WHATISLEFT_PERGAMEMODE_METHODS

    #region WIN_LOSE_METHODS

    //TODO ui pop when game is won.
    private void Win()
    {
        SaveGameEvent();
        CancelInvoke("CheckTimeLeft");
        _levelManager.LoadScene("Menu");
        //Whatever happens the player wins the round.
    }

    //TODO ui pop when game is lost.
    private void Lose()
    {
        SaveGameEvent();
        CancelInvoke("CheckTimeLeft");
        //Whatever happens when the player loses the round.
        _levelManager.LoadScene("Menu");
    }

    private void OnGameWon(int reward)
    {
        PayOutOnCompletedEventArgs args = new PayOutOnCompletedEventArgs();
        args.Reward = reward;
        OnGameWonEvent(args);
    }

    private void AmountAchievedForAchievement(int achievementNumber, int amountAchieved)
    {
        AchievedAmountForAchievementEventArgs args = new AchievedAmountForAchievementEventArgs();
        args.AchievementNumber = achievementNumber;
        args.AmountAchieved = amountAchieved;
        AmountAchievedEvent(args);
    }

    #endregion


}
