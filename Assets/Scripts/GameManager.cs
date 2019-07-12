﻿using System;
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
 
    private GameMode gameMode;

    private static GameManager gManager;

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
    private AchievementInfo _achievementInfo;

    private List<Achievement> _achievements = new List<Achievement>();
    public List<Achievement> Achievements { get { return _achievements; } }

    private int coins = 100;
    public int Coins { get { return coins; }}


    private void Awake()
    {
        Singleton();

        _levelManager = FindObjectOfType<LevelManager>();

    }

    #region EVENT_SUBSCRIPTION

    private void OnEnable ()
    {
        _achievementInfo = FindObjectOfType<AchievementInfo>();
        LevelSizeButton.BoardSize += SetupGame;
        SceneManager.sceneLoaded += LoadedScene;
        AchievementButton.SetAchievementOnCompletedEvent += OnAchievementCompleted;
        AchievementButton.PayOutOnCompletedEvent += AddReward;
        CardBehaviour.CheckCard += CheckCorrectCall;
        _achievementInfo.CreateAchievementsEvent += LoadAchievements;
	}

    private void OnDisable()
    {
        LevelSizeButton.BoardSize -= SetupGame;
        SceneManager.sceneLoaded -= LoadedScene;
        AchievementButton.SetAchievementOnCompletedEvent -= OnAchievementCompleted;
        AchievementButton.PayOutOnCompletedEvent -= AddReward;
        CardBehaviour.CheckCard -= CheckCorrectCall;
        if (_achievementInfo) { _achievementInfo.CreateAchievementsEvent -= LoadAchievements; }
    }

    #endregion EVENT_SUBSCRIPTION

    private void Singleton()
    {
        if (gManager == null)
        {
            gManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadAchievements(CreateAchievementEventArgs args)
    {
        _achievements.Add(args.achievement);
    }

    public void SetGameMode(GameMode mode)
    {
        gameMode = mode;
    }



    public void SetCoins(GameObject go, int loadedcoins)
    {
        if (go.GetComponent<AchievementInfo>())
        {
            coins = loadedcoins;
        }
    }
    public bool CheckEnoughCoins(int cost)
    {
        if(cost >= 0 && cost <= coins)
        {
            coins -= cost;
            SetCoinText();
            return true;
        }
        else
        {
            return false;
            // Could show message: not enough coins to buy.
        }
    }

    private void AddReward(PayOutOnCompletedEventArgs args)
    {
        coins += args.Reward;
        SetCoinText();
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
        bool memoryScene = scene.name == "Memory";
        bool scriptAvailable = FindObjectOfType<DeckBuilder>();

        if (memoryScene && scriptAvailable)
        {
            BuildMemoryDeck();
            GameModeSetup();
        }
        else
        {

            LoadData();
            SetCoinText();
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

    #region SAVE_LOAD_METHODS

    private void LoadData()
    {
        if (DataManager.LoadData() != null)
        {
            AchievementContainer data = DataManager.LoadData();
            coins = data.Coins;
            _achievements = data.Achievements;
        }
        else
        {
            _achievementInfo.CreateAchievement();
            SaveData();
            LoadData();
        }
    }

    private void SaveData()
    {
        AchievementContainer data = new AchievementContainer();
        data.Achievements = _achievements;
        data.Coins = coins;
        DataManager.SaveData(data);
    }

    #endregion SAVE_LOAD_METHODS

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

    //Sends event with text from amount of sets completed.
    private void SetSetsText()
    {
        ChangeSetsTextEventArgs args = new ChangeSetsTextEventArgs();
        args.CurrentSets = correctSets.ToString();
        args.AmountOfSets = amountOfSets.ToString();
        ChangeSetsLeft(args);
    }

    //Sends event with amount of turns left.
    private void SetTurnText(int turn)
    {
        ChangeTurnTextEventArgs args = new ChangeTurnTextEventArgs();
        args.TurnLeft = turn;
        ChangeTurnLeft(args);
    }

    //Sends event with amount of time left.
    private void SetTimeText(int time)
    {
        ChangeTimeTextEventArgs args = new ChangeTimeTextEventArgs();
        args.TimeLeft = time;
        ChangeTimeLeft(args);
    }

    private void SetCoinText()
    {
        _uiController = FindObjectOfType<UIController>();
        _uiController.CoinText.text = coins.ToString() + " Coins";
    }

    #endregion SET_TEXT_FUNCTIONS

    #region SET_HIGHEST_ACHIEVED_METHODS

    private int SetHighestTurnLeft()
    {
        if(turnLeft > _achievements[2].AmountAchieved)
        {
            return turnLeft;
        }
        else
        {
            return _achievements[2].AmountAchieved;
        }
    }

    private int SetHighestTimeLeft()
    {
        if(timeLeft > _achievements[3].AmountAchieved)
        {
            return timeLeft;
        }
        else
        {
            return _achievements[3].AmountAchieved;
        }
    }

    private int SetHighestTimeSurvived()
    {
        if(timeSurvived > _achievements[4].AmountAchieved)
        {
            return timeSurvived;
        }
        else
        {
            return _achievements[4].AmountAchieved;
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
                switch (gameMode)
                {
                    case GameMode.Relax:
                        _achievements[0].AmountAchieved++;
                        coins += 10;
                        Win();
                        break;
                    case GameMode.Turns:
                        coins += 25;
                        _achievements[1].AmountAchieved++;
                        _achievements[2].AmountAchieved = SetHighestTurnLeft();
                        Win();
                        break;
                    case GameMode.Time:
                        coins += 25;
                        _achievements[3].AmountAchieved = SetHighestTimeLeft();
                        Win();
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
                _achievements[4].AmountAchieved = SetHighestTimeSurvived();
                coins += 25; // get more when more rounds won.
            }

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
        SaveData();
        CancelInvoke("CheckTimeLeft");
        _levelManager.LoadScene("Menu");
        //Whatever happens the player wins the round.
    }

    //TODO ui pop when game is lost.
    private void Lose()
    {
        SaveData();
        CancelInvoke("CheckTimeLeft");
        //Whatever happens when the player loses the round.
        _levelManager.LoadScene("Menu");
    }

    #endregion

    private void OnAchievementCompleted(SetAchievementOnCompletedEventArgs e)
    {
        _achievements[e.achievementNumber] = e.achievement;
    }

}
