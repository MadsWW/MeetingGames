using System;
using System.Collections.Generic;
using UnityEngine;

public class CreateCardInfo : MonoBehaviour {

    private DataManager _dataManager;

    [SerializeField] private GameObject _cardInfoButtonPrefab;

    [SerializeField] private GameObject _cardFrontParent;
    [SerializeField] private GameObject _cardBackParent;

    [SerializeField] private Sprite[] _cardFrontSprite;
    [SerializeField] private Sprite[] _cardBacksSprite;


    private void Awake()
    {
        _dataManager = FindObjectOfType<DataManager>();
    }

    private void SendCardInfo()
    {
        for( int i = 0; i < _dataManager.CardBackInfo.Count; i++)
        {
            GameObject go = Instantiate(_cardInfoButtonPrefab, _cardBackParent.transform);
            CardInfoButton cib = go.GetComponent<CardInfoButton>();
            cib.SetInfo(_dataManager.CardBackInfo[i], _cardBacksSprite[i]);
        }

        for (int i = 0; i < _dataManager.CardFrontInfo.Count; i++)
        {
            GameObject go = Instantiate(_cardInfoButtonPrefab, _cardFrontParent.transform);
            CardInfoButton cib = go.GetComponent<CardInfoButton>();
            cib.SetInfo(_dataManager.CardFrontInfo[i], _cardFrontSprite[i]);
        }
    }
}
