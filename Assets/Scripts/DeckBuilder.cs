using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuilder : MonoBehaviour {

    public GameObject _memoryCard;

    public Sprite[] _spriteCollection;
    public Sprite _cardBack;

    List<Sprite> _sprites = new List<Sprite>();
    List<int> recurredNumbers = new List<int>();
    List<GameObject> _cards = new List<GameObject>();

    public float size = 1.5f;


    public void GetSpriteSets(int amountOfSets)
    {
        NonRecurringRandomNumbers(amountOfSets, _spriteCollection.Length);

        for(int i = 0; i < amountOfSets; i++)
        {
            int randomNumber = recurredNumbers[i];
            _sprites.Add(_spriteCollection[randomNumber]);
            _sprites.Add(_spriteCollection[randomNumber]);
        }
        print("Amount of Sprites: " + _sprites.Count);
        recurredNumbers.Clear();
    }

    //Places card on position depending on amount of rows/cols.
    public void PlaceCards(int rows, int cols)
    {
        float xpos = 0;
        float ypos = 0;
        int cardNumber = 0;

        NonRecurringRandomNumbers(_sprites.Count, _sprites.Count);

        for(int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                BuildCard(xpos, ypos, cardNumber);
                xpos += size ;
                cardNumber++;
            }
            ypos -= size; ;
            xpos = 0;
        }

        recurredNumbers.Clear();
    }

    //Sets Position and Sprite of the MemoryCard.
    private void BuildCard(float xpos, float ypos, int cardNumber)
    {
        GameObject card = Instantiate(_memoryCard, new Vector3(xpos, ypos, 0), Quaternion.identity);
        CardBehaviour cardinfo = card.GetComponent<CardBehaviour>();
        int spriteNumber = recurredNumbers[cardNumber];
        Sprite sprite = _sprites[spriteNumber];
        cardinfo.SetCardInfo(sprite);
        _cards.Add(card);
    }

    public void ClearBoard()
    {
        foreach(GameObject go in _cards)
        {
            Destroy(go); // Instead of destroying replace the picture of them cards instead.
        }
    }


    //!! Maybe assign value between 0/1 to all indexy of original list and them sort them for randomness.
    //!! For a large List this will take longer and longer.
    private void NonRecurringRandomNumbers(int amountRandomNr, int highestNumber) // return list of int to avoid emptying class variable list and can be made sep. class
    {
        int randomNumber;

        for (int i = 0; i < amountRandomNr;)
        {
            randomNumber = Random.Range(0, highestNumber);

            if(CheckForRecurredNumber(randomNumber)){
                //Debug.Log("Found Recurring Number"); // 16/24 times called which is not really optimal. !! Check remark above.
            }
            else
            {
                recurredNumbers.Add(randomNumber);
                i++;
            }
        }
        print("ReccuredNumber Count: " + recurredNumbers.Count);
    }


    // Can make separate class if a List<int> is added as parameter same for methode above.
    private bool CheckForRecurredNumber(int nr)
    {
        foreach(int i in recurredNumbers)
        {
            if(recurredNumbers == null)
            {
                return false;
            }
            else if(i == nr)
            {
                return true;
            }
        }
        return false;
    }

}
