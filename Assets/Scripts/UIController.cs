using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private GameObject[] UIPanels;

    private void Start()
    {
        UIPanels = GetComponentsInChildren<GameObject>();
    }
    public void EnablePanel(Panel panel)
    {
        foreach(GameObject gObject in UIPanels)
        {
            gObject.SetActive(false);
        }

        go.SetActive(true);
    }
}
