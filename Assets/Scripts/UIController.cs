using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public RectTransform[] UIPanels;
    public Text CoinText;

    private void Awake()
    {
        EnablePanel(UIPanels[0]);
    }

    public void EnablePanel(RectTransform go)
    {
        foreach(RectTransform gObject in UIPanels)
        {
            if(gObject == go)
            {
                foreach(Image im in gObject.GetComponentsInChildren<Image>())
                {
                    im.enabled = true;
                }
                foreach (Text tx in gObject.GetComponentsInChildren<Text>())
                {
                    tx.enabled = true;
                }
            }
            else
            {
                foreach (Image im in gObject.GetComponentsInChildren<Image>())
                {
                    im.enabled = false;
                }
                foreach (Text tx in gObject.GetComponentsInChildren<Text>())
                {
                    tx.enabled = false;
                }
            }
        }
    }
}
