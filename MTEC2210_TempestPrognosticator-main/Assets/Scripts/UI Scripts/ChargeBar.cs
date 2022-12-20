using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{

    public Slider slider;
    public GameObject player;
    private PlayerHealthStamina PlayerHealthStamina;
    private PlayerMovement PlayerLaunch;
    public CanvasGroup canvasGroup;

    private float currentLaunchCharge;
    private float currentChargeOnSlider;
    private float minLaunchPower;
    private float maxLaunchPower;
    
    

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        PlayerLaunch = player.GetComponent<PlayerMovement>();
        minLaunchPower = PlayerLaunch.minLaunchPower;
        maxLaunchPower = PlayerLaunch.maxLaunchPower;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdateBarUI();
            
    }
        
    
 

    private void UpdateBarUI()
    {
        // sets the UI to visible or invisible if the charge is not 0
        currentLaunchCharge = PlayerLaunch.GetCurrentCharge();
        canvasGroup.alpha = 0;
        if(currentLaunchCharge > 0)
        {
            canvasGroup.alpha = 1;
            currentChargeOnSlider = Util.RemapRange(currentLaunchCharge, minLaunchPower, maxLaunchPower, slider.minValue, slider.maxValue);
            slider.value = currentChargeOnSlider;
        }
        
        
    }


   
    
}
