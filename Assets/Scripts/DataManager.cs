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

    public event ChangeCoinTextDelegate ChangeCoinTextEvent;

    [SerializeField]private TextAsset AchievementXML;
    [SerializeField]private TextAsset CardInfoXML;

    AchievementInfo _achievementInfo;
    CreateCardInfo _createCardInfo;

    private const string GAMEDATA_FILE_NAME = "/gamedata.xml";
    private const string CARDINFO_FILE_NAME = "/cardinfo.xml";

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
        AchievementButton.SetAchievementOnCompletedEvent += OnAchievementCompleted;
        GameManager.OnGameWonEvent += AddReward;
        CardInfoButton.PurchaseItemEvent += PurchaseMade;
    }

    private void OnDisable()
    {
        SaveGame();
        GameManager.SaveGameEvent -= SaveGame;
        GameManager.LoadGameEvent -= LoadGame;
        AchievementButton.SetAchievementOnCompletedEvent -= OnAchievementCompleted;
        GameManager.OnGameWonEvent -= AddReward;
        CardInfoButton.PurchaseItemEvent -= PurchaseMade;
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
        TriggerSetCoinTextEvent();
    }

    private void LoadGameData()
    {
        AchievementContainer data = GetGameData(GAMEDATA_FILE_NAME);
        _coins = data.Coins;
        _achievements = data.Achievements;
    }

    private void LoadCardInfoData()
    {
        CardInfoContainer data = GetCardInfo(CARDINFO_FILE_NAME);
        _cardBackInfo = data.CardBackInfo;
        _cardFrontInfo = data.CardFrontInfo;
    }

    private void OnAchievementCompleted(SetAchievementOnCompletedEventArgs e)
    {
        _achievements[e.achievementNumber] = e.achievement;
    }

    private void AddReward(PayOutOnCompletedEventArgs args)
    {
        _coins += args.Reward;
        TriggerSetCoinTextEvent();
    }

    private void PurchaseMade(OnPurchaseCompletedEventArgs args)
    {
        _coins -= args.Cost;
        TriggerSetCoinTextEvent();
    }

    private void TriggerSetCoinTextEvent()
    {
        ChangeCoinTextEventArgs args = new ChangeCoinTextEventArgs();
        args.Coins = _coins;
        ChangeCoinTextEvent(args);
    }

    #endregion //EVENT_FUNCTIONS

    #region SAVE_LOAD_XML_FUNCTIONS

    private void SaveData<T>(T data, string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        FileStream filestream = File.Create(Application.persistentDataPath + fileName);

        serializer.Serialize(filestream, data);
        filestream.Close();

    }

    private AchievementContainer GetGameData(string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AchievementContainer));
        AchievementContainer data;

        if (File.Exists(Application.persistentDataPath + fileName))
        {
            FileStream filestream = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
            data = serializer.Deserialize(filestream) as AchievementContainer;
            filestream.Close();
            return data;
        }
        else
        {
            StringReader reader = new StringReader(AchievementXML.text);
            data = serializer.Deserialize(reader) as AchievementContainer;
            return data;
        }
    }


    private CardInfoContainer GetCardInfo(string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(CardInfoContainer));
        CardInfoContainer data;

        if (File.Exists(Application.persistentDataPath + fileName))
        {
            FileStream filestream = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
            data = serializer.Deserialize(filestream) as CardInfoContainer;
            filestream.Close();
            return data;
        }
        else
        {
            StringReader reader = new StringReader(CardInfoXML.text);
            data = serializer.Deserialize(reader) as CardInfoContainer;
            return data;
        }
    }

    #endregion //SAVE_LOAD_FUNCTIONS
}
