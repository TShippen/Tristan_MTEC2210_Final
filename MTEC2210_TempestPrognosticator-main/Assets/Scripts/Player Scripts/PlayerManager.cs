using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{


    public GameManager gameManager;

    //player script references
    private PlayerHealthStamina PlayerHealthStamina;
    private PlayerMovement PlayerLaunch;
   
    



    // Start is called before the first frame update
    void Start()
    {
        // get player scripts
        PlayerHealthStamina =  GetComponent<PlayerHealthStamina>();
        PlayerLaunch =  GetComponent<PlayerMovement>();


    }

    // Update is called once per frame
    void Update()
    {
        
        
    }


    
    
}
