using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Animator animator;
    public Sprite ratDead;
    // health variables
    public float maxHealth;
    public float minHealth;
    public float currentHealth;
    public bool alive;

    // Start is called before the first frame update
    void Start()
    {
        // set values for stamina/health
        minHealth = 0;
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // check if alive
        if (GetCurrentHealth() < (maxHealth/2))
        {
            alive = false;
        
        }
        

        if (!alive)
        {
            animator.SetBool("Dead", true);
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void ReduceHealthDamage(float damageAmount) 
    {
        currentHealth -= damageAmount;
        
    }

    
}
