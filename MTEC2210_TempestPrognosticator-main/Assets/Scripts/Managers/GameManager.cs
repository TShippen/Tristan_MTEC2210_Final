using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    private static GameManager instance;
    public GameObject lastCheckPoint;
    public GameObject player;
    public PlayerHealthStamina playerHealthStamina;
    public LevelChangeManager levelChangeManager;
    public EnemyManager enemyManager;


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
        levelChangeManager = GetComponent<LevelChangeManager>();
        playerHealthStamina = player.GetComponent<PlayerHealthStamina>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        playerHealthStamina = player.GetComponent<PlayerHealthStamina>();

    }

    // Update is called once per frame
    void Update()
    {
        // if player dies, restart, bring player back to last checkpoint
        if (playerHealthStamina.alive == false)
        {
            LevelRestart();
        }

        // for debugging purposes only, uncomment below to use
        // if (Input.GetKey(KeyCode.Space))
        // {
        //     LevelRestart();
        // }
    }

    // restart the level by moving the player to the last checkpoint, which will be on the current level
    private void LevelRestart()
    {
        StartCoroutine(levelChangeManager.LevelSwitch(player, lastCheckPoint.name, currentLevel));

        enemyManager.ResurrectEnemies();
        
    }

    // used to get sprite demensions used in camera clamp
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
