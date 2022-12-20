using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // this is used to reset the enemies without resetting the scene
    public void ResurrectEnemies()
    {
        // get all the Enemy game objects in an array
        // I wonder if getting the components in an array is faster than getting them during iteration...
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // loop through the array and reset the health
        for (int i = 0; i < enemies.Length; i++)
        {
            EnemyHealth health = enemies[i].GetComponent<EnemyHealth>();
        
            health.currentHealth = health.maxHealth;
        }
    }
}
