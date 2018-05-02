using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    public Text SetsText;
    public Text TurnText;
    public Text TimeText;

    GameManager gManager;

    private void Awake()
    {
        gManager = FindObjectOfType<GameManager>();
    }


    private void OnEnable()
    {
        gManager.ChangeSetsLeft += SetSetsText;
        gManager.ChangeTurnLeft += SetTurnText;
        gManager.ChangeTimeLeft += SetTimeText;
    }

    private void OnDisable()
    {
        gManager.ChangeSetsLeft -= SetSetsText;
        gManager.ChangeTurnLeft -= SetTurnText;
        gManager.ChangeTimeLeft -= SetTimeText;
    }

    private void SetSetsText(object sender, ChangeSetsTextEventArgs e)
    {
        SetsText.text = string.Format("{0} / {1} Sets", e.CurrentSets, e.AmountOfSets);
    }

    private void SetTurnText(object sender, ChangeTurnTextEventArgs e)
    {
        if (e.TurnLeft == "0")
        {
            e.TurnLeft = "--";
        }

        TurnText.text = string.Format("{0} Turns Left", e.TurnLeft);

    }

    private void SetTimeText(object sender, ChangeTimeTextEventArgs e)
    {
        if (e.TimeLeft == "0")
        {
            e.TimeLeft = "--";
        }

        TimeText.text = string.Format("{0} Sec Left", e.TimeLeft);
    }
}
