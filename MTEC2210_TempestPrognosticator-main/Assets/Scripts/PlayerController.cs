using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    // public or serialized variables
    public GameManager gameManager;
    public float moveSpeed;

    
    public float minLaunchPower;
    public float maxLaunchPower;  
    public float maxChargeTime;
    public bool isCharging;
    public float currentForce;



    public Animator playerAnimator;
    public GameObject chargeBar;

    

    // private variables
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private CanvasGroup chargeCanvasGroup;
    

    private float startChargeTime;
    private Vector3 launchDirection;
    private Vector3 mousePosition;
    
    private bool inFlight = false;







    // Start is called before the first frame update
    void Start()
    {
        // get components used for player movement
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerAnimator = GetComponent<Animator>();
        chargeCanvasGroup = chargeBar.GetComponent<CanvasGroup>();

        // set values for charging and launching
        isCharging = false;
        maxChargeTime = 5f;
        minLaunchPower = 5f;
        maxLaunchPower = 50f;


    }

    // Update is called once per frame
    void Update()
    {
        
        // set collider size to sprite size
        boxCollider2D.size = spriteRenderer.sprite.bounds.size;

         
        moveSpeed = 5;
        // player horizonal movement
        // float xMove =  Input.GetAxisRaw("Horizontal");
        // transform.Translate(xMove * moveSpeed * Time.deltaTime, 0, 0);

        // player launch movement
        if (Input.GetMouseButtonDown(0))
        {
            startChargeTime = Time.time;
            // rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
            isCharging = true;
            playerAnimator.SetBool("charging", isCharging);

                
        }

        if (Input.GetMouseButton(0))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            launchDirection = mousePosition - transform.position;
            launchDirection = launchDirection / launchDirection.magnitude;

            float totalChargeTime = Time.time - startChargeTime;
            
            currentForce = Mathf.Clamp(Util.RemapRange(totalChargeTime, 0.1f, maxChargeTime, minLaunchPower, maxLaunchPower), minLaunchPower, maxLaunchPower);
            Debug.Log(currentForce);
                        
        }

        

        // player initiate launch
        if (Input.GetMouseButtonUp(0))
        {
            isCharging = false;
            playerAnimator.SetBool("charging", isCharging);
            rigidbody2D.AddForce(launchDirection * currentForce, ForceMode2D.Impulse);
            
        }

        // control charge bar UI
        if (isCharging)
        {
            chargeCanvasGroup.alpha = 1;
        }
        else 
        {
            chargeCanvasGroup.alpha = 0;
        }
    }


    void FixedUpdate() 
    {
        
    }

    
}
