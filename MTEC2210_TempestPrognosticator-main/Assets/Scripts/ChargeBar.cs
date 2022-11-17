using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{

    public Slider slider;
    public GameObject player;
    private PlayerController playerController;

    
    private float currentChargeOnSlider;
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
        
        minLaunchPower = playerController.minLaunchPower;
        maxLaunchPower = playerController.maxLaunchPower;
        currentChargeOnSlider = Util.RemapRange(playerController.currentCharge,minLaunchPower, maxLaunchPower, slider.minValue, slider.maxValue);
        slider.value = currentChargeOnSlider;       
            
    }
        
    
    


   
    
}
