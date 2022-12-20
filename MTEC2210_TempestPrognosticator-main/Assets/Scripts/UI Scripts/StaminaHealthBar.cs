using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StaminaHealthBar : MonoBehaviour
{

    // front slider is red, back slider is grey
    public Slider frontSlider;
    public Slider backSlider;
    public GameObject player;
    private PlayerHealthStamina PlayerHealthStamina;

    private float currentHealthStaminaOnSlider;
    private float minStaminaHealth;
    private float maxStaminaHealth;
    private float lerpTimer;
    public float slideTimer;


    // Start is called before the first frame update
    void Start()
    {
        PlayerHealthStamina = player.GetComponent<PlayerHealthStamina>();
        minStaminaHealth = PlayerHealthStamina.minStaminaHealth;
        maxStaminaHealth = PlayerHealthStamina.maxStaminaHealth;

        slideTimer = .2f;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateBarUI();
    }

    private void UpdateBarUI()
    {
        
        currentHealthStaminaOnSlider = Util.RemapRange(PlayerHealthStamina.GetCurrentHealthStamina(), minStaminaHealth, maxStaminaHealth, frontSlider.minValue, frontSlider.maxValue);

        // if the backslider value is greater than the current health mapped to the front slider
        // the front slider (red) will immediately update to the current value and the back slider
        // (grey) will slowly catch up, kind of a chip-away effect
        if (backSlider.value > currentHealthStaminaOnSlider)
        {
            frontSlider.value = currentHealthStaminaOnSlider;
            lerpTimer += Util.FrameDependant(1);
            float percentComplete = lerpTimer / slideTimer;
            
            backSlider.value = Mathf.Lerp(backSlider.value, currentHealthStaminaOnSlider, percentComplete);
        } else if (backSlider.value < currentHealthStaminaOnSlider)
        {
            frontSlider.value = currentHealthStaminaOnSlider;
            lerpTimer += Util.FrameDependant(1);
            float percentComplete = lerpTimer / slideTimer;
            
            backSlider.value = Mathf.Lerp(backSlider.value, currentHealthStaminaOnSlider, percentComplete);
        }
        lerpTimer = 0;
    
    }
}
