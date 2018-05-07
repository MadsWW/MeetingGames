using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public RectTransform[] UIPanels;

    public void EnablePanel(RectTransform go)
    {
        foreach(RectTransform gObject in UIPanels)
        {
            if(gObject == go)
            {
                gObject.gameObject.SetActive(true);
            }
            else
            {
                gObject.gameObject.SetActive(false);
            }
        }
    }
}
