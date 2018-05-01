using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    public Text SetsText;

    GameManager gManager;

    private void Awake()
    {
        gManager = FindObjectOfType<GameManager>();
    }


    private void OnEnable()
    {
        gManager.ChangeSetsTextEvent += SetSetsText;
    }

    private void OnDisable()
    {
        gManager.ChangeSetsTextEvent -= SetSetsText;
    }

    private void SetSetsText(object sender, ChangeSetsTextEventArgs e)
    {
        SetsText.text = string.Format("{0} / {1} Sets", e.CurrentSets, e.AmountOfSets);
    }
}
