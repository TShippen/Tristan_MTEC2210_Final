using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    // health variables
    public float maxHealth;
    public float minHealth;
    private float currentHealth;
    public bool alive;

    // Start is called before the first frame update
    void Start()
    {
        // set values for stamina/health
        minHealth = 0;
        maxHealth = 100;
        currentHealth = maxHealth;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float ReduceHealthDamage(float damageAmount) 
    {
        currentHealth -= damageAmount;
        return currentHealth;
    }
}
