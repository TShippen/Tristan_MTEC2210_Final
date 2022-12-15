using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{


    public GameManager gameManager;

    //player script references
    private PlayerHealthStamina PlayerHealthStamina;
    private PlayerMovement PlayerLaunch;
   
    



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // get player scripts
        PlayerHealthStamina =  GetComponent<PlayerHealthStamina>();
        PlayerLaunch =  GetComponent<PlayerMovement>();

        // start from last checkpoint
        transform.position = gameManager.lastCheckPoint.transform.position;


    }

    // Update is called once per frame
    void Update()
    {
     if (Input.GetKeyDown(KeyCode.Space))
     {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
     }   
        
    }


    
    
}
