using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCardInfo : MonoBehaviour {

    private CardBack[] CardBackInfo = new CardBack[5];
    public Sprite[] CardBacksSprite;
    public GameObject CardBackPrefab;

    private bool InitCardPrefab = false;
	// Use this for initialization
	void Start ()
    {
        CreatCardBack();
    }

    private void CreatCardBack()
    {
        CardBackInfo[0] = new CardBack(100, CardBacksSprite[0]);
        CardBackInfo[1] = new CardBack(200, CardBacksSprite[1]);
        CardBackInfo[2] = new CardBack(400, CardBacksSprite[2]);
        CardBackInfo[3] = new CardBack(400, CardBacksSprite[3]);
        CardBackInfo[4] = new CardBack(1000, CardBacksSprite[4]);
    }

    public void InitCardBackPrefab( RectTransform parent)
    {
        int i = 0;

        foreach(RectTransform go in parent.GetComponentsInChildren<RectTransform>())
        {
            if (go.gameObject.GetComponent<CardBackButton>())
            {
                CardBackButton cbb = go.GetComponent<CardBackButton>();
                cbb.SetInfo(CardBackInfo[i].Price, CardBackInfo[i].CardSprite);
                i++;
            }
        }
    }

}
