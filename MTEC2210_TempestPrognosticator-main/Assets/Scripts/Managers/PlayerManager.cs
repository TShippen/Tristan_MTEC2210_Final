using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour
{


    public GameManager gameManager;

    //player script references
    private PlayerHealthStamina playerHealthStamina;
   
   
    



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // get player scripts
        playerHealthStamina =  GetComponent<PlayerHealthStamina>();

        // start from last checkpoint
        transform.position = gameManager.lastCheckPoint.transform.position;


    }

    // Update is called once per frame
    void Update()
    {
      
        
    }


    
    
}
