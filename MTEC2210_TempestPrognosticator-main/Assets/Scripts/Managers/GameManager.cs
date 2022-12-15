using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    private static GameManager instance;
    public GameObject lastCheckPoint;
    public PlayerHealthStamina playerHealthStamina;
    public LevelChangeManager levelChangeManager;


    public enum ShipLevel
    {
        CargoHold,
        MiddleDeck,
        MainDeck
    }

    public ShipLevel currentLevel;

    
    void Awake() 
    {
        currentLevel = ShipLevel.CargoHold;
        lastCheckPoint = GameObject.Find("Checkpoint 1");
        
    }
    // Start is called before the first frame update
    void Start()
    {
        playerHealthStamina = GameObject.Find("Player").GetComponent<PlayerHealthStamina>();
        levelChangeManager = GetComponent<LevelChangeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // if player dies, bring player back to last checkpoint
        if (playerHealthStamina.alive == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //levelChangeManager.LevelSwitch(playerHealthStamina.gameObject, lastCheckPoint.name, currentLevel);
        }
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
