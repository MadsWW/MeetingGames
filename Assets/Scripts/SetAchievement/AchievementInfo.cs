using System;
using UnityEngine;

public class AchievementInfo : MonoBehaviour
{
    [SerializeField] private GameObject _achievementPrefab;
    [SerializeField] private GameObject _achievementParent;

    private DataManager _dataManager;


    private void Awake()
    {
        _dataManager = FindObjectOfType<DataManager>();
        InstantiateAchievements();
    }

    private void InstantiateAchievements()
    {
        foreach (Achievement achievement in _dataManager.Achievements)
        {
            GameObject go = Instantiate(_achievementPrefab, _achievementParent.transform);
            AchievementButton ab = go.GetComponent<AchievementButton>();
            ab.SetInfo(achievement);
        }
    }
}
