using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public LayerMask ground;
    public GameManager gameManager;

    // charging and launching variables
    public float minLaunchPower;            // maximum launch power
    public float maxLaunchPower;            // minimuim launch power
    public bool isCharging;                 // is charging active
    private bool isChargingUp;               // is charging moving up towards its max, used in the charge bar
    public float currentCharge;             // the force being applied to the player launch when the button is released
    private Vector3 launchDirection;        // direction that the player will be launched, also where the player is "looking"
    private Vector3 mousePosition;          // mouse position taken as long as mouse button is held 
    private bool inFlight = false;          // becomes true if the player is in flight
    public float chargeRate;                // the speed at which the charge increases and decreases

    // stamina/life variables


    // player component references
    public Animator playerAnimator;         // player animator reference
    private Rigidbody2D rigidbody2D;        // player rigidbody reference
    private BoxCollider2D boxCollider2D;    // player box collider reference
    private SpriteRenderer spriteRenderer;  // player sprite renderer reference
    
    // other object references
    private CanvasGroup chargeCanvasGroup;  // reference to the canvas group on the charge bar
    public GameObject chargeBar;            // reference to the chargeBar object

    
    



    // Start is called before the first frame update
    void Start()
    {
        // get components used for player movement
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerAnimator = GetComponent<Animator>();
        chargeCanvasGroup = chargeBar.GetComponent<CanvasGroup>();
        chargeCanvasGroup.alpha = 0;

        // set values for charging and launching
        isCharging = false;
        minLaunchPower = 20f;
        maxLaunchPower = 100f;
        chargeRate = 150f;


    }

    // Update is called once per frame
    void Update()
    {
        
        // set collider size to sprite size
        boxCollider2D.size = spriteRenderer.sprite.bounds.size;

        // get mouse position, update sprite based on direction mouse is facing
        DetermineMousePosition();

        
        
  
        if (Input.GetMouseButton(0) && GroundCheck())
        {
            Charging();
        }

        if (Input.GetMouseButtonUp(0))
        {
            LaunchPlayer();
        }
  
    }



    void DetermineMousePosition()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        launchDirection = mousePosition - transform.position;
        launchDirection = launchDirection / launchDirection.magnitude;   

        if(launchDirection.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
            
    }
    
    void Charging()
    {

        // show charge bar UI
        chargeCanvasGroup.alpha = 1;

        isCharging = true;
        playerAnimator.SetBool("charging", isCharging);

        if(isChargingUp)
        {
            currentCharge += Time.deltaTime * chargeRate;
            if(currentCharge > maxLaunchPower)
            {
                isChargingUp = false;
                currentCharge = maxLaunchPower;
            }
        }
        else
        {
            currentCharge -= Time.deltaTime * chargeRate;
            if(currentCharge < minLaunchPower)
            {
                isChargingUp = true;
                currentCharge = minLaunchPower;
            }
        }
    }



    void LaunchPlayer() 
    {
        // disable charge bar UI
        chargeCanvasGroup.alpha = 0;

        isCharging = false;
        playerAnimator.SetBool("charging", isCharging);
        rigidbody2D.AddForce(launchDirection * currentCharge, ForceMode2D.Impulse);

        // reset current charge
        currentCharge = 0;
    }

    public bool GroundCheck()
    {
        bool groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 0.75f, ground);
        return groundCheck;
    }

    
}
