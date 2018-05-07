using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour {

    private AchievementTracker aTracker;
    private Button button;

    private void Awake()
    {
        aTracker = FindObjectOfType<AchievementTracker>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        aTracker.SetAchievements();
    }
}
