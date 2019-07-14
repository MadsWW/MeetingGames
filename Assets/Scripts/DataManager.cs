using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;



// Save/Load should take and return generic type T (include string param) for datapath
// Should be MonoBehaviour so there cant be new DataManager class generated;
// SetData from events, so only specific classes can access methods.
public  class DataManager : MonoBehaviour {

    public XmlDocument AchievementXML;
    public XmlDocument CardInfoXML;

    AchievementInfo _achievementInfo;
    CreateCardInfo _createCardInfo;

    private const string GAMEDATA_FILE_NAME = "/gamedata.dat";
    private const string CARDINFO_FILE_NAME = "/cardinfo.dat";

    private List<Achievement> _achievements = new List<Achievement>();
    public List<Achievement> Achievements { get { return _achievements; } private set { } }

    private List<CardInfo> _cardBackInfo = new List<CardInfo>();
    public List<CardInfo> CardBackInfo { get { return _cardBackInfo; } private set { } }

    private List<CardInfo> _cardFrontInfo = new List<CardInfo>();
    public List<CardInfo> CardFrontInfo { get { return _cardFrontInfo; } private set { } }

    private int _coins;
    public int Coins { get { return _coins; } private set { } }

    #region UNITY_API

    private void OnEnable()
    {
        LoadGame();
        _achievementInfo = FindObjectOfType<AchievementInfo>();
        _createCardInfo = FindObjectOfType<CreateCardInfo>();
        GameManager.SaveGameEvent += SaveGame;
        GameManager.LoadGameEvent += LoadGame;
        AchievementInfo.CreateAchievementsEvent += AddNewAchievement;
        AchievementButton.SetAchievementOnCompletedEvent += OnAchievementCompleted;
        GameManager.OnGameWonEvent += AddReward;
    }

    private void OnDisable()
    {
        SaveGame();
        GameManager.SaveGameEvent -= SaveGame;
        GameManager.LoadGameEvent -= LoadGame;
        AchievementInfo.CreateAchievementsEvent -= AddNewAchievement;
        AchievementButton.SetAchievementOnCompletedEvent -= OnAchievementCompleted;
        GameManager.OnGameWonEvent -= AddReward;
    }

    #endregion //UNITY_API

    #region EVENT_FUNCTIONS

    private void SaveGame()
    {
        AchievementContainer achievementData = new AchievementContainer();
        achievementData.Achievements = _achievements;
        achievementData.Coins = _coins;
        SaveData(achievementData, GAMEDATA_FILE_NAME);

        CardInfoContainer cardInfoData = new CardInfoContainer();
        cardInfoData.CardBackInfo = _cardBackInfo;
        cardInfoData.CardFrontInfo = _cardFrontInfo;
        SaveData(cardInfoData, CARDINFO_FILE_NAME);
    }

    private void LoadGame()
    {
        LoadGameData();
        LoadCardInfoData();
    }

    private void LoadGameData()
    {
        if (LoadGameData(GAMEDATA_FILE_NAME) != null)
        {
            AchievementContainer data = LoadGameData(GAMEDATA_FILE_NAME);
            _coins = data.Coins;
            _achievements = data.Achievements;
        }
        else
        {
            _achievementInfo.CreateAchievement();
            SaveGame();
            LoadGameData();
        }
    }

    private void LoadCardInfoData()
    {
        if (LoadCardInfo() != null)
        {
            CardInfoContainer data = LoadCardInfo();
            _cardBackInfo = data.CardBackInfo;
            _cardFrontInfo = data.CardFrontInfo;
        }
        else
        {
            _createCardInfo.CreatCardBack();
            _createCardInfo.CreateCardFront();
            SaveGame();
            LoadCardInfo();
        }
    }

    private void AddNewAchievement(CreateAchievementEventArgs args)
    {
        _achievements.Add(args.achievement);
    }

    private void OnAchievementCompleted(SetAchievementOnCompletedEventArgs e)
    {
        _achievements[e.achievementNumber] = e.achievement;
    }

    private void AddReward(PayOutOnCompletedEventArgs args)
    {
        _coins += args.Reward;
    }

    #endregion //EVENT_FUNCTIONS

    #region SAVE_LOAD_FUNCTIONS

    private void SaveData<T>(T data, string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        FileStream filestream = File.Create(Application.persistentDataPath + fileName);

        serializer.Serialize(filestream, data);
        filestream.Close();

    }

    private AchievementContainer LoadGameData(string fileName)
    {
        if(File.Exists(Application.persistentDataPath + fileName))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AchievementContainer));
            FileStream filestream = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
            AchievementContainer data = serializer.Deserialize(filestream) as AchievementContainer;
            filestream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

    private void SaveCardInfo(CardInfoContainer data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(CardInfoContainer));
        FileStream filestream = File.Create(Application.persistentDataPath + "/cardinfo.dat"); // replace hardcoded file to string.

        serializer.Serialize(filestream, data);
        filestream.Close();

    }

    private CardInfoContainer LoadCardInfo()
    {
        if (File.Exists(Application.persistentDataPath + "/cardinfo.dat"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CardInfoContainer));
            FileStream filestream = File.Open(Application.persistentDataPath + "/cardinfo.dat", FileMode.Open);
            CardInfoContainer data = serializer.Deserialize(filestream) as CardInfoContainer;
            filestream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }

    #endregion //SAVE_LOAD_FUNCTIONS
}
