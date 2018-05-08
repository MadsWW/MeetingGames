using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeButton : MonoBehaviour {

    public GameMode buttonGameMode;

    private GameManager gManager;
    private Button button;

    private void Awake()
    {
        gManager = FindObjectOfType<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        gManager.SetGameMode(buttonGameMode);
    }
}
