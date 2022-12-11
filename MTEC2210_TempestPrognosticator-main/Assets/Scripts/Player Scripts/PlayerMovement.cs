using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask ground;
    public LayerMask enemy;


    // charging and launching variables
    public float minLaunchPower;            // maximum launch power
    public float maxLaunchPower;            // minimuim launch power
    public bool isCharging;                 // is charging active
    private bool isChargingUp;               // is charging moving up towards its max, used in the charge bar
    [SerializeField] private float currentCharge;             // the force being applied to the player launch when the button is released
    private Vector3 launchDirection;        // direction that the player will be launched, also where the player is "looking"
    private Vector3 mousePosition;          // mouse position taken as long as mouse button is held 
    private bool inFlight = false;          // becomes true if the player is in flight
    public float chargeRate;                // the speed at which the charge increases and decreases
    public float launchCost;

    // attack variables
    public float attackSpeed;
    public bool attacking;
    public float attackTime;
    public bool draining;
    private GameObject currentEnemy;

    // player component references
    private Animator playerAnimator;         // player animator reference
    private new Rigidbody2D rigidbody2D;        // player rigidbody reference
    private BoxCollider2D boxCollider2D;    // player box collider reference
    private SpriteRenderer spriteRenderer;  // player sprite renderer reference

    // get player scripts
    private PlayerHealthStamina PlayerHealthStamina;
    private PlayerManager PlayerManager;


    // Start is called before the first frame update
    void Start()
    {
        // set values for charging and launching
        isCharging = false;
        minLaunchPower = 1f;
        maxLaunchPower = 20f;
        chargeRate = 10f;
        launchCost = 10;

        //set values for attacking
        attackSpeed = .1f;
        attackTime = 2;
        attacking = false;


        // get player components
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerAnimator = GetComponent<Animator>();

        // get player scripts
        PlayerHealthStamina =  GetComponent<PlayerHealthStamina>();
        PlayerManager =  GetComponent<PlayerManager>();
        
    }

    // Update is called once per frame
    void Update()
    {

        // set collider size to sprite size
        // boxCollider2D.size = spriteRenderer.sprite.bounds.size;
        
        // determine mouse positionm
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        DetermineLaunchDirection();

        
        if (!attacking)
        {
            DetermineSpriteDirection();
            if (Input.GetMouseButton(0) && PlayerGroundCheck())
            {
                Charging();
            }

            if (Input.GetMouseButtonUp(0) && PlayerGroundCheck())
            {
                LaunchPlayer();
            }

            if(Input.GetMouseButtonDown(0) && !PlayerGroundCheck())
            {
                StartCoroutine(StartAttack());
            }

        }

       
        
        
    }

    void FixedUpdate() 
    {
        
            
    }
    
    void DetermineSpriteDirection()
    {
        if(launchDirection.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void DetermineLaunchDirection()
    {
        launchDirection = mousePosition - transform.position;
        launchDirection = launchDirection / launchDirection.magnitude;   
  
    }

    void Charging()
    {


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
        
        PlayerHealthStamina.ReduceHealthStaminaLaunch(currentCharge, minLaunchPower, maxLaunchPower);
        isCharging = false;
        playerAnimator.SetBool("charging", isCharging);
        rigidbody2D.AddForce(launchDirection * currentCharge, ForceMode2D.Impulse);



        // reset current charge
        currentCharge = 0;
    }

    

    public float GetCurrentCharge()
    {
        return currentCharge;
    }

    public bool PlayerGroundCheck()
    {
        // checks if the player is grounded or not

        bool groundCheck = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, ground);

        return groundCheck;
    }

    private IEnumerator StartAttack()
    {
        // checks if an enemy is underneath the player
        // this is a bit of a magic number situation, based on testing in the commented-out OnDrawGizmos function
        // this also returns a RaycastHit2D object so that I can get information about whatever it hits
        
        RaycastHit2D enemyCheck = Physics2D.BoxCast(boxCollider2D.bounds.center - transform.up * .5f, new Vector3(2, 1f, 0), 0f, Vector2.down, 0.5f, enemy);
        if (enemyCheck)
        {
            // get information about the enemy
            currentEnemy = enemyCheck.transform.gameObject;
            float enemyMinHealth = currentEnemy.GetComponent<EnemyHealth>().minHealth;
            float currentEnemyHealth = currentEnemy.GetComponent<EnemyHealth>().GetCurrentHealth();
            
            // player attack should hurt enemy by 1/3 of it's health
            float attackValue = currentEnemyHealth / 3;

            // can only truly attack if enemy current health it greater than min health
            if (currentEnemyHealth > enemyMinHealth)
            {
                // deal damage to enemy
                currentEnemy.GetComponent<EnemyHealth>().ReduceHealthDamage(attackValue);
                
                // let the enemy know you're attacking, it's only polite
                currentEnemy.GetComponent<GroundEnemyMovement>().beingAttacked = true;
                // change attacking bool to true
                attacking = true;

                while (!draining)
                {   
                    transform.position = Vector3.MoveTowards(transform.position, currentEnemy.transform.position, attackSpeed);
                    yield return null;
                }

                StartCoroutine(PlayerDraining());
            }        
        }
    }

    private IEnumerator PlayerDraining() 
    {
        float attackTimeRemaining = attackTime;
        Debug.Log(attackTimeRemaining);
        
        
        
            
        while (attackTimeRemaining > 0)
        {
        
            transform.position = new Vector3(currentEnemy.transform.position.x + .1f, currentEnemy.transform.position.y + .01f, currentEnemy.transform.position.z + -1);
            attackTimeRemaining -= Time.deltaTime;

            yield return null;
        }
        
        
        currentEnemy.GetComponent<GroundEnemyMovement>().beingAttacked = false;    
        attacking = false;
        draining = false;
        currentCharge = maxLaunchPower/2; 
        LaunchPlayer();
            
        
            
        
        

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if ((other.gameObject.tag == "Enemy"))
        {
            if (attacking == true)
            {
                draining = true;
            }
        }
        
    }

    

    


    void OnDrawGizmos()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.red;

        
        //Draw a Ray forward from GameObject toward the maximum distance
        Gizmos.DrawRay(boxCollider2D.bounds.center, -transform.up * .25f);
        //Draw a cube at the maximum distance
        Gizmos.DrawWireCube(boxCollider2D.bounds.center - transform.up * .5f, new Vector3(2, 1f, 0));
        
    }
}
