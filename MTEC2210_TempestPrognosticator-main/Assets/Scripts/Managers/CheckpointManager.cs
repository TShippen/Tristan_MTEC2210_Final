using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameManager gameManager;

    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        // if the player (layer 7) collides with the checkpoint, set the last checkpoint to this object
        if (other.gameObject.layer == 7)
        {
            
            gameManager.lastCheckPoint = gameObject;
        }
        
    }
}
