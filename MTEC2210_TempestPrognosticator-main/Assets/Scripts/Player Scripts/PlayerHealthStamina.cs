using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthStamina : MonoBehaviour
{

    // stamina/health variables
    public float minStaminaHealth;
    public float maxStaminaHealth;
    private float currentStaminaHealth;
    public bool alive;

    public float maxLaunchCost;

    // Start is called before the first frame update
    void Start()
    {
        // set values for stamina/health
        minStaminaHealth = 0;
        maxStaminaHealth = 100;
        currentStaminaHealth = maxStaminaHealth;
        alive = true;

        maxLaunchCost = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetCurrentHealthStamina() < 0)
        {
            alive = false;
        }

        if (alive == false)
        {
            
        }
    }

    public float GetCurrentHealthStamina()
    {
        return currentStaminaHealth;
    }

    public float ReduceHealthStaminaLaunch(float launchAmount, float minLaunchPower, float maxLaunchPower) 
    {
        float launchCostRatio = Util.RemapRange(launchAmount, minLaunchPower, maxLaunchPower, 0, maxLaunchCost);
        currentStaminaHealth -= launchCostRatio;
        return currentStaminaHealth;
    }

    public float ReduceHealthStaminaDamage(float damageAmount) 
    {
        currentStaminaHealth -= damageAmount;
        return currentStaminaHealth;
    }

    public float IncreaseCurrentHealthStamina(float increaseAmount) 
    {
        currentStaminaHealth += increaseAmount;
        return currentStaminaHealth;
    }
    
}
