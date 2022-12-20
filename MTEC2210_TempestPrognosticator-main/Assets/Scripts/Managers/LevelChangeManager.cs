using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeManager : MonoBehaviour
{
    public GameManager gameManager;
    public Animator fadeAnimator;
    public float fadeTime;


    // Start is called before the first frame update
    void Start()
    {
        fadeAnimator = GameObject.Find("Fade").GetComponent<Animator>();
        fadeTime = 5;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.layer == 7)
        {
            
            // if last checkpoint was checkpoint 1, move player to next checkpoint (level 2)
            if (gameManager.lastCheckPoint.name == "Checkpoint 1")
            {
                StartCoroutine(LevelSwitch(other.gameObject, "Checkpoint 2", GameManager.ShipLevel.MiddleDeck));
            }
            // if last checkpoint was checkpoint 2, move player to next checkpoint (level 3)

            if (gameManager.lastCheckPoint.name == "Checkpoint 2")
            {
                StartCoroutine(LevelSwitch(other.gameObject, "Checkpoint 3", GameManager.ShipLevel.MainDeck));
            }
        }
        
    }

    
    public IEnumerator LevelSwitch(GameObject player, string targetCheckpoint, GameManager.ShipLevel targetLevel)
    {
        

        

        // freeze the player in place
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        
        // negin the fade out animation
        fadeAnimator.SetBool("Fade Out", true);
        
        // this fade time process is only to delay the rest of the script
        float fadeTimer = fadeTime;
        while (fadeTimer > 0)
        {
            fadeTimer -= Time.deltaTime;
            yield return null;

        }

        // set the current level to the target level passed to the function
        gameManager.currentLevel = targetLevel;
        
        // make sure the player is alive and reset the health
        PlayerHealthStamina playerHealthStamina = player.GetComponent<PlayerHealthStamina>();
        playerHealthStamina.alive = true;
        playerHealthStamina.SetCurrentHealthStamina(playerHealthStamina.maxStaminaHealth);

        // move player to the checkpoint then unfreeze and fade back in
        player.transform.position = GameObject.Find(targetCheckpoint).transform.position;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        fadeAnimator.SetBool("Fade Out", false);


    }

   
}
