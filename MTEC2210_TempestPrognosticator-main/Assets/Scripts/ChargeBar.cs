using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{

    public Slider slider;
    public GameObject player;
    private PlayerController playerController;

    
    private float currentForce;
    private float minLaunchPower;
    private float maxLaunchPower;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
        playerController = player.GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.isCharging)
        {
            minLaunchPower = playerController.minLaunchPower;
            maxLaunchPower = playerController.maxLaunchPower;
            currentForce = Util.RemapRange(playerController.currentForce,minLaunchPower, maxLaunchPower, slider.minValue, slider.maxValue);
            slider.value = currentForce;       
            
        }
        
    }
    


   
    
}
