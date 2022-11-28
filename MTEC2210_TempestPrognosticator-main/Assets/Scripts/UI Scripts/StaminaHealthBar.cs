using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StaminaHealthBar : MonoBehaviour
{

    public Slider frontSlider;
    public Slider backSlider;
    public GameObject player;
    private PlayerHealthStamina PlayerHealthStamina;

    private float currentHealthStaminaOnSlider;
    private float minStaminaHealth;
    private float maxStaminaHealth;
    public float lerpTimer;
    public float slideTimer;


    // Start is called before the first frame update
    void Start()
    {
        PlayerHealthStamina = player.GetComponent<PlayerHealthStamina>();
        minStaminaHealth = PlayerHealthStamina.minStaminaHealth;
        maxStaminaHealth = PlayerHealthStamina.maxStaminaHealth;

        slideTimer = .4f;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateBarUI();
    }

    private void UpdateBarUI()
    {
        currentHealthStaminaOnSlider = Util.RemapRange(PlayerHealthStamina.GetCurrentHealthStamina(), minStaminaHealth, maxStaminaHealth, frontSlider.minValue, frontSlider.maxValue);
        if (backSlider.value > currentHealthStaminaOnSlider)
        {
            frontSlider.value = currentHealthStaminaOnSlider;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / slideTimer;
            
            backSlider.value = Mathf.Lerp(backSlider.value, currentHealthStaminaOnSlider, percentComplete);
        }
        lerpTimer = 0;
    
    }
}
