using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum ShipLevel
    {
        CargoHold,
        MiddleDeck,
        MainDeck
    }

    public ShipLevel currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = ShipLevel.CargoHold;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public ShipLevel GetCurrentLevel()
    {
        return currentLevel;
    }

    public SpriteRenderer GetLevelSprite()
    {
        SpriteRenderer currentLevelSprite = null;
        
        switch (currentLevel)
        {
            case ShipLevel.CargoHold:
                currentLevelSprite = GameObject.Find("Cargo Hold").GetComponent<SpriteRenderer>();
                break;
            case ShipLevel.MiddleDeck:
                currentLevelSprite = GameObject.Find("Middle Deck").GetComponent<SpriteRenderer>();
                break;
            case ShipLevel.MainDeck:
                currentLevelSprite = GameObject.Find("Main Deck").GetComponent<SpriteRenderer>();
                break;
        }

        return currentLevelSprite;
            
    }
}
