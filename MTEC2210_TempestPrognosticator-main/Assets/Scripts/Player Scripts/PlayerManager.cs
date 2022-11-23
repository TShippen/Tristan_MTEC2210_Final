using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{


    public GameManager gameManager;

    //player script references
    private PlayerHealthStamina playerHealthStamina;
    private PlayerLaunch playerLaunch;
   
    



    // Start is called before the first frame update
    void Start()
    {
        // get player scripts
        playerHealthStamina =  GetComponent<PlayerHealthStamina>();
        playerLaunch =  GetComponent<PlayerLaunch>();


    }

    // Update is called once per frame
    void Update()
    {
        
        
  
    }



    
}
