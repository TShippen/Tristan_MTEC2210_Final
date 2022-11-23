using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{

    public Slider slider;
    public GameObject player;
    private PlayerHealthStamina PlayerHealthStamina;
    private PlayerLaunch PlayerLaunch;
    public CanvasGroup canvasGroup;

    [SerializeField] private float currentLaunchCharge;
    private float currentChargeOnSlider;
    private float minLaunchPower;
    private float maxLaunchPower;
    
    

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        PlayerLaunch = player.GetComponent<PlayerLaunch>();
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
        currentLaunchCharge = PlayerLaunch.GetCurrentCharge();
        if(currentLaunchCharge > 0)
        {
            canvasGroup.alpha = 1;
            currentChargeOnSlider = Util.RemapRange(currentLaunchCharge, minLaunchPower, maxLaunchPower, slider.minValue, slider.maxValue);
            slider.value = currentChargeOnSlider;
        }
        
    }


   
    
}
